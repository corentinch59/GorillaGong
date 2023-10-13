using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.Player;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaGong.Runtime.UI.StartMenu
{
    public class PlayerReadinessElement : MonoBehaviour
    {
        [SerializeField] private int _playerIndex;
        [SerializeField] private PlayerModelGameEvent _playerReadyEvent;
        [SerializeField] private PlayerModelGameEvent _playerUnreadyEvent;
        [SerializeField] private UnityEvent _playerReadyResponse;
        [SerializeField] private UnityEvent _playerUnreadyResponse;

        private void OnEnable()
        {
            _playerReadyEvent.AddListener(OnPlayerReadyEvent);
            _playerUnreadyEvent.AddListener(OnPlayerUnreadyEvent);
        }

        private void OnDisable()
        {
            _playerReadyEvent.RemoveListener(OnPlayerReadyEvent);
            _playerUnreadyEvent.RemoveListener(OnPlayerUnreadyEvent);
        }

        private void OnPlayerReadyEvent(IPlayerModel player)
        {
            if (player.Index != _playerIndex)
            {
                return;
            }
            
            _playerReadyResponse.Invoke();
        }
        
        private void OnPlayerUnreadyEvent(IPlayerModel player)
        {
            if (player.Index != _playerIndex)
            {
                return;
            }
            
            _playerUnreadyResponse.Invoke();
        }
    }
}