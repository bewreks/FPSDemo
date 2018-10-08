using FPSDemo;
using UnityEditor;

namespace FPSDemoEditor.Ammo
{
    internal class ThrowableAmmoCreationState : AmmoCreationState<ThrowableAmmoModel, ThrowableAmmoController>
    {
        private float _damageRadius;
        private float _fuseTimeout;
        
        protected override void OnShow()
        {
            _damageRadius = EditorGUILayout.FloatField("Radius",_damageRadius);
            _fuseTimeout = EditorGUILayout.FloatField("Fuse timeout", _fuseTimeout);
        }

        protected override void OnCreate()
        {
            _model.DamageRadius = _damageRadius;
            _model.FuseTimeout = _fuseTimeout;
        }
    }
}