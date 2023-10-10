using System;
using Runtime.GameModes;
using Runtime.GameModes.Config;
using Runtime.GameModes.Factory;
using UnityEngine;

namespace Game.Runtime
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private GameModeConfigDictionary _gameModeConfigs;
        private IGameModeFactory _gameModeFactory;

        private IGameMode _currentGameMode;

        private void Awake()
        {
            _gameModeFactory = new GameModeFactory(_playerManager, _gameModeConfigs);
            _currentGameMode = _gameModeFactory.Create(GameModeType.Simple);
            _currentGameMode.Start();
        }
    }
}