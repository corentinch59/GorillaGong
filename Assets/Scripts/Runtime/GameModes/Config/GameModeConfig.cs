using Runtime.GameEvents;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Runtime.GameModes.Config
{
    public abstract class GameModeConfig : ScriptableObject
    {
        [field: Header("Events")]
        [field: SerializeField] 
        public GameEvent GameModeStartedEvent { get; private set; }
        
        [field: SerializeField]
        public PlayerModelGameEvent GameModeStoppedEvent { get; private set; }
        
        [field: Header("Score")]
        [field: SerializeField]
        public int ScoreGain { get; private set; }
        
        [field: SerializeField] 
        public int ScoreLoss { get; private set; }
    }
}