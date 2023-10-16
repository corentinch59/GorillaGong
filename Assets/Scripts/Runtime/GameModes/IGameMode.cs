using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Patterns;

namespace GorillaGong.Runtime.GameModes
{
    public interface IGameMode
    {
        public GameModeConfig ConfigBase { get; }
        public GameModeType Type { get; }
        public bool IsFinished { get; }
        public bool IsPlaying { get; }
        
        public Pattern GetPlayerCurrentPattern(Player.Player player);
        public void Start();
        public void Update(float deltaTime);
        public void Stop();
    }
}