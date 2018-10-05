using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class BaseAmmoModel : BaseModel
	{
		public float Damage;
		public float Speed;

		public LayerMask Mask;

		public bool IsHitted;

		public GameObject Owner;

	}
}
