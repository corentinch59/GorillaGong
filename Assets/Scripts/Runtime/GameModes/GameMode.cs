using System.Collections.Generic;
using Game;
using Runtime.GameEvents;
using Runtime.GameModes.Config;
using Runtime.Patterns;

namespace Runtime.GameModes
{
    public abstract class GameMode : IGameMode
    {
        public abstract IReadOnlyList<Pattern> Patterns { get; }

        protected readonly PlayerManager _playerManager;
        private readonly PlayerPatterns.PlayerPatterns _playerPatterns;
        private readonly PlayerModelGameEvent _playerSuccessEvent;
        private readonly PlayerModelGameEvent _playerFailEvent;
        protected readonly GameModeConfig _config;
        private Dictionary<Player, int> _playersCurrentIteration = new Dictionary<Player, int>();

        protected GameMode(GameModeConfig gameModeConfig, PlayerManager playerManager, PlayerPatterns.PlayerPatterns playerPatterns,
            PlayerModelGameEvent playerSuccessEvent, PlayerModelGameEvent playerFailEvent)
        {
            _config = gameModeConfig;
            _playerManager = playerManager;
            _playerPatterns = playerPatterns;
            _playerSuccessEvent = playerSuccessEvent;
            _playerFailEvent = playerFailEvent;
            
            GeneratePatterns();
        }

        public virtual void Start()
        {
            foreach (Player player in _playerManager.GetPlayers())
            {
                player.OnInputPressed += OnPlayerInputPressed;
                _playerPatterns.Values.Add(player.Index, GetCurrentPlayerPattern(player));
            }
        }

        public virtual void Stop()
        {
            foreach (Player player in _playerManager.GetPlayers())
            {
                player.OnInputPressed -= OnPlayerInputPressed;
            }
        }

        protected virtual void OnPlayerInputPressed(Player player, int[] inputsIds)
        {
            bool rightInputPressed = GetCurrentPlayerPattern(player).IsInputValid(inputsIds);
            if (rightInputPressed)
            {
                player.AddScore(_config.ScoreGain);
                _playerSuccessEvent.Raise(player);
            }
            else
            {
                player.RemoveScore(_config.ScoreLoss);
                _playerFailEvent.Raise(player);
            }

            int playerNewCurrentIteration = _playersCurrentIteration[player] + 1;
            _playersCurrentIteration[player] = playerNewCurrentIteration;

            if (playerNewCurrentIteration >= Patterns.Count)
            {
                GeneratePatterns();
            }
            
            _playerPatterns.Values[player.Index] = GetCurrentPlayerPattern(player);
        }

        protected Pattern GetCurrentPlayerPattern(Player player)
        {
            _playersCurrentIteration.TryAdd(player, 0);
            int iterationIndex = _playersCurrentIteration[player];
            return Patterns[iterationIndex];
        }

        protected abstract void GeneratePatterns();
    }
}