using System.Collections.Generic;
using System.Linq;
using Game;
using Runtime.GameModes.Config;
using Runtime.Patterns;

namespace Runtime.GameModes.SpamGameMode
{
    public class SpamGameMode : GameMode<SpamGameModeConfig>
    {
        public override bool IsFinished => _isFinished;
        private bool _isFinished;
        
        private int[] _playersInputsCount;
        private int _offset = 0;

        private float _timer;

        public SpamGameMode(GameModeConfig gameModeConfig) 
            : base(gameModeConfig as SpamGameModeConfig)
        {
        }

        public override void Start()
        {
            _timer = Config.TotalDuration;

            _playersInputsCount = new int[PlayerManager.PlayersCount()];
            _offset = UnityEngine.Random.Range(0, 2);
            base.Start();
        }

        // Overriding cause we don't want to decrease player score or call failed event
        protected override void OnPlayerFailed(Player player) { }
        protected override void OnPlayerSuccess(Player player)
        {
            _playersInputsCount[player.Index]++;
            PlayerPatterns.Values[player.Index] = GetPlayerCurrentPattern(player);
        }

        public override Pattern GetPlayerCurrentPattern(Player player)
        {
            int inputValue = (_playersInputsCount[player.Index] + _offset) % 2;
            return new Pattern(new[] { inputValue });
        }

        public override void Update(float deltaTime)
        {
            if (_isFinished)
            {
                return;
            }
            
            _timer -= deltaTime;
            if (_timer > 0)
            {
                return;
            }
            
            IEnumerable<int> winnersIndex = _playersInputsCount.Select((value, index) => (value, index))
                .Where(tuple => tuple.value == _playersInputsCount.Max())
                .Select(tuple => tuple.index);
            IReadOnlyList<Player> players = PlayerManager.GetPlayers(); 
            foreach (int winnerIndex in winnersIndex)
            {
                Player player = players[winnerIndex];
                player.AddScore(Config.ScoreGain);
                Config.GameModeStoppedEvent.Raise(player);
            }
            _isFinished = true;
        }
    }
}