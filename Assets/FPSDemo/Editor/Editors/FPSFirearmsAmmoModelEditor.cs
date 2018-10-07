using FPSDemo;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Editors
{
    [CustomEditor(typeof(FirearmsAmmoModel))]
    public class FPSFirearmsAmmoModelEditor : FPSBaseAmmoModelEditor<FirearmsAmmoModel>
    {
        protected override void OnGui()
        {
            _model.DestroyTime = EditorGUILayout.FloatField(new GUIContent("DesroyTime", "Not used in code"), _model.DestroyTime);
        }

        protected override void OnGuiReadonly()
        {
        }
    }
}