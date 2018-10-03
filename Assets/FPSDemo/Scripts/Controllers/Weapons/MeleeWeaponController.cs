using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class MeleeWeaponController : BaseWeaponController<MeleeWeaponModel>
    {
        public override void Reload()
        {
            
        }

        public override void TakeAim()
        {
            
        }

        public override void RealizeAim()
        {
            
        }

        protected override bool CantFire()
        {
            return false;
        }

        protected override void OnShoot()
        {
            _model.OnShoot?.Invoke();
        }

        protected override void OnShootAfterPrepare()
        {
            _model.OnShootAfterPrepare?.Invoke();
        }
    }
}