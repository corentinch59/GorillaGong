using System.Collections.Generic;
using System.Linq;
using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Patterns;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.SpamGameMode
{
    public class SpamGameMode : GameMode<SpamGameModeConfig>
    {
        public override GameModeType Type => GameModeType.Spam;
        public override bool IsFinished => _isFinished;
        private bool _isFinished;

        private int[] _playersInputsCount;
        private int _offset = 0;
        private static readonly int[] WantedInputs = new[] { 0, 1 };

        private float _timer;
        private float _blinkTimer;

        public SpamGameMode(GameModeConfig gameModeConfig) 
            : base(gameModeConfig as SpamGameModeConfig)
        {
        }

        public override void Start()
        {
            _timer = _config.EventDuration;
            _playersInputsCount = new int[PlayerManager.PlayersCount()];
            
            _blinkTimer = _config.VisualBlinkDuration;
            
            base.Start();
        }

        public override void Stop()
        {
            IEnumerable<int> winnersIndex = _playersInputsCount.Select((value, index) => (value, index))
                .Where(tuple => tuple.value == _playersInputsCount.Max())
                .Select(tuple => tuple.index);
            IReadOnlyList<Player.Player> players = PlayerManager.GetPlayers(); 
            foreach (int winnerIndex in winnersIndex)
            {
                Player.Player player = players[winnerIndex];
                player.AddScore(_config.ScoreGain);
                _config.GameModeStoppedEvent.Raise(player);
            }
            
            base.Stop();
        }

        protected override bool IsRightInputPressed(Player.Player player, int[] inputsIds) => inputsIds.Intersect(WantedInputs).Any();

        // Overriding cause we don't want to decrease player score or call failed event
        protected override void OnPlayerFailed(Player.Player player) { }
        protected override void OnPlayerSuccess(Player.Player player)
        {
            PlayerSuccessEvent.Raise(player);
            _playersInputsCount[player.Index]++;
            PlayerPatterns.Values[player.Index] = GetPlayerCurrentPattern(player);
        }

        public override Pattern GetPlayerCurrentPattern(Player.Player player)
        {
            return new Pattern(new int[] { _offset % (WantedInputs.Length) });
        }

        public override void Update(float deltaTime)
        {
            if (_isFinished)
            {
                return;
            }

            BlinkUpdate(deltaTime);

            _timer -= deltaTime;
            if (_timer > 0)
            {
                return;
            }
            _isFinished = true;
        }

        private void BlinkUpdate(float deltaTime)
        {
            _blinkTimer -= deltaTime;
            if (_blinkTimer > 0)
            {
                return;
            }
            _blinkTimer = _config.VisualBlinkDuration;

            _offset++;
            for (int i = 0; i < _playersInputsCount.Length; i++)
            {
                PlayerPatterns.Values[i] = GetPlayerCurrentPattern(null);
            }
        }
    }
}