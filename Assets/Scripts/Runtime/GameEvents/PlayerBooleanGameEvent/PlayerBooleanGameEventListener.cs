using System;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaGong.Runtime.GameEvents
{
    public class PlayerBooleanGameEventListener : MonoBehaviour
    {
        [SerializeField] private int _playerIndex;
        [SerializeField] private PlayerBooleanGameEvent _event;

        [SerializeField] private UnityEvent _trueValueResponse;
        [SerializeField] private UnityEvent _falseValueResponse;

        private void OnEnable()
        {
            _event.AddListener(OnEvent);
        }

        private void OnDisable()
        {
            _event.RemoveListener(OnEvent);
        }

        private void OnEvent(PlayerBoolean obj)
        {
            if (obj.player.Index != _playerIndex)
            {
                return;
            }

            if (obj.value)
            {
                _trueValueResponse.Invoke();
            }
            else
            {
                _falseValueResponse.Invoke();
            }
        }
    }
}