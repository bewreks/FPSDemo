using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class AmmoController : BaseController<AmmoModel>
    {
        protected override void Initialize()
        {
            _model.Speed = 0;
        }

        public void Fire(float force)
        {
            _model.Speed = force;
        }

        private void FixedUpdate()
        {
            if (_model.IsHitted)
            {
                return;
            }

            var finalPos = transform.position + transform.forward * _model.Speed * Time.fixedDeltaTime;
            RaycastHit hit;
            if (Physics.Linecast(transform.position, finalPos, out hit, _model.Mask))
            {
                _model.IsHitted = true;
                transform.position = hit.point;

                /*IDamageable d = hit.collider.GetComponent<IDamageable>();
                if (d != null)
                    d.ApplyDamage(_damage);*/

                Destroy(gameObject, 0.3f);
            }
            else
            {
                transform.position = finalPos;
            }
        }
    }
}