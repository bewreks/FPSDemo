using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseAmmoController<M> : BaseController<M>, IFire where M : BaseAmmoModel
    {
        protected override void Initialize()
        {
            _model.Speed = 0;
            OnInit();
        }
        
        public void Fire(float force)
        {
            _model.Speed = force;
            OnFire();
        }

        protected abstract void OnFire();
        protected abstract void OnInit();
    }
}