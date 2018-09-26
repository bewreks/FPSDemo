using System;
using System.Collections;
using UnityEngine;

namespace FPSDemo
{
    [Serializable]
    public struct ReloadTransform
    {
        public Vector3 ReloadPosition;
        public Vector3 ReloadRotation;
    }
    
    public class PistolWeaponView : BaseWeaponView<FirearmsWeaponModel>
    {
        
        public ReloadTransform ReloadTransform;
        private Light _light;

        private Vector3 _rotationStart;

        protected override void OnInitialize()
        {
            _light = _firepoint.GetComponent<Light>();
            _model.OnShoot += OnShoot;
            _model.OnEmptyShoot += OnEmptyShoot;
            _model.OnReload += OnReload;
        }

        private void OnReload()
        {
            StartCoroutine(StartReload("StandardReload"));
        }

        private void OnEmptyShoot()
        {
            Shoot("FireEmpty");
        }

        private void OnShoot()
        {
            Shoot("Fire");
        }

        private void Shoot(string animatioinName)
        {
            _animation.Play(animatioinName);
            StartCoroutine(ShowReflect());
        }

        private IEnumerator StartReload(string animatioinName)
        {
            transform.Translate(ReloadTransform.ReloadPosition);
            transform.Rotate(ReloadTransform.ReloadRotation);
            _animation.Play(animatioinName);
            yield return WaitForAnimation(_animation);
            transform.Rotate(-ReloadTransform.ReloadRotation);
            transform.Translate(-ReloadTransform.ReloadPosition);
        }

        private IEnumerator ShowReflect()
        {
            yield return new WaitForSeconds(0.1f);
            _light.enabled = true;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            _light.enabled = false;
        }
    }
}