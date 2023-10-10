using System;
using System.ComponentModel;
using Game;
using Runtime.GameModes.Config;

namespace Runtime.GameModes.Factory
{
    public class GameModeFactory : IGameModeFactory
    {
        private readonly GameModeConfigDictionary _gameModeConfigs;
        private readonly PlayerPatterns.PlayerPatterns _playerPatterns;
        private readonly PlayerManager _playerManager;

        public GameModeFactory(PlayerManager playerManager, GameModeConfigDictionary gameModeConfigs, PlayerPatterns.PlayerPatterns playerPatterns)
        {
            _playerManager = playerManager;
            _gameModeConfigs = gameModeConfigs;
            _playerPatterns = playerPatterns;
        }

        public IGameMode Create(GameModeType type)
        {
            GameModeConfig gameModeConfig = GetGameModeConfig(type);
            return type switch
            {
                GameModeType.Simple => new SimpleGameMode(gameModeConfig, _playerManager, _playerPatterns),
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