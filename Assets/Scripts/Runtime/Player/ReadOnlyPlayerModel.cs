using UniRx;

namespace GorillaGong.Runtime.Player
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