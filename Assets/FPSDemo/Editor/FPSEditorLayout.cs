using System.IO;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor
{
    public static class FPSEditorLayout
    {
        public static void ShowDelim()
        {
            EditorGUILayout.LabelField("==============================================================",
                EditorStyles.centeredGreyMiniLabel);
        }
        
        public static GameObject CreateField(string fieldName, GameObject gameObject)
        {
            return EditorGUILayout.ObjectField(fieldName, gameObject, typeof(GameObject), true) as GameObject;
        }

        public static void ShowHeader<T>(MonoBehaviour target)
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(target), typeof(T), false);
            GUI.enabled = true;
        }
    }
}