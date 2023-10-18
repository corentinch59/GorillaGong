using GorillaGong.Runtime.GameModes.Config;

namespace GorillaGong.Runtime.GameModes
{
    public abstract class GameMode<T> : GameMode where T : GameModeConfig
    {
        public override GameModeConfig ConfigBase => _config;
        protected T _config { get; private set; }
        public override bool IsPlaying => !IsFinished && _isPlaying;
        protected bool _isPlaying;

        protected GameMode(T gameModeConfig)
        {
            _config = gameModeConfig;
        }
        
        public override void Start()
        {
            foreach (Player.Player player in PlayerManager.GetPlayers())
            {
                player.OnInputPressed += OnPlayerInputPressed;
                PlayerPatterns.Values[player.Index] = GetPlayerCurrentPattern(player);
            }
            
            _config.GameModeStartedEvent.Raise();

            _isPlaying = true;
        }

        public override void Stop() => Disable();
        public override void Disable()
        {
            foreach (Player.Player player in PlayerManager.GetPlayers())
            {
                player.OnInputPressed -= OnPlayerInputPressed;
                PlayerPatterns.Values[player.Index] = null;
            }

            _isPlaying = false;
        }


        protected virtual void OnPlayerInputPressed(Player.Player player, int[] inputsIds)
        {
            bool rightInputPressed = IsRightInputPressed(player, inputsIds);
            if (rightInputPressed)
            {
                OnPlayerSuccess(player);
            }
            else
            {
                OnPlayerFailed(player);
            }
        }
        protected virtual bool IsRightInputPressed(Player.Player player, int[] inputsIds) => GetPlayerCurrentPattern(player).IsInputValid(inputsIds);

        protected virtual void OnPlayerSuccess(Player.Player player)
        {
            player.AddScore(_config.WinnerScoreGain);
            PlayerSuccessEvent.Raise(player);
        }

        protected virtual void OnPlayerFailed(Player.Player player)
        {
            player.AddScore(_config.LoserScoreGain);
            PlayerFailEvent.Raise(player);
        }

        protected  int GenerateRandomInput() => UnityEngine.Random.Range(0, 4);
    }
}