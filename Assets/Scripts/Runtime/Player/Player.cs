using System;
using System.Collections.Generic;
using GorillaGong.Runtime.Inputs;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GorillaGong.Runtime.Player
{
    public partial class Player : MonoBehaviour, IPlayerModel
    {
        public int Index { get; set; }

        [Header("Inputs")] 
        [SerializeField] private PlayerInputGameEvent _playerInputEvent;
        [SerializeField] private float _doubleInputToleranceInSeconds = 0.1f;
        [SerializeField] private GameEvent _gameRestartInputPressed;
        private List<int> _currentInputs = new List<int>();
        private float _toleranceTimer;

        private InputActionMap _actionMap;
        private UnityEvent _onInputTriggered;

        public event Action<Player, int[]> OnInputPressed;

        private void Update()
        {
            if (_currentInputs.Count == 0)
            {
                return;
            }
            
            _toleranceTimer -= Time.deltaTime;
            if (_toleranceTimer > 0)
            {
                return;
            }
            
            SendInputPressedEvent();
        }

        private void OnDisable()
        {
            if (ActionMap == null)
            {
                return;
            }
            
            _actionMap.actions[0].started -= NotifyULpressed;
            _actionMap.actions[1].started -= NotifyURpressed;
            _actionMap.actions[2].started -= NotifyBLpressed;
            _actionMap.actions[3].started -= NotifyBRpressed;
            _actionMap.actions[4].started -= NotifyGameRestartPressed;
        }

        private void NotifyGameRestartPressed(InputAction.CallbackContext obj)
        {
            _gameRestartInputPressed.Raise();
        }

        public event UnityAction OnInputTriggered
        {
            add => _onInputTriggered.AddListener(value);
            remove => _onInputTriggered.RemoveListener(value);
        }

        #region PROPERTIES
        public InputActionMap ActionMap
        {
            get => _actionMap;
            set
            {
                _actionMap = value;
                _actionMap.Enable();

                if (_actionMap.actions.Count < 4)
                    throw new ArgumentException("Not Enough actions in Action map");

                _actionMap.actions[0].started += NotifyULpressed;
                _actionMap.actions[1].started += NotifyURpressed;
                _actionMap.actions[2].started += NotifyBLpressed;
                _actionMap.actions[3].started += NotifyBRpressed;
                _actionMap.actions[4].started += NotifyGameRestartPressed;
            }
        }
        #endregion

        public void NotifyULpressed(InputAction.CallbackContext ctx) => QueueInput(0);
        public void NotifyURpressed(InputAction.CallbackContext ctx) => QueueInput(1);
        public void NotifyBLpressed(InputAction.CallbackContext ctx) => QueueInput(2);
        public void NotifyBRpressed(InputAction.CallbackContext ctx) => QueueInput(3);

        private void QueueInput(int inputId)
        {
            _currentInputs.Add(inputId);

            if (_currentInputs.Count == 2)
            {
                SendInputPressedEvent();
            }
            _toleranceTimer = _doubleInputToleranceInSeconds;
        }

        private void SendInputPressedEvent()
        {
            OnInputPressed?.Invoke(this, _currentInputs.ToArray());
            _playerInputEvent.Raise(new GorillaGong.Runtime.Inputs.PlayerInput(this, _currentInputs.ToArray()));
            // Debug.Log($"Inputs pressed: {string.Join(", ", _currentInputs)} // {_doubleInputToleranceInSeconds - _toleranceTimer}");
            _currentInputs.Clear();
        }
        
        
    }
}