﻿using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GorillaGong.Runtime.GameModes.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModes/GameModeConfig Dictionary", order = 0)]
    public class GameModeConfigDictionary : ScriptableObject
    {
        public IReadOnlyDictionary<GameModeType, GameModeConfig> Values => _values;
        [SerializeField] private SerializedDictionary<GameModeType, GameModeConfig> _values;
    }
}