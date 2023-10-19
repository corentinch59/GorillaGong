using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.RuntimeSets
{
    public abstract class RuntimeSet : ScriptableObject, IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }
    public abstract class RuntimeSet<T> : RuntimeSet
    {
        public abstract IReadOnlyReactiveCollection<T> ReadOnlyCollection { get; }
        public override IEnumerator GetEnumerator() => ReadOnlyCollection.GetEnumerator();
    }

    public abstract class BaseRuntimeSet<T> : RuntimeSet<T>
    {
        public override IReadOnlyReactiveCollection<T> ReadOnlyCollection => ReactiveCollection;

        public ReactiveCollection<T> ReactiveCollection
        {
            get
            {
                if (_reactiveCollection is null)
                {
                    _reactiveCollection = new ReactiveCollection<T>();
                }

                return _reactiveCollection;
            }
        }
        
        [SerializeField]
        protected ReactiveCollection<T> _reactiveCollection;
    }
}