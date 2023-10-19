using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.RuntimeSets;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.SpamGameMode
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/Config/Spam")]
    public class SpamGameModeConfig : GameModeConfig
    {
        [field: Header("Spam Game Mode Specific")]
        [field: SerializeField] public float EventDuration { get; private set; } = 5f;
        [field: SerializeField] public IntRuntimeSet PlayersHitCount { get; private set; }

        [field: Header("Spam Visuals")]
        [field: SerializeField] public float VisualBlinkDuration { get; private set; } = 0.1f;
    }
}