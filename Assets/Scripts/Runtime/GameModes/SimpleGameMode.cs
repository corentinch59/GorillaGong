using System.Collections.Generic;
using Game;
using Runtime.GameModes.Config;
using UnityEngine;

namespace Runtime.GameModes
{
    public class SimpleGameMode : GameMode
    {
        public override IReadOnlyList<int> Patterns => _patterns;
        private List<int> _patterns = new();

        public SimpleGameMode(GameModeConfig gameModeConfig, PlayerManager playerManager, PlayerPatterns.PlayerPatterns playerPatterns) 
            : base(gameModeConfig, playerManager, playerPatterns)
        {
        }

        protected override void GeneratePatterns()
        {
            for (int i = 0; i < 100; i++)
            {
                _patterns.Add(UnityEngine.Random.Range(0, 4));
            }
            Debug.Log($"Patterns: {string.Join(", ", _patterns)}");
        }
    }
}