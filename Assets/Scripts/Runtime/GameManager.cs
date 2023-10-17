using System;
using System.Collections;
using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.GameModes;
using GorillaGong.Runtime.GameModes.Factory;
using GorillaGong.Runtime.GameStates;
using GorillaGong.Runtime.Player;
using GorillaGong.Runtime.Variables;
using ScriptableObjectArchitecture;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime
{
    public class GameManager : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private FloatVariable _gameStartDelayInSeconds;
        [SerializeField] private FloatVariable _scoreToReach;
        [SerializeField] private Vector2Variable _gameModesIntervalInSecondsMinMax;
        [SerializeField] private FloatVariable _gameModePreparationDuration;
        [SerializeField] private FloatVariable _gameModeEndTransitionDuration;
        
        [Header("Dependencies")]
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] GameModeFactory _gameModeFactory;

        [Header("Events")] 
        [SerializeField] private GameEvent _gameStartRequestedEvent;
        [SerializeField] private GameEvent _gameStartedEvent;
        [SerializeField] private PlayerModelGameEvent _gameFinishedEvent;

        [Header("Variables")] 
        [SerializeField] private GameStateVariable _gameState;
        [SerializeField] private FloatVariable _gameStartTimer;
        [SerializeField] private FloatVariable _gameModePreparationTimer;
        [SerializeField] private FloatVariable _gameModeTransitionTimer;
        [SerializeField] private GameModeTypeVariable _currentGameModeType;
        [SerializeField] private GameModeConfigVariable _currentGameModeConfig;
        
        private IGameMode _currentGameMode;
        private IGameMode _mainGameMode;
        private float _eventTimer;
        private CompositeDisposable _disposables;

        private void Awake()
        {
            _disposables = new CompositeDisposable();
            _gameState.Value = GameState.Preparation;
        }

        private void Start()
        {
            foreach (Player.Player player in _playerManager.GetPlayers())
            {
                _disposables.Add(
                player.Score.Subscribe(score => OnPlayerScoredChanged(player, score))
                );
            }

            _gameStartRequestedEvent.AddListener(StartGame);
        }
        
        private void OnDestroy()
        {
            _disposables?.Dispose();
            _disposables = null;
        }
        
        private void StartGame()
        {
            StartCoroutine(StartGameAsync());
            _gameStartRequestedEvent.RemoveListener(StartGame);
        }
        private IEnumerator StartGameAsync()
        {
            // Delay start game
            _gameState.Value = GameState.Starting;
            _gameStartTimer.Value = _gameStartDelayInSeconds.Value;
            while (_gameStartTimer.Value > 0f)
            {
                yield return null;
                _gameStartTimer.Value -= Time.deltaTime;
            }
            
            _mainGameMode = _gameModeFactory.Create(GameModeType.Main);
            SetCurrentGameMode(_mainGameMode);
            _eventTimer = UnityEngine.Random.Range(_gameModesIntervalInSecondsMinMax.Value.x,
                _gameModesIntervalInSecondsMinMax.Value.y);
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
                    GameplayUpdate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GameplayUpdate()
        {
            if (_currentGameMode is null)
            {
                _gameModeTransitionTimer.Value -= Time.deltaTime;
                if (_gameModeTransitionTimer <= 0f)
                {
                    _eventTimer = UnityEngine.Random.Range(_gameModesIntervalInSecondsMinMax.Value.x,
                        _gameModesIntervalInSecondsMinMax.Value.y);
                    SetCurrentGameMode(_mainGameMode);
                    _currentGameMode.Start();
                }
                return;
            }

            if (_currentGameMode.IsPlaying)
            {
                _currentGameMode.Update(Time.deltaTime);
            }

            // Handle game modes timer and game mode preparation
            if (_currentGameMode == _mainGameMode)
            {
                if (_currentGameMode.IsPlaying)
                {
                    _eventTimer -= Time.deltaTime;
                    if (_eventTimer <= 0f)
                    {
                        _currentGameMode.Stop();
                        _gameModePreparationTimer.Value = _gameModePreparationDuration.Value;
                        Debug.Log("Stopped main game mode");
                    }
                }
                else
                {
                    _gameModePreparationTimer.Value -= Time.deltaTime;
                    if (_gameModePreparationTimer.Value <= 0f)
                    {
                        Debug.Log("Starting new game mode");
                        SetCurrentGameMode(_gameModeFactory.CreateRandom());
                        _currentGameMode.Start();
                    }
                }
                return;
            }
            
            // GameMode is not main game mode
            // Handle game mode switch on current game mode end
            if (!_currentGameMode.IsFinished)
            {
                return;
            }
            _currentGameMode.Stop();
            SetCurrentGameMode(null);
            _gameModeTransitionTimer.Value = _gameModeEndTransitionDuration.Value;
            Debug.Log("Stopped game mode");
        }

        private void OnPlayerScoredChanged(Player.Player player, float score)
        {
            if (score < _scoreToReach)
            {
                return;
            }
            
            OnGameFinished(player);
        }

        private void OnGameFinished(Player.Player winningPlayer)
        {
            _disposables?.Dispose();
            _disposables = null;
            
            _gameState.Value = GameState.GameOver;
            _gameFinishedEvent.Raise(winningPlayer);

            _currentGameMode.Disable();
            SetCurrentGameMode(null);
        }

        private void SetCurrentGameMode(IGameMode newGameMode)
        {
            _currentGameMode = newGameMode;
            
            _currentGameModeType.Value = _currentGameMode?.Type ?? GameModeType.None;
            _currentGameModeConfig.Value = _currentGameMode?.ConfigBase;
        }
    }
}