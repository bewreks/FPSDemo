using FPSDemo;
using UnityEditor;

namespace FPSDemoEditor.Weapons
{
    public class FirearmsBehaviourState : WeaponsTypeBehaviourState<FirearmsWeaponModel, FirearmsWeaponController>
    {
        private float _muzzleSpeed;
        private float _reloadTime;
        private int _bulletsCountMax;
        
        protected override void OnShow()
        {
            _muzzleSpeed = EditorGUILayout.FloatField("Muzzle speed", _muzzleSpeed);
            _reloadTime = EditorGUILayout.FloatField("Reload time", _reloadTime);
            _bulletsCountMax = EditorGUILayout.IntField("Bullets max", _bulletsCountMax);
        }

        protected override void OnCreate()
        {
            _model.MuzzleSpeed = _muzzleSpeed;
            _model.ReloadTime = _reloadTime;
            _model.BulletsCountCurrent = _bulletsCountMax;
        }
    }
}