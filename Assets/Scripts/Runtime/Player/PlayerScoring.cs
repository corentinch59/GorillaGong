using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.Player
{
    public partial class Player
    {
        [SerializeField, BoxGroup("Configuration")] private FloatVariable _maxScore;
        public IReadOnlyReactiveProperty<float> Score => _score;
        [SerializeField, ReadOnly]
        private ReactiveProperty<float> _score = new();

        public void AddScore(float amount)
        {
            _score.Value = Mathf.Clamp(_score.Value + amount, 0f, _maxScore.Value);
        }
    }
}
