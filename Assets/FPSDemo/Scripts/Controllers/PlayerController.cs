using UnityEngine;

namespace FPSDemo
{
    public class PlayerController : BaseController<PlayerModel>
    {
        
        
        protected override void Initialize()
        {
            
        }

        public void Move(float x, float y)
        {
            var movement = new Vector3(x, 0, y) * _model.Speed;
            movement = Vector3.ClampMagnitude(movement, _model.Speed);

            movement.y = _model.Gravity;

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _model.MoveVector = movement;
        }

        public void Rotate(float x, float y)
        {
            var rotationY = x * _model.HorizontalSensivity;
            var rotationX = y * _model.VerticalSensivity;

            _model.RotateVector = new Vector3(-rotationX, rotationY);
        }
    }
}