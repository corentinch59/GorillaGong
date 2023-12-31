﻿using GorillaGong.Runtime.Patterns;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.PlayerPatterns
{
    /// <summary>
    /// ScriptableObject containing the current pattern of each player
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Player Patterns")]
    public class PlayerPatterns : ReadOnlyPlayerPatterns
    {
        public override IReadOnlyReactiveDictionary<int, Pattern> Patterns => _patterns;
        public ReactiveDictionary<int, Pattern> Values => _patterns;

        private ReactiveDictionary<int, Pattern> _patterns = new();

        public void ResetValues()
        {
            _patterns.Clear();
        }
    }
}