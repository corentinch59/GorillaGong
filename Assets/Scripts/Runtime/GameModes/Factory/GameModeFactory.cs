using System.ComponentModel;
using Game;
using Runtime.GameEvents;
using Runtime.GameModes.Config;
using UnityEngine;

namespace Runtime.GameModes.Factory
{
    public class GameModeFactory : MonoBehaviour, IGameModeFactory
    {
        [SerializeField] private GameModeConfigDictionary _gameModeConfigs;
        [SerializeField] private PlayerPatterns.PlayerPatterns _playerPatterns;
        [SerializeField] private PlayerModelGameEvent _playerSuccessEvent;
        [SerializeField] private PlayerModelGameEvent _playerFailEvent;
        [SerializeField] private PlayerManager _playerManager;

        private void Awake()
        {
            _playerPatterns.ResetValues();
        }

        public IGameMode Create(GameModeType type)
        {
            GameModeConfig gameModeConfig = GetGameModeConfig(type);
            return type switch
            {
                GameModeType.Simple => new SimpleGameMode(gameModeConfig, _playerManager, _playerPatterns, _playerSuccessEvent, _playerFailEvent),
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