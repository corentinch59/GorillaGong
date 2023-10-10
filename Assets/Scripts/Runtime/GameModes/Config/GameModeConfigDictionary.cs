using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Runtime.GameModes.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameMode/GameModeConfig Dictionary", order = 0)]
    public class GameModeConfigDictionary : ScriptableObject
    {
        public IReadOnlyDictionary<GameModeType, GameModeConfig> Values => _values;
        [SerializeField] private SerializedDictionary<GameModeType, GameModeConfig> _values;
    }
}