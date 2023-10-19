using System.Collections.Generic;
using GorillaGong.Runtime.Patterns;
using UnityEngine;

namespace GorillaGong.Runtime
{
    public class GorillaMaterialPatcher : PlayerPatternsReactive
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int[] _materialsIndexes;
        private Material[] _materials;
        private Color[] _defaultColors;
        
        [SerializeField] private string _materialColorName;
        [SerializeField] private Color _highlightedZoneColor;
        
        private void Awake()
        {
            // Populate _materials array
            List<Material> rendererMaterials = new List<Material>();
            _renderer.GetMaterials(rendererMaterials);
            
            _materials = new Material[_materialsIndexes.Length];
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i] = rendererMaterials[_materialsIndexes[i]];
            }
            // //
            
            // Populate default colors array
            _defaultColors = new Color[_materials.Length];
            for (int i = 0; i < _materials.Length; i++)
            {
                _defaultColors[i] = _materials[i].GetColor(_materialColorName);
            }
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
                for (int i = 0; i < _materials.Length; i++)
                {
                    _materials[i].SetColor(_materialColorName, _defaultColors[i]);
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
                _materials[value].SetColor(_materialColorName, _highlightedZoneColor);
            }
        }
    }
}