using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class FirearmsAmmoController : BaseAmmoController<FirearmsAmmoModel>
    {
        protected override void OnFire()
        {
            
        }

        protected override void OnInit()
        {
            
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

                var d = hit.collider.GetComponent<IDamagable>();
                d?.DoDamage(_model.Damage);

                Destroy(gameObject, 0.3f);
            }
            else
            {
                transform.position = finalPos;
            }
        }
    }
}