using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game
{
    public partial class Player : MonoBehaviour, IPlayerModel
    {
        public int Index { get; set; }

        private InputActionMap _actionMap;
        private UnityEvent _onInputTriggered;

        public event Action<Player, int[]> OnInputPressed;

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
            }
        }
        #endregion

        public void NotifyULpressed(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " : " + ctx.action);
            OnInputPressed?.Invoke(this, new []{0});
        }

        public void NotifyURpressed(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " : " + ctx.action);
            OnInputPressed?.Invoke(this, new []{1});
        }

        public void NotifyBLpressed(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " : " + ctx.action);
            OnInputPressed?.Invoke(this, new []{2});
        }

        public void NotifyBRpressed(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " : " + ctx.action);
            OnInputPressed?.Invoke(this, new []{3});
        }
    }
}