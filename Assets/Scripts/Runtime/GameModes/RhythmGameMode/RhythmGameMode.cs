using GorillaGong.Runtime.GameModes;
using GorillaGong.Runtime.Patterns;
using GorillaGong.Runtime.Player;

namespace Runtime.GameModes.RhythGameMode
{
    public class RhythmGameMode : GameMode<RhythmGameModeConfig>
    {
        private bool _isFinished;
        public override bool IsFinished => _isFinished;

        public RhythmGameMode(RhythmGameModeConfig gameModeConfig) : base(gameModeConfig)
        {
        }

        protected override void OnPlayerFailed(Player player) { }

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

