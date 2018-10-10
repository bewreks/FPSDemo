using UnityEngine;

namespace FPSDemo {
	public class PlayerMainView : BaseView<PlayerModel>
	{
		private CharacterController _char;
		
		protected override void Initialize()
		{
			_char = GetComponent<CharacterController>();
			_model.OnMove += OnMove;
			_model.OnRotate += OnRotate;
			_model.OnSetSummaryRotation += OnLoad;
		}

		private void OnLoad(Vector3 euler)
		{
			transform.rotation = Quaternion.Euler(0, euler.y, 0);
		}

		private void OnRotate(Vector2 rotation)
		{
			transform.Rotate(0, rotation.y, 0);
			_model.SummaryRotationVector = new Vector3(
				_model.SummaryRotationVector.x, 
				transform.rotation.eulerAngles.y,
				0);
		}

		private void OnMove(Vector3 movement)
		{
			_char.Move(movement);
		}
	}
}
