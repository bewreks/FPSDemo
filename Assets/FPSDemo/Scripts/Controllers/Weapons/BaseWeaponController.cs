﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public abstract class BaseWeaponController<M> : BaseController<M>, IWeapon where M : BaseWeaponModel
    {
        private Transform _firepoint;

        public UnityAction OnFireEnd { get; set; }
        public UnityAction<GameObject> OnBulletInstatiate { get; set; }
        public GameObject GameObject => gameObject;

        public GameObject Owner;
        
        protected override void Initialize()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.CompareTag("FirePoint"))
                {
                    _firepoint = child;
                    break;
                }
            }

            if (Owner)
            {
                SetOwner(Owner);
            }
        }


        public bool Fire()
        {
            if (_model.IsEmpty)
            {
                return false;
            }

            if (CantFire())
            {
                return false;
            }

            if (_model.IsTimeout)
            {
                return false;
            }

            _model.BulletsCountCurrent--;
            _model.LastShootTime = Time.time;
            OnShoot();

            StartCoroutine(Shoot());
            return true;
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(_model.Preparation);
            OnShootAfterPrepare();

            Vector3 hitPoint = CheckHitPoint(_firepoint.position);
            var ammoRotation = _firepoint.rotation;
            if (hitPoint != _firepoint.position)
            {
                var forward = hitPoint - _firepoint.position;
                ammoRotation = Quaternion.LookRotation(forward);
            }

            var ammo = Instantiate(_model.AmmoPrefab, _firepoint.position, ammoRotation);
            ammo.GetComponent<IFire>().Fire(_model.Power, _model.Owner);
            OnBulletInstatiate?.Invoke(ammo);

            OnFireEnd?.Invoke();
        }

        public bool IsActive()
        {
            return gameObject.activeInHierarchy;
        }

        protected virtual Vector3 CheckHitPoint(Vector3 hitPoint)
        {
            return hitPoint;
        }

        public void SetOwner(GameObject owner)
        {
            if (_model)
            {
                _model.Owner = owner;
                Owner = null;
            }
            else
            {
                Owner = owner;
            }
        }

        public abstract void Reload();
        public abstract void TakeAim();
        public abstract void RealizeAim();
        protected abstract bool CantFire();
        protected abstract void OnShoot();
        protected abstract void OnShootAfterPrepare();
    }
}