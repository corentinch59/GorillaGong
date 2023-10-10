using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerModelList : ScriptableObject
    {
        private List<PlayerModel> _mPlayers = new();

        public List<PlayerModel> Players => _mPlayers;
    }
}

