using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GorillaGong.Runtime.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField, BoxGroup("Dependencies")] private InputActionAsset _actionAsset;
        [SerializeField, BoxGroup("Dependencies")] private List<PlayerBindings> _keyBindings = new ();

        [SerializeField, BoxGroup("Bindings")] private PlayerModelList _listToInitialize;

        [SerializeField, BoxGroup("Temporary Dependencies")] private List<Player> _players = new ();

        public IReadOnlyList<Player> GetPlayers()
        {
            return _players;
        }
        public int PlayersCount() => _players.Count;

        private void Awake()
        {
            _listToInitialize.ResetValues();
            _listToInitialize.Players = _players.Cast<IPlayerModel>().ToList();

            for (var i = 0; i < _players.Count; i++)
            {
                _players[i].Index = i;
            }
        }

        private void Start()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                InputActionMap map = CloneActionMap("Player");

                for (int j = 0; j < _keyBindings[i].GetPlayerBindings.Length; j++)
                {
                    InputBinding binding = map.actions[j].bindings[0];
                    binding.overridePath = "<Keyboard>/" + _keyBindings[i].GetPlayerBindings[j];
                    map.actions[j].ChangeBinding(0).WithPath(binding.overridePath);
                }


                _players[i].ActionMap = map;
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
