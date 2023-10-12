using System.ComponentModel;
using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Player;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.Factory
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
            GameMode gameMode = type switch
            {
                GameModeType.Main => new MainGameMode.MainGameMode(gameModeConfig),
                GameModeType.Spam => new SpamGameMode.SpamGameMode(gameModeConfig),
                _ => throw new InvalidEnumArgumentException()
            };
            gameMode.SetPlayerManager(_playerManager)
                .SetPlayerPatterns(_playerPatterns)
                .SetPlayerFailEvent(_playerFailEvent)
                .SetPlayerSuccessEvent(_playerSuccessEvent);
            return gameMode;
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