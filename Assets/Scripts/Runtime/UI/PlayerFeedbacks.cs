using System.Threading.Tasks;
using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.Player;
using UnityEngine;

namespace GorillaGong.Runtime.UI
{
    public class PlayerFeedbacks : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private PlayerModelGameEvent _playerSuccessEvent;
        [SerializeField] private PlayerModelGameEvent _playerFailEvent;
        [SerializeField] private int _playerIndex;

        [Header("Visuals")] 
        [SerializeField] private float _toggleDuration;
        [SerializeField] private GameObject _successGameObject;
        [SerializeField] private GameObject _failGameObject;

        private void Awake()
        {
            _successGameObject.SetActive(false);
            _failGameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _playerSuccessEvent.AddListener(OnPlayerSuccess);
            _playerFailEvent.AddListener(OnPlayerFail);
        }

        private void OnDisable()
        {
            _playerSuccessEvent.RemoveListener(OnPlayerSuccess);
            _playerFailEvent.RemoveListener(OnPlayerFail);
        }
        
        private void OnPlayerFail(IPlayerModel playerModel)
        {
            if (playerModel.Index != _playerIndex)
            {
                return;
            }
            _ = ToggleGameObject(_failGameObject);
        }

        private void OnPlayerSuccess(IPlayerModel playerModel)
        {
            if (playerModel.Index != _playerIndex)
            {
                return;
            }
            _ = ToggleGameObject(_successGameObject);
        }

        private async Task ToggleGameObject(GameObject target)
        {
            if (target == null)
            {
                return;
            }
            
            target.SetActive(true);
            await Task.Delay((int)(_toggleDuration * 1000f));
            target.SetActive(false);
        }
    }
}