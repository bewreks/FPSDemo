using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public abstract class BaseSceneObject : MonoBehaviour
	{
		protected AnimationCurve _animationCurve;
		protected Light _light;
		protected Rigidbody _rigidbody;
		protected Material _material;
		protected Renderer _renderer;
		protected ParticleSystem _particleSystem;
		
		private void Awake()
		{
			_animationCurve = GetComponent<AnimationCurve>();
			_light = GetComponent<Light>();
			_rigidbody = GetComponent<Rigidbody>();
			_particleSystem = GetComponent<ParticleSystem>();
			_renderer = GetComponent<Renderer>();
			if (_renderer)
			{
				_material = _renderer.material;
			}

			OnAwake();
		}

		protected abstract void OnAwake();
	}
}
