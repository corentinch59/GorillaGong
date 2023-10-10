using System;
using Runtime.PlayerPatterns;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Game.ZonesPatcher
{
    //TODO: this class should be instantiated at runtime and patched instead of being in the scene with SerializeField
    public class ZonesPatcher : MonoBehaviour
    {
        [SerializeField] private ReadOnlyPlayerPatterns _playerPatterns;
        [SerializeField] private int _playerIndex;
        [SerializeField] private Image[] _images;

        private CompositeDisposable _disposables;
        
        private void OnEnable()
        {
            _disposables = new CompositeDisposable();
                
            _disposables.Add(
                _playerPatterns.Patterns.ObserveAdd().Subscribe(OnPlayerPatternCreated)    
            );
            _disposables.Add(
                _playerPatterns.Patterns.ObserveReplace().Subscribe(OnPlayerPatternReplaced)
            );
        }

        private void OnDisable()
        {
            _disposables?.Dispose();
            _disposables = null;
        }

        private void OnPlayerPatternCreated(DictionaryAddEvent<int, int> obj) => OnPlayerPatternReplaced(obj.Key, 0, obj.Value);
        private void OnPlayerPatternReplaced(DictionaryReplaceEvent<int, int> obj) => OnPlayerPatternReplaced(obj.Key, obj.OldValue, obj.NewValue);
        private void OnPlayerPatternReplaced(int key, int oldValue, int newValue)
        {
            if (key != _playerIndex)
            {
                return;
            }

            _images[oldValue].color = Color.white;
            _images[newValue].color = Color.green;
        }
    }
}