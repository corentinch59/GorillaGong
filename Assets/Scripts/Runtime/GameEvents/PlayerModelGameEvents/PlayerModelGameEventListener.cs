using System;
using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.Player;
using UnityEngine;

namespace GorillaGong.Runtime
{
    public abstract class PlayerModelGameEventListener : MonoBehaviour
    {
        [SerializeField] private PlayerModelGameEvent _gameFinishedEvent;
        
        protected virtual void OnEnable()
        {
            _gameFinishedEvent.AddListener(GameFinished);
        }
        
        private void OnDisable()
        {
            _gameFinishedEvent.RemoveListener(GameFinished);
        }

        protected abstract void GameFinished(IPlayerModel player);
    }
}