using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game
{
    public interface IPlayerModel
    {
        public IReadOnlyReactiveProperty<float> Score { get; }
    }
}
