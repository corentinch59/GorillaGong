using GorillaGong.Runtime.Player;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace GorillaGong.Runtime.Inputs
{
    public struct PlayerInput
    {
        public IPlayerModel PlayerModel;
        public int[] InputIds;

        public PlayerInput(IPlayerModel playerModel, int[] inputIds)
        {
            PlayerModel = playerModel;
            InputIds = inputIds;
        }
    }
    [CreateAssetMenu(menuName = "Game Events/PlayerInput")]
    public class PlayerInputGameEvent : GameEventBase<PlayerInput>
    {
        
    }
}