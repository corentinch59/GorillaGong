using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(GameEventBase), true)]
    public sealed class GameEventEditor : BaseGameEventEditor
    {
        private GameEvent Target { get { return (GameEvent)target; } }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var serializedProperty = serializedObject.FindProperty("Debug");
            if (serializedProperty != null)
            {
                EditorGUILayout.PropertyField(serializedProperty);
            }
        }

        protected override void DrawRaiseButton()
        {
            if (GUILayout.Button("Raise"))
            {
                Target.Raise();
            }
        }
    } 
}