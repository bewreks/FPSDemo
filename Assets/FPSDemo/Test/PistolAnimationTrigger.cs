using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PistolAnimationTrigger : MonoBehaviour
{

	public UnityAction OnReloadEnd;
	public UnityAction OnReloadRotationStart;
	public UnityAction OnReloadRotationEnd;

	public void EndReloadRotation()
	{
		OnReloadRotationEnd?.Invoke();
	}

	public void StartReloadRotation()
	{
		OnReloadRotationStart?.Invoke();
	}

	public void ReloadEnd()
	{
		OnReloadEnd?.Invoke();
	}
}
