using System.Collections.Generic;
using Game;
using Runtime.GameModes.Config;
using UnityEngine;

namespace Runtime.GameModes
{
    public abstract class GameMode : IGameMode
    {
        public abstract IReadOnlyList<int> Patterns { get; }

        protected PlayerManager _playerManager;
        protected GameModeConfig _config;
        private Dictionary<Player, int> _playersCurrentIteration = new Dictionary<Player, int>();

        protected GameMode(GameModeConfig gameModeConfig, PlayerManager playerManager)
        {
            _config = gameModeConfig;
            _playerManager = playerManager;
            GeneratePatterns();
        }

        public virtual void Start()
        {
            foreach (Player player in _playerManager.GetPlayers())
            {
                player.OnInputPressed += OnPlayerInputPressed;
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
            _playersCurrentIteration.TryAdd(player, 0);
            int iterationIndex = _playersCurrentIteration[player];
            
            // TODO: delegate validation to a "Pattern"
            bool rightInputPressed = inputId == Patterns[iterationIndex];
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
        }

        protected abstract void GeneratePatterns();
    }
}