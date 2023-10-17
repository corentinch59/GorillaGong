using UnityEngine;
using UniRx;

namespace GorillaGong.Runtime.Patterns
{
    public abstract class RythmGameModePatternReactive : MonoBehaviour
    {
        [field: SerializeField] protected ReadOnlyRythmGameModePatternsVariable PatternVariable { get; private set; }
        private CompositeDisposable _disposables;

        private void OnEnable()
        {
            _disposables = new CompositeDisposable();
            _disposables.Add(
            PatternVariable.ReadOnlyPatterns.ObserveAdd().Subscribe(OnNewPatternAdded)
            );
            _disposables.Add(
                PatternVariable.ReadOnlyPatterns.ObserveRemove().Subscribe(OnPatternRemoved)    
            );
        }

        private void OnDisable()
        {
            _disposables.Dispose();
            _disposables = null;
        }

        protected abstract void OnPatternRemoved(CollectionRemoveEvent<RythmGameModePattern> patternRemovedEvent);
        protected abstract void OnNewPatternAdded(CollectionAddEvent<RythmGameModePattern> patternAddedEvent);
    }
}