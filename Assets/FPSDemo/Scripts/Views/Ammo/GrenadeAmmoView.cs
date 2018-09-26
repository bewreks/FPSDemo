using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class GrenadeAmmoView : BaseView<ThrowableAmmoModel>
	{
		public float LightTimeout = 0.5f;
		protected ParticleSystem _particleSystem;
		protected Light _light;
		
		protected override void Initialize()
		{
			_particleSystem = GetComponent<ParticleSystem>();
			_light = GetComponent<Light>();
			_model.OnExplosion += OnExplosion;
		}

		private void OnExplosion()
		{
			_particleSystem.Play();
			_light.enabled = true;
			StartCoroutine(StopLightning());
		}

		private IEnumerator StopLightning()
		{
			yield return new WaitForSeconds(LightTimeout);
			_light.enabled = false;
		}
	}
}
