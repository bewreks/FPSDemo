using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class WeaponView : BaseView<WeaponModel>
    {
        public GameObject _firepoint;
        private Animation _animation;
        private Light _light;
        
        protected override void Initialize()
        {
            _light = _firepoint.GetComponent<Light>();
            _animation = transform.GetComponentInChildren<Animation>();
            _model.OnShoot += OnShoot;
            _model.OnEmptyShoot += OnEmptyShoot;
            _model.OnEmptyReload += OnEmptyReload;
            _model.OnReload += OnReload;
        }

        private void OnReload()
        {
            _animation.Play("StandardReload");
        }

        private void OnEmptyReload()
        {
            _animation.Play("ReloadEmpty");
        }

        private void OnEmptyShoot()
        {
            _animation.Play("FireEmpty");
            Shoot();
        }

        private void OnShoot()
        {
            _animation.Play("Fire");
            Shoot();
        }

        private void Shoot()
        {
            StartCoroutine(HideBlick());
        }

        private IEnumerator HideBlick()
        {
            yield return new WaitForSeconds(0.1f);
            _light.enabled = true;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            _light.enabled = false;
        }
    }
}