using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class ThrowableAmmoController : BaseAmmoController<ThrowableAmmoModel>
	{
		private Rigidbody _rigidbody;
		
		protected override void OnFire()
		{
			_rigidbody.AddForce(transform.forward * _model.Speed, ForceMode.Impulse);
			StartCoroutine(Fuse());
		}

		protected override void OnInit()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private IEnumerator Fuse()
		{
			yield return new WaitForSeconds(_model.FuseTimeout);
			_rigidbody.isKinematic = true;
			_model.IsHitted = true;
			var colliders = Physics.OverlapSphere(transform.position, _model.DamageRadius, _model.Mask);
			foreach (var hitCollider in colliders)
			{
				var d = hitCollider.GetComponent<IDamagable>();
				d?.DoDamage(_model.Damage, _model.Owner);
			}
			_model.OnExplosion?.Invoke();
			Destroy(gameObject, 0.3f);
		}
	}
}
