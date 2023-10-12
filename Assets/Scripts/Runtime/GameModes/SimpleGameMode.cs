using System;
using System.Collections.Generic;
using Game;
using Runtime.GameEvents;
using Runtime.GameModes.Config;
using Runtime.Patterns;
using UnityEngine;

namespace Runtime.GameModes
{
    public class SimpleGameMode : GameMode
    {
        public override IReadOnlyList<Pattern> Patterns => _patterns;
        private List<Pattern> _patterns = new();

        private MainGameModeConfig _mainGameModeConfig;

        public SimpleGameMode(GameModeConfig gameModeConfig, PlayerManager playerManager, 
            PlayerPatterns.PlayerPatterns playerPatterns, PlayerModelGameEvent playerSuccessEvent, 
            PlayerModelGameEvent playerFailEvent) 
            : base(gameModeConfig, playerManager, playerPatterns, playerSuccessEvent, playerFailEvent)
        {
            if (gameModeConfig is not MainGameModeConfig mainGameModeConfig)
            {
                throw new Exception("GameModeConfig is not a MainGameModeConfig");
            }
            _mainGameModeConfig = mainGameModeConfig;
        }

        protected override void GeneratePatterns()
        {
            for (int i = 0; i < 100; i++)
            {
                Pattern generatedPattern;
                
                float random = UnityEngine.Random.Range(0f, 1f);
                if (random < _mainGameModeConfig.DoubleInputProbability)
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

        private int GenerateRandomInput() => UnityEngine.Random.Range(0, 4);
    }
}