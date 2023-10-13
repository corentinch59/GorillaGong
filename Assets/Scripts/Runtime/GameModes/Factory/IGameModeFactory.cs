using GorillaGong.Runtime.GameModes.Config;

namespace GorillaGong.Runtime.GameModes.Factory
{
    public interface IGameModeFactory
    {
        public IGameMode Create(GameModeType type);
        public GameModeConfig GetGameModeConfig(GameModeType type);
        public IGameMode CreateRandom();
    }
}