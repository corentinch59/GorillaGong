using System.Collections;
using Runtime.GameEvents;
using Runtime.GameModes;
using Runtime.GameModes.Factory;
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
        
        private IGameMode _currentGameMode;
        private bool _isGameFinished;
        private CompositeDisposable _disposables;

        private void Awake()
        {
            _disposables = new CompositeDisposable();
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
            
            _gameStartedEvent.Raise();
        }

        private void Update()
        {
            if (_currentGameMode == null || _isGameFinished)
            {
                return;
            }
            
            _currentGameMode.Update(Time.deltaTime);
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

            _isGameFinished = true;
            _gameFinishedEvent.Raise(winningPlayer);
        }
    }
}