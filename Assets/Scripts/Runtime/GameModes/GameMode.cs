﻿using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Patterns;
using GorillaGong.Runtime.Player;

namespace GorillaGong.Runtime.GameModes
{
    public abstract class GameMode : IGameMode
    {
        protected PlayerManager PlayerManager { get; private set; }
        protected PlayerPatterns.PlayerPatterns PlayerPatterns { get; private set; }
        protected PlayerModelGameEvent PlayerSuccessEvent {get; private set;}
        protected PlayerModelGameEvent PlayerFailEvent {get; private set;}

        public abstract GameModeConfig ConfigBase { get; }
        public abstract GameModeType Type { get; }
        public abstract bool IsFinished { get; }
        public abstract bool IsPlaying { get; }
        public abstract Pattern GetPlayerCurrentPattern(Player.Player player);
        public abstract void Start();
        public abstract void Update(float deltaTime);
        public abstract void Stop();
        public abstract void Disable();

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