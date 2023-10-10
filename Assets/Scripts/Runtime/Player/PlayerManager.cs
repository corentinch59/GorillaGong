using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _actionAsset;

        [SerializeField] private List<Player> m_Players = new ();
        [SerializeField] private List<PlayerBindings> m_Bindings = new ();

        public IReadOnlyList<Player> GetPlayers()
        {
            return m_Players;
        }

        private void Start()
        {
            for (int i = 0; i < m_Players.Count; i++)
            {
                InputActionMap map = CloneActionMap("Player");

                for (int j = 0; j < m_Bindings[i].GetPlayerBindings.Length; j++)
                {
                    InputBinding binding = map.actions[j].bindings[0];
                    binding.overridePath = "<Keyboard>/" + m_Bindings[i].GetPlayerBindings[j];
                    map.actions[j].ChangeBinding(0).WithPath(binding.overridePath);
                }


                m_Players[i].ActionMap = map;
            }
        }

        private InputActionMap CloneActionMap(string mapName)
        {
            InputActionMap originalMap = _actionAsset.FindActionMap(mapName);
            if (originalMap == null) return null;

            InputActionMap clonedMap = new InputActionMap(name: originalMap.name);

            foreach (var action in originalMap)
            {
                InputAction clonedAction = clonedMap.AddAction(name: action.name, binding: action.bindings[0].path, interactions: action.interactions, processors: action.processors);

                for (int i = 1; i < action.bindings.Count; i++)
                {
                    clonedAction.AddBinding(action.bindings[i].path, interactions: action.bindings[i].interactions, groups: action.bindings[i].groups);
                }
            }

            return clonedMap;
        }

    }
}