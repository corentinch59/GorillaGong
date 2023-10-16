using GorillaGong.Runtime.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace GorillaGong.Runtime.UI.ZonesPatcher
{
    //TODO: this class should be instantiated at runtime and patched instead of being in the scene with SerializeField
    public class ZonesPatcher : PlayerPatternsReactive
    {
        [SerializeField] private Image[] _images;

        protected override void OnPlayerPatternReplaced(int key, Pattern oldValue, Pattern newValue)
        {
            if (key != PlayerIndex)
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