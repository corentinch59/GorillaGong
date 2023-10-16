using GorillaGong.Runtime.GameEvents;
using UnityEditor;
using UnityEngine;

namespace Editor.Inspectors
{
    [CustomEditor(typeof(PlayerModelGameEvent))]
    public class PlayerModelGameEventInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (target is not PlayerModelGameEvent playerModelGameEvent)
            {
                return;
            }
            
            if (GUILayout.Button("DEBUG RAISE"))
            {
                playerModelGameEvent.Raise();
            }
        }
    }
}