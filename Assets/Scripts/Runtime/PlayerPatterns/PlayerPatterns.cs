using UniRx;
using UnityEngine;

namespace Runtime.PlayerPatterns
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player Patterns")]
    public class PlayerPatterns : ReadOnlyPlayerPatterns
    {
        public override IReadOnlyReactiveDictionary<int, int> Patterns => _patterns;
        public ReactiveDictionary<int, int> Values => _patterns;

        private readonly ReactiveDictionary<int, int> _patterns = new();
    }
}