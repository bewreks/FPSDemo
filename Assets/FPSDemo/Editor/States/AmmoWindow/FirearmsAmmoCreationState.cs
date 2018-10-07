using FPSDemo;
using UnityEditor;

namespace FPSDemoEditor.Ammo
{
    internal class FirearmsAmmoCreationState : AmmoCreationState<FirearmsAmmoModel, FirearmsAmmoController>
    {
        private float _destroyTime;
        
        protected override void OnShow()
        {
            _destroyTime = EditorGUILayout.FloatField("Destroy time", _destroyTime);
        }

        protected override void OnCreate()
        {
            _model.DestroyTime = _destroyTime;
        }
    }
}