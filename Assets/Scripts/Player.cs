using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private InputActionMap _actionMap;

        public event Action<Player, InputAction> OnULpressed;
        public event Action<Player, InputAction> OnURpressed;
        public event Action<Player, InputAction> OnBLpressed;
        public event Action<Player, InputAction> OnBRpressed;

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
            OnULpressed?.Invoke(this, ctx.action);
        }

        public void NotifyURpressed(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " : " + ctx.action);
            OnURpressed?.Invoke(this, ctx.action);
        }

        public void NotifyBLpressed(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " : " + ctx.action);
            OnBLpressed?.Invoke(this, ctx.action);
        }

        public void NotifyBRpressed(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " : " + ctx.action);
            OnBRpressed?.Invoke(this, ctx.action);
        }
    }
}