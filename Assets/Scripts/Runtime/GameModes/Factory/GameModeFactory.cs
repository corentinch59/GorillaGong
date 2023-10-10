using System.ComponentModel;
using Game;
using Runtime.GameModes.Config;

namespace Runtime.GameModes.Factory
{
    public class GameModeFactory : IGameModeFactory
    {
        private GameModeConfigDictionary _gameModeConfigs;
        private PlayerManager _playerManager;

        public GameModeFactory(PlayerManager playerManager, GameModeConfigDictionary gameModeConfigs)
        {
            _playerManager = playerManager;
            _gameModeConfigs = gameModeConfigs;
        }

        public IGameMode Create(GameModeType type)
        {
            GameModeConfig gameModeConfig = GetGameModeConfig(type);
            return type switch
            {
                GameModeType.Simple => new SimpleGameMode(gameModeConfig, _playerManager),
                _ => throw new InvalidEnumArgumentException()
            };
        }

        public GameModeConfig GetGameModeConfig(GameModeType type)
        {
            if(!_gameModeConfigs.Values.TryGetValue(type, out GameModeConfig config))
            {
                throw new InvalidEnumArgumentException();
            }
            return config;
        }
    }
}