using System.Collections;
using System.Collections.Generic;
using FPSDemo;
using UnityEngine;

public class MinimapView : BaseView<PlayerModel> {
	private Minimap _minimap;

	protected override void Initialize()
	{
		_minimap = GetComponentInParent<Minimap>();


		if (_minimap.IsInited)
		{
			OnMinimapInit();
		}
		else
		{
			_minimap.OnMinimapInit += OnMinimapInit;
		}

	}

	private void OnMinimapInit()
	{
		OnMove(Vector3.zero);

		_model.OnRotate += OnRotate;
		_model.OnMove += OnMove;
	}

	private void OnMove(Vector3 move)
	{
		var playerPosition = _model.transform.position;
		playerPosition.y = transform.position.y;
		transform.position = playerPosition;
	}

	private void OnRotate(Vector2 rotation)
	{
		transform.Rotate(0, 0, -rotation.y);
	}
}
