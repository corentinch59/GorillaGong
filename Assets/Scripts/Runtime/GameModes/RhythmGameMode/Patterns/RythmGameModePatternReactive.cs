using UnityEngine;
using UniRx;

namespace GorillaGong.Runtime.Patterns
{
    public abstract class RythmGameModePatternReactive : MonoBehaviour
    {
        [field: SerializeField] protected int PlayerIndex { get; private set; }
        [field: SerializeField] protected ReadOnlyRythmGameModePatternsVariable PatternVariable { get; private set; }
        private CompositeDisposable _disposables;

        private void OnEnable()
        {
            _disposables = new CompositeDisposable();
            _disposables.Add(
            PatternVariable.ReadOnlyPatterns[PlayerIndex].ObserveAdd().Subscribe(OnNewPatternAdded)
            );
            _disposables.Add(
                PatternVariable.ReadOnlyPatterns[PlayerIndex].ObserveRemove().Subscribe(OnPatternRemoved)    
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