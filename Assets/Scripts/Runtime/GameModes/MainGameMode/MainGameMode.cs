using System.Collections.Generic;
using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Patterns;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.MainGameMode
{
    public class MainGameMode : GameMode<MainGameModeConfig>
    {
        public override GameModeType Type => GameModeType.Main;
        public override bool IsFinished => false;
        
        private List<Pattern> _patterns = new();
        
        private float[] _playersDeathTimer;
        private int[] _playersCurrentIteration;

        public MainGameMode(GameModeConfig gameModeConfig) 
            : base(gameModeConfig as MainGameModeConfig)
        {
        }

        public override void Start()
        {
            InitPlayersDeathTimer();
            if (_playersCurrentIteration is null)
            {
                _playersCurrentIteration = new int[PlayerManager.PlayersCount()];
                GeneratePatterns();
            }

            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            _config.GameModeStoppedEvent.Raise();
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

        protected override void OnPlayerFailed(Player.Player player)
        {
            base.OnPlayerFailed(player);
            GoToNextIteration(player);
        }
        
        protected override void OnPlayerSuccess(Player.Player player)
        {
            base.OnPlayerSuccess(player);
            GoToNextIteration(player);
        }

        private void GoToNextIteration(Player.Player player)
        {
            ResetPlayerDeathTimer(player);
            
            _playersCurrentIteration[player.Index]++;
            if (_playersCurrentIteration[player.Index] >= _patterns.Count)
            {
                GeneratePatterns();
            }

            PlayerPatterns.Values[player.Index] = GetPlayerCurrentPattern(player);
        }
        
        public override Pattern GetPlayerCurrentPattern(Player.Player player)
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
                if (random < _config.DoubleInputProbability)
                {
                    int[] inputs = new int[2];
                    inputs[0] = GenerateRandomInput();
                    do { inputs[1] = GenerateRandomInput(); }
                    while (inputs[0] == inputs[1]);

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
                _playersDeathTimer[i] = _config.DeathTime;
            }
        }

        private void ResetPlayerDeathTimer(Player.Player player)
        {
            _playersDeathTimer[player.Index] = _config.DeathTime;
        }
        #endregion
    }
}