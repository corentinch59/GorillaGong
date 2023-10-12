using System.Collections.Generic;
using UnityEngine;

namespace GorillaGong.Runtime.Player
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
