using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseWeaponController<M> : BaseController<M> where M : BaseWeaponModel
    {
        private Transform _firepoint;
        
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
        }
        
        public void Fire()
        {
            if (_model.IsEmpty)
            {
                return;
            }

            if (CantFire())
            {
                return;
            }

            if (_model.IsTimeout)
            {
                return;
            }
            
            _model.BulletsCountCurrent--;
            _model.LastShootTime = Time.time;
            OnShoot();

            var ammo = Instantiate(_model.AmmoPrefab, _firepoint.position, _firepoint.rotation);
            ammo.GetComponent<IFire>().Fire(_model.Power);
        }

        public abstract void Reload();
        protected abstract bool CantFire();
        protected abstract void OnShoot();
    }
}