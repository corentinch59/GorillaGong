using System.Collections.Generic;
using Game;
using Runtime.GameEvents;
using Runtime.GameModes.Config;
using Runtime.Patterns;
using UnityEngine;

namespace Runtime.GameModes.MainGameMode
{
    public class MainGameMode : GameMode<MainGameModeConfig>
    {
        private List<Pattern> _patterns = new();

        private float[] _playersDeathTimer;
        private int[] _playersCurrentIteration;

        public MainGameMode(GameModeConfig gameModeConfig) 
            : base(gameModeConfig as MainGameModeConfig)
        {
        }

        public override void Start()
        {
            _playersCurrentIteration = new int[PlayerManager.PlayersCount()];
            InitPlayersDeathTimer();
            
            GeneratePatterns();
            
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            for (int i = 0; i < _playersDeathTimer.Length; i++)
            {
                _playersDeathTimer[i] -= deltaTime;
                if (_playersDeathTimer[i] <= 0)
                {
                    OnPlayerFailed(PlayerManager.GetPlayers()[i]);
                }
            }
        }

        protected override void OnPlayerFailed(Player player)
        {
            base.OnPlayerFailed(player);
            GoToNextIteration(player);
        }

        protected override void OnPlayerSuccess(Player player)
        {
            base.OnPlayerSuccess(player);
            GoToNextIteration(player);
        }

        private void GoToNextIteration(Player player)
        {
            ResetPlayerDeathTimer(player);
            
            _playersCurrentIteration[player.Index]++;
            if (_playersCurrentIteration[player.Index] >= _patterns.Count)
            {
                GeneratePatterns();
            }

            PlayerPatterns.Values[player.Index] = GetPlayerCurrentPattern(player);
        }

        public override Pattern GetPlayerCurrentPattern(Player player)
        {
            int iterationIndex = _playersCurrentIteration[player.Index];
            return _patterns[iterationIndex];
        }

        private void GeneratePatterns()
        {
            for (int i = 0; i < 100; i++)
            {
                Pattern generatedPattern;

                float random = UnityEngine.Random.Range(0f, 1f);
                if (random < Config.DoubleInputProbability)
                {
                    int[] inputs = new int[2];
                    for (int j = 0; j < inputs.Length; j++)
                    {
                        inputs[j] = GenerateRandomInput();
                    }

                    generatedPattern = new Pattern(inputs);
                }
                else
                {
                    generatedPattern = new Pattern(new[] { GenerateRandomInput() });
                }

                _patterns.Add(generatedPattern);
            }

            Debug.Log($"Patterns: {string.Join(", ", _patterns)}");
        }

        #region Death Timers
        private void InitPlayersDeathTimer()
        {
            _playersDeathTimer = new float[PlayerManager.PlayersCount()];
            for (int i = 0; i < _playersDeathTimer.Length; i++)
            {
                _playersDeathTimer[i] = Config.DeathTime;
            }
        }

        private void ResetPlayerDeathTimer(Player player)
        {
            _playersDeathTimer[player.Index] = Config.DeathTime;
        }
        #endregion
    }
}