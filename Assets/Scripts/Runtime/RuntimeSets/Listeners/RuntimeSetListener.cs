using System;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.RuntimeSets.Listeners
{
    public abstract class RuntimeSetListener<T> : MonoBehaviour
    {
        [field: SerializeField] protected RuntimeSet<T> RuntimeSet { get; private set; }
        private CompositeDisposable _disposables;
        
        private void OnEnable()
        {
            _disposables = new CompositeDisposable();
            _disposables.Add(
            RuntimeSet.ReadOnlyCollection.ObserveReplace().Subscribe(OnValueChanged)
            );
            _disposables.Add(
                RuntimeSet.ReadOnlyCollection.ObserveAdd().Subscribe(OnValueAdded)
            );            
            _disposables.Add(
                RuntimeSet.ReadOnlyCollection.ObserveRemove().Subscribe(OnValueRemoved)
            );
        }

        private void OnDisable()
        {
            _disposables.Dispose();
            _disposables = null;
        }

        protected virtual void OnValueRemoved(CollectionRemoveEvent<T> collectionRemoveEvent)
        {
        }

        protected virtual void OnValueAdded(CollectionAddEvent<T> collectionAddEvent)
        {
        }

        protected virtual void OnValueChanged(CollectionReplaceEvent<T> collectionReplaceEvent)
        {
            
        }
    }
}