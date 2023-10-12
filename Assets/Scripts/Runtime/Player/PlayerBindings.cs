using UnityEngine;

namespace GorillaGong.Runtime.Player
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player Bindings")]
    public class PlayerBindings : ScriptableObject
    {
        [SerializeField] private string[] _playerBindings = new string[4];

        public string[] GetPlayerBindings => _playerBindings;
    }
}

