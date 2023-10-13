using GorillaGong.Runtime.GameModes.Config;
using UnityEngine;

namespace Runtime.GameModes.RhythGameMode
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/Config/Rhythm")]
    public class RhythmGameModeConfig : GameModeConfig
    {
        [field: Header("Rhythm Game Mode Specific")]
        [field: SerializeField] public float TimeBeforeSuccessInput { get; private set; }
    }
}
