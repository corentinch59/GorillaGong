using GorillaGong.Runtime.Player;
using NaughtyAttributes;
using UnityEngine;

namespace GorillaGong.Runtime.UI
{
    public class PlayerRepartitor : MonoBehaviour
    {
        [SerializeField, BoxGroup("UI Element")] private PlayerElement _prefabs;
        [SerializeField, BoxGroup("Dependency")] private ReadOnlyPlayerModelList _players;

        private void Start()
        {
            foreach (var player in _players)
            {
                PlayerElement newElement = Instantiate(_prefabs, transform);
                newElement.Deploy(player);
            }

        }
    }
}
