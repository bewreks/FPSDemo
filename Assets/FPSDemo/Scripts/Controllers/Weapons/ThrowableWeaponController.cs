using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class ThrowableWeaponController : BaseWeaponController<ThrowableWeaponModel> {
		public override void Reload()
		{
			
		}

		protected override bool CantFire()
		{
			return false;
		}

		protected override void OnShoot()
		{
			_model.OnShoot?.Invoke();
		}
	}
}
