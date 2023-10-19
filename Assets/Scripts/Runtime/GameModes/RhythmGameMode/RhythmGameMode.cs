using System;
using System.Collections.Generic;
using System.Linq;
using GorillaGong.Runtime.GameModes;
using GorillaGong.Runtime.GameModes.Config;
using GorillaGong.Runtime.Patterns;
using GorillaGong.Runtime.Player;
using UniRx;
using UnityEngine.Assertions.Must;

namespace Runtime.GameModes.RhythGameMode
{
    public class RhythmGameMode : GameMode<RhythmGameModeConfig>
    {
        public override GameModeType Type => GameModeType.Rhythm;
        public override bool IsFinished => _isFinished;
        private bool _isFinished;

        private int[] _playersScore;
        private List<RythmGameModePattern> _instantiatedPatterns = new ();

        private float _spawnCooldown;
        
        public RhythmGameMode(GameModeConfig gameModeConfig) 
            : base(gameModeConfig as RhythmGameModeConfig)
        {
        }

        public override void Start()
        {
            base.Start();
            int playersCount = PlayerManager.PlayersCount();
            _playersScore = new int[playersCount];

            _config.RythmGameModePatterns.Patterns = new ReactiveCollection<RythmGameModePattern>[playersCount];
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
            
            _config.RythmGameModePatterns.Clear();
            _instantiatedPatterns.Clear();

            base.Stop();
        }

        public override void Disable()
        {
            base.Disable();
            _config.RythmGameModePatterns.Clear();
        }

        public override void Update(float deltaTime)
        {
            // TODO: spawns patterns
            PatternsSpawnUpdate();

            // Handle spawned patterns destruction and validity
            for (int i = 0; i < _instantiatedPatterns.Count; i++)
            {
                RythmGameModePattern rythmGameModePattern = _instantiatedPatterns[i];
                rythmGameModePattern.DecreaseDuration(deltaTime);

                if (rythmGameModePattern.CurrentState.Value is RythmGameModePattern.State.NotValid &&
                    rythmGameModePattern.Duration <= rythmGameModePattern.ValidityDuration)
                {
                    rythmGameModePattern.CurrentState.Value = RythmGameModePattern.State.Valid;
                }
                
                if (rythmGameModePattern.Duration < 0)
                {
                    // Remove pattern from all lists
                    foreach (ReactiveCollection<RythmGameModePattern> reactivePatternCollection in _config.RythmGameModePatterns.Patterns)
                    {
                        reactivePatternCollection.Remove(rythmGameModePattern);
                    }
                    
                    _instantiatedPatterns.RemoveAt(i);
                    i--;
                    
                    continue;
                }
            }
        }

        private void PatternsSpawnUpdate(in float deltaTime)
        {
            _spawnCooldown -= deltaTime;
            if (_spawnCooldown < 0)
            {
                return;
            }
            
            _spawnCooldown = 

            return;
        }

        private float GetSpawnCooldown()
        {
            
        }

        protected override void OnPlayerInputPressed(Player player, int[] inputsIds)
        {
            if (inputsIds.Length > 1)
            {
                OnPlayerFailed(player);
                return;
            }

            RythmGameModePattern correspondingPattern = null;
            foreach (RythmGameModePattern rythmGameModePattern in _config.RythmGameModePatterns.Patterns[player.Index])
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
            _config.RythmGameModePatterns.Patterns[player.Index].Remove(correspondingPattern);
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

