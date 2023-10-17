using System.Collections.Generic;
using System.Linq;
using GorillaGong.Runtime.GameModes;
using GorillaGong.Runtime.Patterns;
using GorillaGong.Runtime.Player;
using UnityEngine.Assertions.Must;

namespace Runtime.GameModes.RhythGameMode
{
    public class RhythmGameMode : GameMode<RhythmGameModeConfig>
    {
        public override GameModeType Type => GameModeType.Rhythm;
        public override bool IsFinished => _isFinished;
        private bool _isFinished;

        private int[] _playersScore;

        public RhythmGameMode(RhythmGameModeConfig gameModeConfig) : base(gameModeConfig)
        {
        }

        public override void Start()
        {
            base.Start();
            _playersScore = new int[PlayerManager.PlayersCount()];
        }

        public override void Stop()
        {
            IEnumerable<int> winnersIndex = _playersScore.Select((value, index) => (value, index))
                .Where(tuple => tuple.value == _playersScore.Max())
                .Select(tuple => tuple.index);
            IReadOnlyList<Player> players = PlayerManager.GetPlayers(); 
            for (int i = 0; i < _playersScore.Length; i++)
            {
                bool isWinner = winnersIndex.Contains(i);
                Player player = players[i];
                player.AddScore(isWinner ? _config.WinnerScoreGain : _config.LoserScoreGain);    
                if (isWinner)
                {
                    _config.GameModeStoppedEvent.Raise(player);
                }
            }
            
            base.Stop();
        }

        public override void Update(float deltaTime)
        {
            for (int i = 0; i < _config.RythmGameModePatterns.Patterns.Count; i++)
            {
                RythmGameModePattern rythmGameModePattern = _config.RythmGameModePatterns.Patterns[i];
                rythmGameModePattern.DecreaseDuration(deltaTime);

                if (rythmGameModePattern.CurrentState.Value is RythmGameModePattern.State.NotValid &&
                    rythmGameModePattern.Duration <= rythmGameModePattern.ValidityDuration)
                {
                    rythmGameModePattern.CurrentState.Value = RythmGameModePattern.State.Valid;
                }
                
                if (rythmGameModePattern.Duration < 0)
                {
                    _config.RythmGameModePatterns.Patterns.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }

        protected override void OnPlayerInputPressed(Player player, int[] inputsIds)
        {
            if (inputsIds.Length > 1)
            {
                OnPlayerFailed(player);
                return;
            }

            RythmGameModePattern correspondingPattern = null;
            foreach (RythmGameModePattern rythmGameModePattern in _config.RythmGameModePatterns.Patterns)
            {
                if (!rythmGameModePattern.Pattern.IsInputValid(inputsIds))
                {
                    continue;
                }
                
                correspondingPattern = rythmGameModePattern;
                break;
            }

            if (correspondingPattern is null)
            {
                OnPlayerFailed(player);
                return;
            }
            
            // correspondingPattern is not null
            if (correspondingPattern.Duration <= correspondingPattern.ValidityDuration)
            {
                OnPlayerSuccess(player);
            }
            else
            {
                OnPlayerFailed(player);
            }
            _config.RythmGameModePatterns.Patterns.Remove(correspondingPattern);
        }

        protected override void OnPlayerSuccess(Player player)
        {
            PlayerSuccessEvent.Raise(player);
            _playersScore[player.Index]++;
        }

        protected override void OnPlayerFailed(Player player)
        {
            PlayerFailEvent.Raise(player);
        }

        public override Pattern GetPlayerCurrentPattern(Player player) => null;
    }
}

