using GorillaGong.Runtime.Player;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace GorillaGong.Runtime.GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Player Boolean")]
    public class PlayerBooleanGameEvent : GameEventBase<PlayerBoolean> { }
    public struct PlayerBoolean
    {
        public IPlayerModel player;
        public bool value;

        public PlayerBoolean(IPlayerModel player, bool value)
        {
            this.player = player;
            this.value = value;
        }
    }
}