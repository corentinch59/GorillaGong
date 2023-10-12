using UnityEngine;

namespace Runtime.GameModes.Config
{
    public abstract class GameModeConfig : ScriptableObject
    {
        [field: SerializeField]
        public int ScoreGain { get; private set; }
        
        [field: SerializeField] 
        public int ScoreLoss { get; private set; }
    }
}