using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public abstract class BaseSceneObject : MonoBehaviour
	{
		protected Light _light;
		protected Rigidbody _rigidbody;
		protected Material _material;
		protected Renderer _renderer;
		protected ParticleSystem _particleSystem;
		protected Collider _collider;

		public Light Light => _light;
		public Rigidbody Rigidbody => _rigidbody;
		public Material Material => _material;
		public Renderer Renderer => _renderer;
		public ParticleSystem ParticleSystem => _particleSystem;
		public Collider Collider => _collider;
		
		private void Awake()
		{
			_light = GetComponent<Light>();
			_rigidbody = GetComponent<Rigidbody>();
			_particleSystem = GetComponent<ParticleSystem>();
			_renderer = GetComponent<Renderer>();
			if (_renderer)
			{
				_material = _renderer.material;
			}

			_collider = GetComponent<Collider>();

			OnAwake();
		}

		protected abstract void OnAwake();
	}
}
