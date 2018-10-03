using UnityEngine;

namespace FPSDemo
{
    public class MeleeAmmoController : BaseAmmoController<MeleeAmmoModel>
    {
        private Collider _collider;
        
        protected override void OnFire()
        {
            _collider.enabled = true;
            Invoke("Remove", _model.LifeTime);
        }

        protected override void OnInit()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var component = other.GetComponent<IDamagable>();
            component?.DoDamage(_model.Damage);
        }

        private void Remove()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}