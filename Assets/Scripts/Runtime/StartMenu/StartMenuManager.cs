using System.Linq;
using GorillaGong.Runtime.GameEvents;
using GorillaGong.Runtime.Player;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Events;

public class StartMenuManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private int[] _readyInputsIds;
    
    [Header("Dependencies")]
    [SerializeField] private PlayerManager _playerManager;

    [Header("Events")] 
    [SerializeField] private GameEvent _allPlayersReadyEvent;
    [SerializeField] private UnityEvent _allPlayersReadyResponse;
    [SerializeField] private PlayerModelGameEvent _playerReadyEvent;
    [SerializeField] private PlayerModelGameEvent _playerUnreadyEvent;

    private bool[] _playersReadiness;
    
    private void OnEnable()
    {
        _playersReadiness = new bool[_playerManager.PlayersCount()];
        foreach (Player player in _playerManager.GetPlayers())
        {
            player.OnInputPressed += OnPlayerInputPressed;
        }
    }

    private void OnDisable()
    {
        foreach (Player player in _playerManager.GetPlayers())
        {
            player.OnInputPressed -= OnPlayerInputPressed;
        }
    }

    private void OnPlayerInputPressed(Player player, int[] inputsIds)
    {
        foreach (var readyInputsId in _readyInputsIds)
        {
            if (!inputsIds.Contains(readyInputsId))
            {
                return;
            }
        }
        
        // Players pressed ready inputs
        bool newValue = !_playersReadiness[player.Index];
        _playersReadiness[player.Index] = newValue;
        if (newValue)
        {
            _playerReadyEvent.Raise(player);
        }
        else
        {
            _playerUnreadyEvent.Raise(player);
        }

        foreach (bool isPlayerReady in _playersReadiness)
        {
            if (!isPlayerReady)
            {
                return;
            }
        }
        // All players are ready
        _allPlayersReadyEvent.Raise();
        _allPlayersReadyResponse.Invoke();
        gameObject.SetActive(false);
    }
}
