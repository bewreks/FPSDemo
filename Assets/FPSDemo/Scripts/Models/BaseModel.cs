using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
	public class BaseModel : MonoBehaviour
	{
		public UnityAction OnInit;

		protected bool _isInited;

		public bool IsInited => _isInited;

		protected void Awake()
		{
			_isInited = true;
			OnInit?.Invoke();
		}
	}
}