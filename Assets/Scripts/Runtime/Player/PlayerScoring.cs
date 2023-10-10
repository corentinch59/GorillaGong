using ScriptableObjectArchitecture;
using UniRx;
using UnityEngine;

namespace Game
{
    public partial class Player
    {
        [SerializeField] private FloatVariable _maxScore;
        public IReadOnlyReactiveProperty<float> Score => _score;
        private ReactiveProperty<float> _score = new();

        public void AddScore(float amount)
        {
            _score.Value = Mathf.Min(_maxScore.Value, _score.Value + amount);
        }

        public void RemoveScore(float amount)
        {
            _score.Value = Mathf.Max(0, _score.Value - amount);
        }
    }
}