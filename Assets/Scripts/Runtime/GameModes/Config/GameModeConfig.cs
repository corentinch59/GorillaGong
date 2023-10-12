using UnityEngine;

namespace Runtime.GameModes.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameMode Config/Base")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField]
        public int ScoreGain { get; private set; }
        
        [field: SerializeField] 
        public int ScoreLoss { get; private set; }
        
        [field: SerializeField]
        public int DeathTime { get; private set; }
    }
}