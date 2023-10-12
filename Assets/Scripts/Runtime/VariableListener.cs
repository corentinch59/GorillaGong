using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaGong.Runtime
{
    public class VariableListener<T> : MonoBehaviour
    {
        [SerializeReference] private BaseVariable<T> _variable;
        [SerializeField] private UnityEvent<T> _response;
        private void OnEnable()
        {
            _variable.AddListener(OnVariableValueChanged);
        }

        private void OnDisable()
        {
            _variable.RemoveListener(OnVariableValueChanged);
        }

        private void OnVariableValueChanged()
        {
            _response.Invoke(_variable.Value);
        }
    }
}