using System.Collections.Generic;
using Runtime.Patterns;

namespace Runtime.GameModes
{
    public interface IGameMode
    {
        public IReadOnlyList<Pattern> Patterns { get; }
        public void Start();
        public void Update(float deltaTime);
        public void Stop();
    }
}