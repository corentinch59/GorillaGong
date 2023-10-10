using System.Collections.Generic;
using Game;
using Runtime.GameModes.Config;
using UnityEngine.InputSystem;

namespace Runtime.GameModes
{
    public class SimpleGameMode : GameMode
    {
        public override IReadOnlyList<int> Patterns => _patterns;
        private List<int> _patterns;

        public SimpleGameMode(GameModeConfig gameModeConfig, PlayerManager playerManager) : base(gameModeConfig, playerManager)
        {
            
        }

        public override void Start()
        {
            
        }

        protected override void GeneratePatterns()
        {
            
        }
    }
}