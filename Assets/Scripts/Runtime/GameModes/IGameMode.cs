using System.Collections.Generic;

namespace Runtime.GameModes
{
    public interface IGameMode
    {
        public IReadOnlyList<int> Patterns { get; }
        public void Start();
        public void Stop();
    }
}