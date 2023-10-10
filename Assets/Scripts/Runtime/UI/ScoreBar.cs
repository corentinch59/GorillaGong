using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.UI;

namespace Game
{
    public class ScoreBar : PlayerElement
    {
        private static int _nbInstances;

        [SerializeField] private Image _image;
        [SerializeField] private Material _scoreMaterial;
        [SerializeField] private FloatVariable _maxScore;

        public override void Deploy(IPlayerModel player)
        {
            _scoreMaterial = Instantiate(_scoreMaterial);
            _image.material = _scoreMaterial;
            _scoreMaterial.SetFloat("_Player", _nbInstances);
            transform.position += new Vector3(0.0f, 100.0f * _nbInstances, 0.0f);
            player.Score.Subscribe(UpdateScoreBar);
            _nbInstances++;
        }

        public void UpdateScoreBar(float amount)
        {
            _scoreMaterial.SetFloat("_Score", amount / _maxScore.Value);
        }
    }
}
