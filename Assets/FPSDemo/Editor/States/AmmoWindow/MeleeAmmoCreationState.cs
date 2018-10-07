using FPSDemo;
using UnityEditor;

namespace FPSDemoEditor.Ammo
{
    internal class MeleeAmmoCreationState : AmmoCreationState<MeleeAmmoModel, MeleeAmmoController>
    {
        private float _lifeTime = 0.1f;
        
        protected override void OnShow()
        {
            _lifeTime = EditorGUILayout.FloatField("Life time", _lifeTime);
        }

        protected override void OnCreate()
        {
            _model.LifeTime = _lifeTime;
        }
    }
}