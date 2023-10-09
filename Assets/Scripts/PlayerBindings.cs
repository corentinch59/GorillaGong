using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "PlayerBindings")]
    public class PlayerBindings : ScriptableObject
    {
        [SerializeField] private string[] _playerBindings = new string[4];

        public string[] GetPlayerBindings => _playerBindings;
    }
}

