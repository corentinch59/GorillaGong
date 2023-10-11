using Runtime.Patterns;
using UniRx;
using UnityEngine;

namespace Runtime.PlayerPatterns
{
    public abstract class ReadOnlyPlayerPatterns : ScriptableObject
    {
        public abstract IReadOnlyReactiveDictionary<int, Pattern> Patterns { get; }
    }
}