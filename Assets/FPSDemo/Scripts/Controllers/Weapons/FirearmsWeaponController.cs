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

        public override void TakeAim()
        {
            _model.IsAim = true;
        }

        public override void RealizeAim()
        {
            _model.IsAim = false;
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

        protected override Vector3 CheckHitPoint(Vector3 hitPoint)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0));

            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }
            return base.CheckHitPoint(hitPoint);
        }

        protected override void OnShootAfterPrepare()
        {
            
        }
    }
}