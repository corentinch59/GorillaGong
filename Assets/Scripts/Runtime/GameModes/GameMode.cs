using System.Collections.Generic;
using Game;
using Runtime.GameModes.Config;
using UniRx;
using UnityEngine;

namespace Runtime.GameModes
{
    public abstract class GameMode : IGameMode
    {
        public abstract IReadOnlyList<int> Patterns { get; }

        protected readonly PlayerManager _playerManager;
        private readonly PlayerPatterns.PlayerPatterns _playerPatterns;
        protected readonly GameModeConfig _config;
        private Dictionary<Player, int> _playersCurrentIteration = new Dictionary<Player, int>();

        protected GameMode(GameModeConfig gameModeConfig, PlayerManager playerManager, PlayerPatterns.PlayerPatterns playerPatterns)
        {
            _config = gameModeConfig;
            _playerManager = playerManager;
            _playerPatterns = playerPatterns;
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

        private void OnPlayerInputPressed(Player player, int inputId)
        {
            // TODO: delegate validation to a "Pattern"
            bool rightInputPressed = inputId == GetCurrentPlayerPattern(player);
            if (rightInputPressed)
            {
                player.AddScore(_config.ScoreGain);
                // TODO: trigger SOEvents PlayerSuccess
                Debug.Log("WIN");
            }
            else
            {
                player.RemoveScore(_config.ScoreLoss);
                // TODO: trigger SOEvents PlayerFail
                Debug.Log("LOSS");
            }

            _playersCurrentIteration[player]++;
            _playerPatterns.Values[player.Index] = GetCurrentPlayerPattern(player);
        }

        protected int GetCurrentPlayerPattern(Player player)
        {
            _playersCurrentIteration.TryAdd(player, 0);
            int iterationIndex = _playersCurrentIteration[player];
            return Patterns[iterationIndex];
        }

        protected abstract void GeneratePatterns();
    }
}