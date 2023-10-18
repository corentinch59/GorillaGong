using System.Collections.Generic;
using System.Linq;
using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Patterns;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.SpamGameMode
{
    public class SpamGameMode : GameMode<SpamGameModeConfig>
    {
        public override GameModeType Type => GameModeType.Spam;
        public override bool IsFinished => _isFinished;
        private bool _isFinished;

        private Collection<int> PlayersHitCount => _config.PlayersHitCount;
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
            
            // Populate PlayersHitCount
            _config.PlayersHitCount.Clear();
            for (int i = 0; i < PlayerManager.PlayersCount(); i++)
            {
                _config.PlayersHitCount.Add(0);
            }
            
            _blinkTimer = _config.VisualBlinkDuration;
            
            base.Start();
        }

        public override void Stop()
        {
            // Give the right amount of score for the winner(s) and the loser(s)
            IEnumerable<int> winnersIndex = PlayersHitCount.Select((value, index) => (value, index))
                .Where(tuple => tuple.value == PlayersHitCount.Max())
                .Select(tuple => tuple.index);
            IReadOnlyList<Player.Player> players = PlayerManager.GetPlayers(); 
            for (int i = 0; i < PlayersHitCount.Count; i++)
            {
                bool isWinner = winnersIndex.Contains(i);
                Player.Player player = players[i];
                player.AddScore(isWinner ? _config.WinnerScoreGain : _config.LoserScoreGain);    
                if (isWinner)
                {
                    _config.GameModeStoppedEvent.Raise(player);
                }
            }
            
            base.Stop();
        }

        protected override bool IsRightInputPressed(Player.Player player, int[] inputsIds) => inputsIds.Intersect(WantedInputs).Any();

        // Overriding cause we don't want to decrease player score or call failed event
        protected override void OnPlayerFailed(Player.Player player) { }
        protected override void OnPlayerSuccess(Player.Player player)
        {
            PlayerSuccessEvent.Raise(player);
            PlayersHitCount[player.Index]++;
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
            for (int i = 0; i < PlayersHitCount.Count; i++)
            {
                PlayerPatterns.Values[i] = GetPlayerCurrentPattern(null);
            }
        }
    }
}