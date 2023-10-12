using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Editor.GDWindow
{
    public class GDWindow : EditorWindow
    {
        // Data
        private ScriptableObject[] _allObjects;
        
        // GUI
        private Vector2 _scrollPos;
        // //  Selected button style
        private readonly Color _selectedButtonColor = Color.green;
        //

        [MenuItem("Window/GD Window", priority = 0)]
        public static void ShowWindow()
        {
            var window = GetWindow<GDWindow>();
            window.titleContent = new GUIContent("GD Window");
            window.Show();
        }

        private void OnEnable()
        {
            _allObjects = LoadAllAssets<ScriptableObject>()
                .Where(obj => AssetDatabase.GetLabels(obj).Contains("GD"))
                .ToArray();
        }

        private void OnGUI()
        {
            // Title and subtitle
            GUILayout.Label("GD WINDOW", new GUIStyle(GUI.skin.label){fontSize = 30});
            GUILayout.Label("Bang bang goes the gorilla", new GUIStyle(GUI.skin.label){fontStyle = FontStyle.Italic});
            
            // Content
            using (var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPos))
            {
                _scrollPos = scrollView.scrollPosition;
                
                for (int i = 0; i < _allObjects.Length; i++)
                {
                    ScriptableObject element = _allObjects[i];
                    GUIStyle style = element == Selection.activeObject
                        ? new GUIStyle(GUI.skin.button){normal = {textColor = _selectedButtonColor}}
                        : GUI.skin.button;
                    
                    if (GUILayout.Button(element.name, style))
                    {
                        Selection.activeObject = element;
                    }
                }
            }
        }
        
        private IEnumerable<T> LoadAllAssets<T>() where T : ScriptableObject
        {
            List<T> results = new();
            string[] guids = AssetDatabase.FindAssets("t:"+typeof(T), null);
            foreach (string guid in guids)
            {
                T item = (T)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(T));
                if (item != null)
                {
                    results.Add(item);
                }
            }
            return results;
        }
    }
}