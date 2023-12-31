using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaGong.Runtime.Player
{
    public abstract class ReadOnlyPlayerModelList : ScriptableObject, IEnumerable<IPlayerModel>
    {
        protected List<IPlayerModel> _players;

        public ReadOnlyPlayerModelList(IEnumerable<IPlayerModel> players)
        {
            _players = new List<IPlayerModel>(players);
        }

        public IReadOnlyList<IPlayerModel> PlayerModels => _players.AsReadOnly();

        public IEnumerator<IPlayerModel> GetEnumerator()
        {
            return _players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _players.GetEnumerator();
        }
    }
}
