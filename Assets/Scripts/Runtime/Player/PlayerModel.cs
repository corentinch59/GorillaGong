using UniRx;

namespace GorillaGong.Runtime.Player
{
    public class PlayerModel : ReadOnlyPlayerModel
    {
        private ReactiveProperty<int> _score = new ();
        public PlayerModel(int score) : base(score)
        {
            _score.Value = score;
        }

        public override ReactiveProperty<int> Score => _score;
    }
}

