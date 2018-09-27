using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseWeaponView<M> : BaseView<M> where M : BaseWeaponModel
    {
        protected GameObject _firepoint;
        protected Animation _animation;
        
        protected override void Initialize()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.CompareTag("FirePoint"))
                {
                    _firepoint = child.gameObject;
                    break;
                }
            }
            _animation = transform.GetComponentInChildren<Animation>();
            OnInitialize();
        }

        protected abstract void OnInitialize();
    }
}