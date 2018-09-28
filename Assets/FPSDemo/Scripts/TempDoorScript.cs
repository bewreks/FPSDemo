using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class TempDoorScript : MonoBehaviour {
		private void OnTriggerEnter(Collider other)
		{
			transform.GetChild(0).Translate(-1, 0,0);
		}

		private void OnTriggerExit(Collider other)
		{
			transform.GetChild(0).Translate(1, 0,0);
		}
	}
}
