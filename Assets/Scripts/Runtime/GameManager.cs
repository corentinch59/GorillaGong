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
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private FloatVariable _scoreToReach;
        
        [Header("Events")]
        [SerializeField] private PlayerModelGameEvent _gameFinishedEvent;
        [SerializeField] GameModeFactory _gameModeFactory;

        private IGameMode _currentGameMode;
        private CompositeDisposable _disposables;

        private void Awake()
        {
            _disposables = new CompositeDisposable();
        }

        private void Start()
        {
            foreach (Player player in _playerManager.GetPlayers())
            {
                _disposables.Add(
                player.Score.Subscribe(score => OnPlayerScoredChanged(player, score))
                );
            }
            
            _currentGameMode = _gameModeFactory.Create(GameModeType.Simple);
            _currentGameMode.Start();
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
            
            _gameFinishedEvent.Raise(winningPlayer);
            
        }
    }
}