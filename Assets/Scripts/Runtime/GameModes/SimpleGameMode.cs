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

        public SimpleGameMode(GameModeConfig gameModeConfig, PlayerManager playerManager, 
            PlayerPatterns.PlayerPatterns playerPatterns, PlayerModelGameEvent playerSuccessEvent, 
            PlayerModelGameEvent playerFailEvent) 
            : base(gameModeConfig, playerManager, playerPatterns, playerSuccessEvent, playerFailEvent)
        {
        }

        protected override void GeneratePatterns()
        {
            for (int i = 0; i < 100; i++)
            {
                _patterns.Add( new Pattern(new []{UnityEngine.Random.Range(0, 4)}));
            }
            Debug.Log($"Patterns: {string.Join(", ", _patterns)}");
        }
    }
}