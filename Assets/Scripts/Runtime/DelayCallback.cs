using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GorillaGong.Runtime
{
    public class DelayCallback : MonoBehaviour
    {
        [SerializeField] private float _delayDuration;

        [SerializeField] private UnityEvent _response;
        
        private Coroutine _delayCoroutine;
        
        private void OnEnable()
        {
            _delayCoroutine = StartCoroutine(Raise(_delayDuration));
        }

        private void OnDisable()
        {
            StopCoroutine(_delayCoroutine);
        }


        private IEnumerator Raise(float delay)
        {
            yield return new WaitForSeconds(delay);
            Raise();
        }

        private void Raise()
        {
            _response.Invoke();
        }
    }
}