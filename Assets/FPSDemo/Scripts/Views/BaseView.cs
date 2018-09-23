using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
	public abstract class BaseView<M> : MonoBehaviour where M : BaseModel
	{
		protected M _model;

		protected virtual void Awake()
		{
			_model = FindObjectOfType<M>();
			if (_model.IsInited)
			{
				InitializeView();
			}
			else
			{
				_model.OnInit += InitializeView;
			}
		}

		protected abstract void InitializeView();
	}
}
