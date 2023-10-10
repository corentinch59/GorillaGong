using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game
{
    public interface IPlayerModel
    {
        public int Index { get; }
        public IReadOnlyReactiveProperty<float> Score { get; }
    }
}
