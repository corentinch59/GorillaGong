using GorillaGong.Runtime.Inputs;
using UnityEngine;

namespace GorillaGong.Runtime
{
    public class GorillaAnimator : MonoBehaviour
    {
        [SerializeField] private int _playerIndex;
        [SerializeField] private PlayerInputGameEvent _playerInputEvent;

        [Header("Animator")] 
        [SerializeField] private Animator _animator;
        [SerializeField] private string[] _animatorTriggersName;

        private void OnEnable()
        {
            _playerInputEvent.AddListener(OnPlayerInputEvent);
        }

        private void OnPlayerInputEvent(PlayerInput playerInput)
        {
            if (_playerIndex != playerInput.PlayerModel.Index)
            {
                return;
            }

            foreach (int playerInputId in playerInput.InputIds)
            {
                _animator.SetTrigger(_animatorTriggersName[playerInputId]);
            }
        }
    }
}