using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.Patterns
{
    [CreateAssetMenu(menuName = "Variables/Pattern Variable")]
    public class RythmGameModePatternsVariable : ReadOnlyRythmGameModePatternsVariable
    {
        public override IReadOnlyReactiveCollection<RythmGameModePattern>[] ReadOnlyPatterns => Patterns;
        public ReactiveCollection<RythmGameModePattern>[] Patterns { get; set; }

        public void Clear()
        {
            Patterns = null;
        }
    }
    
    public abstract class ReadOnlyRythmGameModePatternsVariable : ScriptableObject
    {
        public abstract IReadOnlyReactiveCollection<RythmGameModePattern>[] ReadOnlyPatterns { get; }
    }

    public class RythmGameModePattern
    {
        public Pattern Pattern { get; private set; }
        
        /// <summary>
        /// The duration the pattern should be visible at the screen
        /// </summary>
        public float Duration { get; private set; }

        
        /// <summary>
        /// The duration in which the pattern is valid for inputs
        /// </summary>
        public float ValidityDuration { get; private set; }
        
        /// <summary>
        /// Current state of the pattern
        /// </summary>
        public IReactiveProperty<State> CurrentState { get; private set; }
        public enum State
        {
            None = -1,
            NotValid,
            Valid,
            Pressed
        }

        public RythmGameModePattern(Pattern pattern, float duration, float validityDuration)
        {
            Pattern = pattern;
            Duration = duration;
            ValidityDuration = validityDuration;
            CurrentState = new ReactiveProperty<State>(State.NotValid);
        }

        public void DecreaseDuration(float deltaTime)
        {
            Duration -= deltaTime;
        }
    }
}