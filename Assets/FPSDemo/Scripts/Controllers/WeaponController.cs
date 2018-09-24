using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class WeaponController : BaseController<WeaponModel>
    {
        protected override void Initialize()
        {
            
        }

        public void Fire()
        {
            if (_model.IsEmpty)
            {
                return;
            }

            if (_model.IsReloading)
            {
                return;
            }

            if (_model.IsTimeout)
            {
                return;
            }
            
            _model.BulletsCountCurrent--;
            _model.LastShootTime = Time.time;
            if (_model.BulletsCountCurrent == 0)
            {
                _model.OnEmptyShoot?.Invoke();
            }
            else
            {
                _model.OnShoot?.Invoke();
            }
        }

        public void Reload()
        {
            if (_model.IsFull)
            {
                return;
            }
            
            if (_model.BulletsCountCurrent == 0)
            {
                _model.OnEmptyReload?.Invoke();
            }
            else
            {
                _model.OnReload?.Invoke();
            }
            _model.BulletsCountCurrent = _model.BulletsCountMax;
            _model.StartReloadTime = Time.time;
        }
    }
}