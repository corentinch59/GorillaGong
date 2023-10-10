using UnityEngine;

namespace Runtime.GameModes.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Simple GameMode Config")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField]
        public int ScoreGain { get; private set; }
        
        [field: SerializeField] 
        public int ScoreLoss { get; private set; }
    }
}