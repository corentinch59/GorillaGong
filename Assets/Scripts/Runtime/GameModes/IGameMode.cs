using GorillaGong.Runtime.GameModes.Config;

namespace GorillaGong.Runtime.GameModes
{
    public interface IGameMode
    {
        /// <summary>
        /// Config of the GameMode
        /// </summary>
        public GameModeConfig ConfigBase { get; }
        
        /// <summary>
        /// Type of the GameMode
        /// </summary>
        public GameModeType Type { get; }
        
        /// <summary>
        /// Has the GameMode been started and finished
        /// </summary>
        public bool IsFinished { get; }
        
        /// <summary>
        /// Is the GameMode currently playing
        /// </summary>
        public bool IsPlaying { get; }
        
        /// <summary>
        /// Starts the GameMode
        /// </summary>
        public void Start();
        
        /// <summary>
        /// Update of the GameMode when playing
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime);
        
        /// <summary>
        /// Stops the GameMode and selects the winner(s)
        /// </summary>
        public void Stop();
        
        /// <summary>
        /// Stops the GameMode WITHOUT selecting the winner(s)
        /// </summary>
        public void Disable();
    }
}