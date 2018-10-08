using FPSDemo;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Editors
{
    [CustomEditor(typeof(MeleeAmmoModel))]
    public class FPSMeleeAmmoModelEditor : FPSBaseAmmoModelEditor<MeleeAmmoModel>
    {
        protected override void OnGui()
        {
            _model.LifeTime = EditorGUILayout.FloatField("Life time", _model.LifeTime);
        }

        protected override void OnGuiReadonly()
        {
            
        }
    }
}