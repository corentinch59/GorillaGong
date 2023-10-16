using GorillaGong.Runtime.PlayerPatterns;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.Patterns
{
    public abstract class PlayerPatternsReactive : MonoBehaviour
    {
        [field: SerializeField] protected ReadOnlyPlayerPatterns PlayerPatterns { get; private set; }
        [field: SerializeField] protected int PlayerIndex { get; private set; }

        private CompositeDisposable _disposables;
        
        private void OnEnable()
        {
            _disposables = new CompositeDisposable();
                
            _disposables.Add(
                PlayerPatterns.Patterns.ObserveAdd().Subscribe(OnPlayerPatternCreated)    
            );
            _disposables.Add(
                PlayerPatterns.Patterns.ObserveReplace().Subscribe(OnPlayerPatternReplaced)
            );
        }
        
        private void OnDisable()
        {
            _disposables?.Dispose();
            _disposables = null;
        }

        private void OnPlayerPatternCreated(DictionaryAddEvent<int, Pattern> obj) => OnPlayerPatternReplaced(obj.Key, null, obj.Value);
        private void OnPlayerPatternReplaced(DictionaryReplaceEvent<int, Pattern> obj) => OnPlayerPatternReplaced(obj.Key, obj.OldValue, obj.NewValue);
        protected abstract void OnPlayerPatternReplaced(int key, Pattern oldValue, Pattern newValue);
    }
}