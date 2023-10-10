using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
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
