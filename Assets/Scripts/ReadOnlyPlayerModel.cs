using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game
{
    public abstract class ReadOnlyPlayerModel
    {
        public abstract ReactiveProperty<int> Score { get; }

        protected ReadOnlyPlayerModel(int score)
        {
            Score.Value = score;
        }
    }
}