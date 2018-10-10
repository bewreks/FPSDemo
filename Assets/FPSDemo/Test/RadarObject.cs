using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject : MonoBehaviour
{

	public Image Image;

	private void Start()
	{
		Radar.Instance.Register(this);
	}

	private void OnDisable()
	{
		Radar.Instance.Remove(this);
	}
}
