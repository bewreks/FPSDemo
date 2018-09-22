using System;
using UnityEngine;

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
