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
        private int[] _playersCurrentIteration;
        private float[] _playersDeathTimer;

        protected GameMode(GameModeConfig gameModeConfig, PlayerManager playerManager, PlayerPatterns.PlayerPatterns playerPatterns,
            PlayerModelGameEvent playerSuccessEvent, PlayerModelGameEvent playerFailEvent)
        {
            _config = gameModeConfig;
            _playerManager = playerManager;
            _playerPatterns = playerPatterns;
            _playerSuccessEvent = playerSuccessEvent;
            _playerFailEvent = playerFailEvent;

            int playersCount = _playerManager.PlayersCount();
            _playersCurrentIteration = new int[playersCount];
            _playersDeathTimer = new float[playersCount];
            for (int i = 0; i < playersCount; i++)
            {
                _playersDeathTimer[i] = _config.DeathTime;
            }
            
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

        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _playersDeathTimer.Length; i++)
            {
                _playersDeathTimer[i] -= deltaTime;
                if (_playersDeathTimer[i] <= 0)
                {
                    OnPlayerFailed(_playerManager.GetPlayers()[i]);
                }
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
                OnPlayerSuccess(player);
            }
            else
            {
                OnPlayerFailed(player);
            }
        }

        private void OnPlayerSuccess(Player player)
        {
            player.AddScore(_config.ScoreGain);
            _playerSuccessEvent.Raise(player);
            
            GoToNextIteration(player);
        }

        private void OnPlayerFailed(Player player)
        {
            player.RemoveScore(_config.ScoreLoss);
            _playerFailEvent.Raise(player);
            
            GoToNextIteration(player);
        }
        
        private void GoToNextIteration(Player player)
        {
            _playersDeathTimer[player.Index] = _config.DeathTime;
            
            _playersCurrentIteration[player.Index]++;
            if (_playersCurrentIteration[player.Index] >= Patterns.Count)
            {
                GeneratePatterns();
            }

            _playerPatterns.Values[player.Index] = GetCurrentPlayerPattern(player);
        }

        protected Pattern GetCurrentPlayerPattern(Player player)
        {
            int iterationIndex = _playersCurrentIteration[player.Index];
            return Patterns[iterationIndex];
        }

        protected abstract void GeneratePatterns();
    }
}