using GorillaGong.Runtime.Inputs;
using UnityEditor;
using UnityEngine;

namespace Editor.Inspectors
{
    [CustomEditor(typeof(PlayerInputGameEvent))]
    public class PlayerInputGameEventInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (target is not PlayerInputGameEvent playerInputGameEvent)
            {
                return;
            }
            
            if (GUILayout.Button("DEBUG RAISE"))
            {
                playerInputGameEvent.Raise();
            }
        }
    }
}