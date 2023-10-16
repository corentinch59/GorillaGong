using GorillaGong.Runtime.GameEvents;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Serialization;

namespace GorillaGong.Runtime.GameModes.Config
{
    public abstract class GameModeConfig : ScriptableObject
    {
        [field: Header("Display")]
        [field: SerializeField]
        public string Title { get; private set; }
        
        [field: Header("Events")]
        [field: SerializeField] 
        public GameEvent GameModeStartedEvent { get; private set; }
        
        [field: SerializeField]
        public PlayerModelGameEvent GameModeStoppedEvent { get; private set; }
        
        [field: Header("Score")]
        [field: SerializeField]
        public int WinnerScoreGain { get; private set; }
        
        [field: SerializeField] 
        public int LoserScoreGain { get; private set; }
    }
}