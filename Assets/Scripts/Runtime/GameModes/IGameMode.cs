using GorillaGong.Runtime.Patterns;

namespace GorillaGong.Runtime.GameModes
{
    public interface IGameMode
    {
        public bool IsFinished { get; }
        
        public Pattern GetPlayerCurrentPattern(Player.Player player);
        public void Start();
        public void Update(float deltaTime);
        public void Stop();
    }
}