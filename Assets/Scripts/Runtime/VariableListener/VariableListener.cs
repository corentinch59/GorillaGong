using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaGong.Runtime
{
    public class VariableListener<T> : MonoBehaviour
    {
        [SerializeReference] protected BaseVariable<T> _variable;
        [SerializeField] private UnityEvent<T> _response;
        private void OnEnable()
        {
            _variable.AddListener(OnVariableValueChanged);
        }

        private void OnDisable()
        {
            _variable.RemoveListener(OnVariableValueChanged);
        }

        public virtual void OnVariableValueChanged()
        {
            _response.Invoke(_variable.Value);
        }
    }
}