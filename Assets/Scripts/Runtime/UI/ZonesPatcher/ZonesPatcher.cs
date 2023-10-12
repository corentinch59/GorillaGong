using GorillaGong.Runtime.Patterns;
using GorillaGong.Runtime.PlayerPatterns;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GorillaGong.Runtime.UI.ZonesPatcher
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

        private void OnPlayerPatternCreated(DictionaryAddEvent<int, Pattern> obj) => OnPlayerPatternReplaced(obj.Key, null, obj.Value);
        private void OnPlayerPatternReplaced(DictionaryReplaceEvent<int, Pattern> obj) => OnPlayerPatternReplaced(obj.Key, obj.OldValue, obj.NewValue);
        private void OnPlayerPatternReplaced(int key, Pattern oldValue, Pattern newValue)
        {
            if (key != _playerIndex)
            {
                return;
            }

            if (newValue is null)
            {
                foreach (Image image in _images)
                {
                    image.color = Color.white;
                }

                return;
            }
            
            
            if (oldValue is not null)
            {
                foreach (int old in oldValue.Values)
                {
                    _images[old].color = Color.white;
                }
            }

            foreach (int newValueValue in newValue.Values)
            {
                _images[newValueValue].color = Color.green;
            }
        }
    }
}