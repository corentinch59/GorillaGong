using Game;
using Runtime.GameModes.Config;
using Runtime.Patterns;

namespace Runtime.GameModes.SpamGameMode
{
    public class SpamGameMode : GameMode<SpamGameModeConfig>
    {
        public SpamGameMode(GameModeConfig gameModeConfig) 
            : base(gameModeConfig as SpamGameModeConfig)
        {
        }

        public override Pattern GetPlayerCurrentPattern(Player player)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}