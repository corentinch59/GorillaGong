using System.Collections.Generic;
using Game;
using Runtime.Patterns;

namespace Runtime.GameModes
{
    public interface IGameMode
    {
        public bool IsFinished { get; }
        
        public Pattern GetPlayerCurrentPattern(Player player);
        public void Start();
        public void Update(float deltaTime);
        public void Stop();
    }
}