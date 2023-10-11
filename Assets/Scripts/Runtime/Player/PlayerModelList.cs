using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player Model List")]
    public class PlayerModelList : ReadOnlyPlayerModelList
    {
        public List<IPlayerModel> Players
        {
            get => _players;
            set => _players = value;
        }

        public PlayerModelList(IEnumerable<IPlayerModel> players) : base(players)
        {
        }

        public void ResetValues()
        {
            if (_players is null)
            {
                return;
            }
            
            _players.Clear();
        }
    }
}
