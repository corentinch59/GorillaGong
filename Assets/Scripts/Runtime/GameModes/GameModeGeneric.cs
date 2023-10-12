using GorillaGong.Runtime.GameModes.Config;

namespace GorillaGong.Runtime.GameModes
{
    public abstract class GameMode<T> : GameMode where T : GameModeConfig
    {
        protected readonly T Config;

        protected GameMode(T gameModeConfig)
        {
            Config = gameModeConfig;
        }
        
        public override void Start()
        {
            foreach (Player.Player player in PlayerManager.GetPlayers())
            {
                player.OnInputPressed += OnPlayerInputPressed;
                PlayerPatterns.Values.Add(player.Index, GetPlayerCurrentPattern(player));
            }
            
            Config.GameModeStartedEvent.Raise();
        }

        public override void Stop()
        {
            foreach (Player.Player player in PlayerManager.GetPlayers())
            {
                player.OnInputPressed -= OnPlayerInputPressed;
            }
        }

        protected virtual void OnPlayerInputPressed(Player.Player player, int[] inputsIds)
        {
            bool rightInputPressed = GetPlayerCurrentPattern(player).IsInputValid(inputsIds);
            if (rightInputPressed)
            {
                OnPlayerSuccess(player);
            }
            else
            {
                OnPlayerFailed(player);
            }
        }

        protected virtual void OnPlayerSuccess(Player.Player player)
        {
            player.AddScore(Config.ScoreGain);
            PlayerSuccessEvent.Raise(player);
        }

        protected virtual void OnPlayerFailed(Player.Player player)
        {
            player.RemoveScore(Config.ScoreLoss);
            PlayerFailEvent.Raise(player);
        }

        protected  int GenerateRandomInput() => UnityEngine.Random.Range(0, 4);
    }
}