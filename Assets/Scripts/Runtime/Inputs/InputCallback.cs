using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GorillaGong.Runtime.Inputs
{
    public class InputCallback : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inputActionRef;
        [SerializeField] private UnityEvent _response;
        
        private void OnEnable()
        {
            _inputActionRef.action.performed += OnInputActionPerformed;
        }

        private void OnDisable()
        {
            _inputActionRef.action.performed -= OnInputActionPerformed;
        }

        private void OnInputActionPerformed(InputAction.CallbackContext obj)
        {
            _response.Invoke();
        }
    }
}