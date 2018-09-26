using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class FirearmsWeaponController : BaseWeaponController<FirearmsWeaponModel>
    {
        public override void Reload()
        {
            if (_model.IsFull)
            {
                return;
            }
            
            _model.OnReload?.Invoke();
            _model.BulletsCountCurrent = _model.BulletsCountMax;
            _model.StartReloadTime = Time.time;
        }

        protected override bool CantFire()
        {
            return _model.IsReloading;
        }

        protected override void OnShoot()
        {
            if (_model.BulletsCountCurrent == 0)
            {
                _model.OnEmptyShoot?.Invoke();
            }
            else
            {
                _model.OnShoot?.Invoke();
            }
        }
    }
}