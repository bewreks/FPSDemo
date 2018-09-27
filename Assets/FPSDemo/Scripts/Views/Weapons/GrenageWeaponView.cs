using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class GrenageWeaponView : BaseWeaponView<ThrowableWeaponModel>
    {

        protected override void OnInitialize()
        {
            _model.OnShoot += OnShoot;
            _model.OnShootAfterPrepare += OnShootAfterPrepare;
        }

        private void OnShootAfterPrepare()
        {
            _animation.Play("Idle01");
        }

        private void OnShoot()
        {
            _animation.Play("Throw");
        }
    }
}