using GorillaGong.Runtime.GameModes.Config;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.MainGameMode
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/Config/Main")]
    public class MainGameModeConfig : GameModeConfig
    {
        [field: Header("Main Game Mode Specific")]
        [field: SerializeField]
        public float DeathTime { get; private set; }
        
        [field: SerializeField, Range(0f, 1f)] 
        public float DoubleInputProbability { get; private set; } = 0.25f;
    }
}