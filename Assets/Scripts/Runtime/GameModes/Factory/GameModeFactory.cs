using System;
using System.ComponentModel;
using System.Linq;
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

        public IGameMode CreateRandom()
        {
            var possibilities = _gameModeConfigs.Values.Keys.ToList();
            possibilities.Remove(GameModeType.Main);

            if (possibilities.Count == 0)
            {
                throw new Exception("No other GameMode than 'Main' in the available GameModes");
            }

            return Create(possibilities[UnityEngine.Random.Range(0, possibilities.Count)]);
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