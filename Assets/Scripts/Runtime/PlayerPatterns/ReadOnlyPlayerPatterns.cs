using GorillaGong.Runtime.Patterns;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.PlayerPatterns
{
    public abstract class ReadOnlyPlayerPatterns : ScriptableObject
    {
        public abstract IReadOnlyReactiveDictionary<int, Pattern> Patterns { get; }
    }
}