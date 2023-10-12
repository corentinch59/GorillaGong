using System;
using GorillaGong.Runtime.Player;
using ScriptableObjectArchitecture;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GorillaGong.Runtime.UI
{
    public class ScoreBar : PlayerElement
    {
        private static int _nbInstances;

        [SerializeField] private Image _image;
        [SerializeField] private Material _scoreMaterial;
        [SerializeField] private FloatVariable _maxScore;

        private IDisposable _disposable;

        private void OnDisable()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        public override void Deploy(IPlayerModel player)
        {
            _scoreMaterial = Instantiate(_scoreMaterial);
            _image.material = _scoreMaterial;
            _scoreMaterial.SetFloat("_Player", _nbInstances); 
            _disposable = player.Score.Subscribe(UpdateScoreBar);
            _nbInstances++;
        }

        public void UpdateScoreBar(float amount)
        {
            _scoreMaterial.SetFloat("_Score", amount / _maxScore.Value);
        }
    }
}
