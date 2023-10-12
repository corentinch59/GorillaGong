using UnityEngine;

namespace Runtime.GameModes.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/Config/Main")]
    public class MainGameModeConfig : GameModeConfig
    {
        [field: SerializeField]
        public int DeathTime { get; private set; }
        
        [field: SerializeField, Range(0f, 1f)] 
        public float DoubleInputProbability { get; private set; } = 0.25f;
    }
}