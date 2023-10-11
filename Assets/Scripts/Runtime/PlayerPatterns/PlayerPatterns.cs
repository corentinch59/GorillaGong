using Runtime.Patterns;
using UniRx;
using UnityEngine;

namespace Runtime.PlayerPatterns
{
    /// <summary>
    /// ScriptableObject containing the current pattern of each player
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Player Patterns")]
    public class PlayerPatterns : ReadOnlyPlayerPatterns
    {
        public override IReadOnlyReactiveDictionary<int, Pattern> Patterns => _patterns;
        public ReactiveDictionary<int, Pattern> Values => _patterns;

        private readonly ReactiveDictionary<int, Pattern> _patterns = new();
    }
}