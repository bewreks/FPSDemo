using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class PlayerSecondaryView : BaseView<PlayerModel>
    {
        private float _rotationX;
        
        protected override void Initialize()
        {
            _rotationX = transform.localEulerAngles.x;
            _model.OnRotate += OnRotate;
        }

        private void OnRotate(Vector2 rotation)
        {
            _rotationX += rotation.x;
            _rotationX = Mathf.Clamp(_rotationX, _model.MinAngle, _model.MaxAngle);

            transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
        }
    }
}