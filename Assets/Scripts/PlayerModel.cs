using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game
{
    public class PlayerModel : ReadOnlyPlayerModel
    {
        public PlayerModel(int score) : base(score)
        {

        }

        public ReactiveProperty<int> SetScore { set => Score = value; }
    }
}

