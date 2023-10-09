using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game
{
    public abstract class ReadOnlyPlayerModel
    {
        protected ReactiveProperty<int> Score = new();

        public ReactiveProperty<int> GetScore => Score;

        protected ReadOnlyPlayerModel(int score)
        {
            Score.Value = score;
        }
    }
}