using GorillaGong.Runtime.RuntimeSets;
using UnityEditor;

namespace Editor.Inspectors
{
    [CustomEditor(typeof(BaseRuntimeSet<>), true)]
    public class BaseRuntimeSetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (target is not RuntimeSet runtimeSet)
            {
                return;
            }

            EditorGUILayout.LabelField("Values");
            foreach (var VARIABLE in runtimeSet)
            {
                EditorGUILayout.LabelField(VARIABLE.ToString());
            }
        }
    }
}