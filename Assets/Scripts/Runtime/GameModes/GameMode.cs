using System.Collections.Generic;
using Game;
using Runtime.GameEvents;
using Runtime.Patterns;

namespace Runtime.GameModes
{
    public abstract class GameMode : IGameMode
    {
        protected PlayerManager PlayerManager { get; private set; }
        protected PlayerPatterns.PlayerPatterns PlayerPatterns { get; private set; }
        protected PlayerModelGameEvent PlayerSuccessEvent {get; private set;}
        protected PlayerModelGameEvent PlayerFailEvent {get; private set;}

        public abstract bool IsFinished { get; }
        public abstract Pattern GetPlayerCurrentPattern(Player player);
        public abstract void Start();
        public abstract void Update(float deltaTime);
        public abstract void Stop();
        
        #region Setters
        public GameMode SetPlayerManager(PlayerManager value)
        {
            PlayerManager = value;
            return this;
        }

        public GameMode SetPlayerPatterns(PlayerPatterns.PlayerPatterns value)
        {
            PlayerPatterns = value;
            return this;
        }

        public GameMode SetPlayerSuccessEvent(PlayerModelGameEvent value)
        {
            PlayerSuccessEvent = value;
            return this;
        }

        public GameMode SetPlayerFailEvent(PlayerModelGameEvent value)
        {
            PlayerFailEvent = value;
            return this;
        }
        #endregion
    }
}