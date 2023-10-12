using System;
using Runtime.GameEvents;
using Runtime.GameModes.Config;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Runtime.GameModes
{
    public class GameModesManager : MonoBehaviour
    {
        [SerializeField] private GameModeConfigDictionary _gameModesDictionary;
        [SerializeField] private GameEvent _anyGameModeStarted;
        [SerializeField] private PlayerModelGameEvent _anyGameModeEnded;

        private void OnEnable()
        {
            foreach (GameModeConfig gameModeConfig in _gameModesDictionary.Values.Values)
            {
                gameModeConfig.GameModeStartedEvent.AddListener(_anyGameModeStarted.Raise);
                gameModeConfig.GameModeStoppedEvent.AddListener(_anyGameModeEnded.Raise);
            }
        }

        private void OnDestroy()
        {
            foreach (GameModeConfig gameModeConfig in _gameModesDictionary.Values.Values)
            {
                gameModeConfig.GameModeStartedEvent.RemoveListener(_anyGameModeStarted.Raise);
                gameModeConfig.GameModeStoppedEvent.RemoveListener(_anyGameModeEnded.Raise);
            }
        }
    }
}