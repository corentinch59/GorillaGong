using GorillaGong.Runtime.GameModes.Config;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.SpamGameMode
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/Config/Spam")]
    public class SpamGameModeConfig : GameModeConfig
    {
        [field: Header("Spam Game Mode Specific")]
        [field: SerializeField] public float StartDelayDuration { get; private set; } = 3f;
        [field: SerializeField] public float EventDuration { get; private set; } = 5f;
    }
}