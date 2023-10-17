using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.Player;
using UnityEngine;

namespace GorillaGong.Runtime
{
    public abstract class PlayerSuccessFailureListener : MonoBehaviour
    {
        [field: SerializeField] protected int PlayerIndex { get; private set; }
        [field: SerializeField] protected PlayerPatterns.PlayerPatterns PlayerPatterns { get; private set; }
        [SerializeField] private PlayerModelGameEvent _playerSuccessEvent;
        [SerializeField] private PlayerModelGameEvent _playerFailEvent;

        private void OnEnable()
        {
            _playerSuccessEvent.AddListener(OnPlayerSuccess);
            _playerFailEvent.AddListener(OnPlayerFail);
        }

        private void OnDisable()
        {
            _playerSuccessEvent.RemoveListener(OnPlayerSuccess);
            _playerFailEvent.RemoveListener(OnPlayerFail);
        }

        protected abstract void OnPlayerSuccess(IPlayerModel player);
        protected abstract void OnPlayerFail(IPlayerModel player);
    }
}