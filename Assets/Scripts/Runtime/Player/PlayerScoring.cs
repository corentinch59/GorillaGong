using UniRx;

namespace Game
{
    public partial class Player
    {
        public IReadOnlyReactiveProperty<int> Score => _score;
        private ReactiveProperty<int> _score = new();

        public void AddScore(int amount)
        {
            _score.Value += amount;
        }

        public void RemoveScore(int amount)
        {
            _score.Value -= amount;
        }
    }
}