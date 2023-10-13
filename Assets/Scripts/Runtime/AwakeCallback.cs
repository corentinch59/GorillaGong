using UnityEngine;
using UnityEngine.Events;

namespace GorillaGong.Runtime
{
    public class AwakeCallback : MonoBehaviour
    {
        [SerializeField] private UnityEvent _awakeResponse;

        private void Awake()
        {
            _awakeResponse.Invoke();
        }
    }
}