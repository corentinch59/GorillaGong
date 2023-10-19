using GorillaGong.Runtime.Player;
using UnityEngine;

namespace GorillaGong.Runtime
{
    public class ParticleEffectsSpawner : PlayerSuccessFailureListener
    {
        [Header("Particles")]
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private GameObject _successParticlesPrefab;
        [SerializeField] private GameObject _failParticlesPrefab;
        [SerializeField] private float _particlesDestroyTime = 2f;
        
        protected override void OnPlayerSuccess(IPlayerModel player)
        {
            if (player.Index != PlayerIndex)
            {
                return;
            }
            
            SpawnParticle(_successParticlesPrefab);
        }

        protected override void OnPlayerFail(IPlayerModel player)
        {
            if (player.Index != PlayerIndex)
            {
                return;
            }
            
            SpawnParticle(_failParticlesPrefab);
        }

        private void SpawnParticle(GameObject particlePrefab)
        {
            foreach (int targets in PlayerPatterns.Values[PlayerIndex].Values)
            {
                Destroy(GameObject.Instantiate(particlePrefab, _spawnPositions[targets]), _particlesDestroyTime);
            }
        }
    }
}