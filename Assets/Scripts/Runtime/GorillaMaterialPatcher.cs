using System.Collections.Generic;
using System.Linq;
using GorillaGong.Runtime.Patterns;
using UnityEngine;

namespace GorillaGong.Runtime
{
    public class GorillaMaterialPatcher : PlayerPatternsReactive
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int[] _materialsIndexes;
        
        private List<Material> _defaultRendererMaterials = new List<Material>();
        private List<Material> _materials = new List<Material>();

        [SerializeField] private Material _highlightedZoneMaterial;
        
        private void Awake()
        {
            // Get materials from the given Renderer
            _renderer.GetMaterials(_defaultRendererMaterials);
            _materials = new List<Material>(_defaultRendererMaterials);
            // //
        }

        protected override void OnPlayerPatternReplaced(int key, Pattern oldValue, Pattern newValue)
        {
            if (key != PlayerIndex)
            {
                return;
            }

            // Reassign all colors to default values
            if (newValue is null || oldValue is not null)
            {
                _renderer.SetMaterials(_defaultRendererMaterials);
                for (int i = 0; i < _defaultRendererMaterials.Count; i++)
                {
                    _materials[i] = _defaultRendererMaterials[i];
                }
                
                if (newValue is null)
                {
                    return;
                }
            }

            // newValue is not null
            // Change wanted zones color
            foreach (int value in newValue.Values)
            {
                _materials[_materialsIndexes[value]] = _highlightedZoneMaterial;
            }
            _renderer.SetMaterials(_materials);
        }
    }
}