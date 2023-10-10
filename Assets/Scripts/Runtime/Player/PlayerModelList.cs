using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player Model List")]
    public class PlayerModelList : ReadOnlyPlayerModelList
    {
        private List<IPlayerModel> _mPlayers = new();

        public List<IPlayerModel> Players
        {
            get => _mPlayers;
            set => _mPlayers = value;
        }

        public PlayerModelList(IEnumerable<IPlayerModel> players) : base(players)
        {
        }
    }
}
