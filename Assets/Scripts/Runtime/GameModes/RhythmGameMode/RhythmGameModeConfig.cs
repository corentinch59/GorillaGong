using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Patterns;
using UnityEngine;

namespace Runtime.GameModes.RhythGameMode
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/Config/Rhythm")]
    public class RhythmGameModeConfig : GameModeConfig
    {
        [field: Header("Rhythm Game Mode Specific")]
        [field: SerializeField] public RythmGameModePatternsVariable RythmGameModePatterns { get; private set; }
        [field: SerializeField] public float TimeBeforeSuccessInput { get; private set; }

        [field: Space] 
        [field: SerializeField] public float GameModeDuration { get; private set; }
        [field: SerializeField] public Vector2 QteSpawnIntervalInSecondsMinMax { get; private set; }
    }
}
