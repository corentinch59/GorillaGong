using System.Collections.Generic;
using Game;
using Runtime.GameModes.Config;
using Runtime.Patterns;
using UnityEngine;

namespace Runtime.GameModes
{
    public abstract class GameMode : IGameMode
    {
        public abstract IReadOnlyList<Pattern> Patterns { get; }

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

        private void OnPlayerInputPressed(Player player, int[] inputsIds)
        {
            bool rightInputPressed = GetCurrentPlayerPattern(player).IsInputValid(inputsIds);
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

        protected Pattern GetCurrentPlayerPattern(Player player)
        {
            _playersCurrentIteration.TryAdd(player, 0);
            int iterationIndex = _playersCurrentIteration[player];
            return Patterns[iterationIndex];
        }

        protected abstract void GeneratePatterns();
    }
}