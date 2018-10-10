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

    [Serializable]
    public struct AimTransform
    {
        public Vector3 AimPosition;
        public float FieldOfView;
        public float OldFieldOfView;
    }
    
    public class PistolWeaponView : BaseWeaponView<FirearmsWeaponModel>
    {
        public ReloadTransform ReloadTransform;
        public AimTransform AimTransform;
        private PistolAnimationTrigger _trigger;
        private Light _light;

        private Vector3 _rotationStart;

        protected override void OnInitialize()
        {
            AimTransform.OldFieldOfView = Camera.main.fieldOfView;
            _trigger = GetComponentInChildren<PistolAnimationTrigger>();
            _trigger.OnReloadEnd += OnReloadEnd;
            _trigger.OnReloadRotationStart += OnReloadRotationStart;
            _trigger.OnReloadRotationEnd += OnReloadRotationEnd;
            _light = _firepoint.GetComponent<Light>();
            _model.OnShoot += OnShoot;
            _model.OnReload += OnReload;
            _model.OnTakeAim += OnTakeAim;
            _model.OnRealizeAim += OnRealizeAim;
            _animator.SetInteger("Bullets", _model.BulletsCountCurrent);
        }

        private void OnRealizeAim()
        {
            transform.Translate(-AimTransform.AimPosition);
            Camera.main.fieldOfView = AimTransform.OldFieldOfView;
        }

        private void OnTakeAim()
        {
            transform.Translate(AimTransform.AimPosition);
            Camera.main.fieldOfView = AimTransform.FieldOfView;
        }

        private void OnReload()
        {
            _model.OnTakeAim -= OnTakeAim;
            _model.OnRealizeAim -= OnRealizeAim;
            if (_model.IsAim)
            {
                OnRealizeAim();
            }
            _animator.SetTrigger("Reload");
            _animator.SetInteger("Bullets", _model.BulletsCountCurrent);
        }

        private void OnReloadEnd()
        {
            _model.OnTakeAim += OnTakeAim;
            _model.OnRealizeAim += OnRealizeAim;
            if (_model.IsAim)
            {
                OnTakeAim();
            }
        }

        private void OnReloadRotationEnd()
        {
            transform.Rotate(-ReloadTransform.ReloadRotation);
            transform.Translate(-ReloadTransform.ReloadPosition);
        }

        private void OnReloadRotationStart()
        {
            transform.Translate(ReloadTransform.ReloadPosition);
            transform.Rotate(ReloadTransform.ReloadRotation);
        }

        private void OnShoot()
        {
            _animator.SetInteger("Bullets", _model.BulletsCountCurrent);
            _animator.SetTrigger("Fire");
            StartCoroutine(ShowReflect());
        }

        private IEnumerator ShowReflect()
        {
            yield return new WaitForSeconds(0.1f);
            _light.enabled = true;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            _light.enabled = false;
        }

        private void OnEnable()
        {
            if (IsInitialized)
            {
                _animator.SetInteger("Bullets", _model.BulletsCountCurrent);
                _animator.SetTrigger("Take");
            }
        }
    }
}