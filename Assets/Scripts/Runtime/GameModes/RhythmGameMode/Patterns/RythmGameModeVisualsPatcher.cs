using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace GorillaGong.Runtime.Patterns
{
    public class RythmGameModeVisualsPatcher : RythmGameModePatternReactive
    {
        [SerializeField] private ParticleSystem _particlesPrefab;
        [SerializeField] private Transform[] _targetsPositions;
        private Dictionary<RythmGameModePattern, ParticleSystem> _instantiatedParticles = new();

        [Header("Colors")] 
        [SerializeField] private Color _notValidColor = Color.red;
        [SerializeField] private Color _validColor = Color.green;

        protected override void OnNewPatternAdded(CollectionAddEvent<RythmGameModePattern> patternAddedEvent)
        {
            foreach (int inputId in patternAddedEvent.Value.Pattern.Values)
            {
                _instantiatedParticles.Add(patternAddedEvent.Value, SpawnParticle(inputId, patternAddedEvent.Value));
            }
        }

        private ParticleSystem SpawnParticle(int inputId, RythmGameModePattern pattern)
        {
            ParticleSystem particlesInstance = GameObject.Instantiate(_particlesPrefab, _targetsPositions[inputId], true);
            var colorOverLifeTime = particlesInstance.colorOverLifetime;
            colorOverLifeTime.enabled = true;

            Gradient gradient = new Gradient();
            float colorSwitchPercentage = 1f - (pattern.ValidityDuration / pattern.Duration);
            GradientColorKey[] colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(_notValidColor, 0f),
                new GradientColorKey(_notValidColor,colorSwitchPercentage - 0.01f),
                new GradientColorKey(_validColor, colorSwitchPercentage + 0.01f),
                new GradientColorKey(_validColor, 1f)
            };
            gradient.SetKeys(colorKeys, new GradientAlphaKey[]{new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)});

            colorOverLifeTime.color = gradient;
            return particlesInstance;
        }

        protected override void OnPatternRemoved(CollectionRemoveEvent<RythmGameModePattern> patternRemovedEvent)
        {
            if (!_instantiatedParticles.TryGetValue(patternRemovedEvent.Value, out ParticleSystem toDestroy))
            {
                return;
            }
            
            Destroy(toDestroy);
            _instantiatedParticles.Remove(patternRemovedEvent.Value);
        }
    }
}