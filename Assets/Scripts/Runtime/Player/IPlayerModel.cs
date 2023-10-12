using UniRx;

namespace GorillaGong.Runtime.Player
{
    public interface IPlayerModel
    {
        public int Index { get; }
        public IReadOnlyReactiveProperty<float> Score { get; }
    }
}
