using System;
using System.Collections;
using Runtime;
using Runtime.GameEvents;
using Runtime.GameModes;
using Runtime.GameModes.Factory;
using Runtime.GameStates;
using ScriptableObjectArchitecture;
using UniRx;
using UnityEngine;

namespace Game.Runtime
{
    public class GameManager : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private FloatVariable _gameStartDelayInSeconds;
        [SerializeField] private FloatVariable _gameStartTimer;
        [SerializeField] private FloatVariable _scoreToReach;
        
        [Header("Dependencies")]
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] GameModeFactory _gameModeFactory;

        [Header("Events")] 
        [SerializeField] private GameEvent _gameStartRequestedEvent;
        [SerializeField] private GameEvent _gameStartedEvent;
        [SerializeField] private PlayerModelGameEvent _gameFinishedEvent;

        [Header("Variables")] 
        [SerializeField] private GameStateVariable _gameState;
        
        private IGameMode _currentGameMode;
        private CompositeDisposable _disposables;

        private void Awake()
        {
            _disposables = new CompositeDisposable();
            _gameState.Value = GameState.Preparation;
        }

        private IEnumerator Start()
        {
            foreach (Player player in _playerManager.GetPlayers())
            {
                _disposables.Add(
                player.Score.Subscribe(score => OnPlayerScoredChanged(player, score))
                );
            }

            // _gameStartRequestedEvent.AddListener(StartGame); FOR LATER
            
            // Delay start game
            _gameState.Value = GameState.Starting;
            _gameStartTimer.Value = _gameStartDelayInSeconds.Value;
            while (_gameStartTimer.Value > 0f)
            {
                yield return null;
                _gameStartTimer.Value -= Time.deltaTime;
            }
            
            StartGame();
        }

        private void StartGame()
        {
            _currentGameMode = _gameModeFactory.Create(GameModeType.Main);
            _currentGameMode.Start();
            
            _gameState.Value = GameState.Gameplay;
            _gameStartedEvent.Raise();
        }

        private void Update()
        {
            switch (_gameState.Value)
            {
                case GameState.Preparation:
                case GameState.Starting:
                case GameState.GameOver:
                    break;
                case GameState.Gameplay:
                    _currentGameMode?.Update(Time.deltaTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
            _disposables = null;
        }

        private void OnPlayerScoredChanged(Player player, float score)
        {
            if (score < _scoreToReach)
            {
                return;
            }
            
            OnGameFinished(player);
        }

        private void OnGameFinished(Player winningPlayer)
        {
            _disposables?.Dispose();
            _disposables = null;
            
            _currentGameMode.Stop();

            _gameState.Value = GameState.GameOver;
            _gameFinishedEvent.Raise(winningPlayer);
        }
    }
}