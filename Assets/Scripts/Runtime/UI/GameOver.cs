using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.Player;
using TMPro;
using UnityEngine;

namespace Runtime.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private PlayerModelGameEvent _gameFinishedEvent;
        [SerializeField] private TMP_Text _playerWonText;

        private void OnEnable()
        {
            _gameFinishedEvent.AddListener(Patch);
        }

        private void OnDisable()
        {
            _gameFinishedEvent.RemoveListener(Patch);
        }

        public void Patch(IPlayerModel playerModel)
        {
            _playerWonText.text = _playerWonText.text.Replace("[PLAYER_ID]", (playerModel.Index + 1).ToString());
        }
    }
}