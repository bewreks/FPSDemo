using System;
using UnityEngine;

namespace FPSDemo
{
//TODO: review MVC for multiple models of one type
	public class BaseModel : MonoBehaviour
	{
		public Action OnInit;

		protected bool _isInited;

		public bool IsInited => _isInited;

		protected void Awake()
		{
			_isInited = true;
			OnInit?.Invoke();
		}
	}
}