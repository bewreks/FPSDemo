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
        }

        private void OnShoot()
        {
            _animation.Play("Throw");
            
        }
    }
}