using UniRx;
using UnityEngine;

namespace Runtime.PlayerPatterns
{
    public abstract class ReadOnlyPlayerPatterns : ScriptableObject
    {
        public abstract IReadOnlyReactiveDictionary<int, int> Patterns { get; }
    }
}