using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class AmmoModel : BaseModel
	{
		public float Damage;
		public float DestroyTime;

		public LayerMask Mask;
		public float Speed;

		public bool IsHitted;

	}
}
