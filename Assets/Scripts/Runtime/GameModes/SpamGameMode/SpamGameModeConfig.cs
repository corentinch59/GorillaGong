using Runtime.GameModes.Config;
using UnityEngine;

namespace Runtime.GameModes.SpamGameMode
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/Config/Spam")]
    public class SpamGameModeConfig : GameModeConfig
    {
        [field: Header("Spam Game Mode Specific")]
        [field: SerializeField] public float TotalDuration { get; private set; } = 5f;
    }
}