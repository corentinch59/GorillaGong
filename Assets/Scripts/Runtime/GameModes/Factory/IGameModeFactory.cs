using Runtime.GameModes.Config;

namespace Runtime.GameModes.Factory
{
    public interface IGameModeFactory
    {
        public IGameMode Create(GameModeType type);
        public GameModeConfig GetGameModeConfig(GameModeType type);
    }
}