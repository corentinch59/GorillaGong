using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerModelList : ReadOnlyPlayerModelList
    {
        private List<IPlayerModel> _mPlayers = new();

        public List<IPlayerModel> Players => _mPlayers;
        public override IReadOnlyList<IPlayerModel> PlayerModels => _mPlayers;
    }
}

