using FPSDemo;
using UnityEditor;

namespace FPSDemoEditor.Editors
{
    [CustomEditor(typeof(ThrowableAmmoModel))]
    public class FPSThrowableAmmoModelEditor : FPSBaseAmmoModelEditor<ThrowableAmmoModel>
    {
        protected override void OnGui()
        {
            _model.FuseTimeout = EditorGUILayout.FloatField("Fuse timeout", _model.FuseTimeout);
            _model.DamageRadius = EditorGUILayout.FloatField("Damage radius", _model.DamageRadius);
        }

        protected override void OnGuiReadonly()
        {
            
        }
    }
}