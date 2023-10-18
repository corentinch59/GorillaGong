using GorillaGong.Runtime.Player;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaGong.Runtime
{
    public class SpecificPlayerModelGameEventLister : PlayerModelGameEventListener
    {
        [SerializeField] private int _playerIndex;
        [SerializeField] private UnityEvent _winResponse;
        [SerializeField] private UnityEvent _loseResponse;

        protected override void GameFinished(IPlayerModel player)
        {
            if (player.Index == _playerIndex)
            {
                _winResponse.Invoke();
            }
            else
            {
                _loseResponse.Invoke();
            }
        }
    }
}