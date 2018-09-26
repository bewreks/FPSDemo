using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class GrenadeAmmoView : BaseView<ThrowableAmmoModel>
	{
		protected ParticleSystem _particleSystem;
		
		protected override void Initialize()
		{
			_particleSystem = GetComponent<ParticleSystem>();
			_model.OnExplosion += OnExplosion;
		}

		private void OnExplosion()
		{
			_particleSystem.Play();
		}
	}
}
