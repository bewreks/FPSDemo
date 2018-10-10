using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

namespace FPSDemo
{
    public class PlayerSecondaryView : BaseView<PlayerModel>
    {
        private float _rotationX;
        
        protected override void Initialize()
        {
            _rotationX = transform.localEulerAngles.x;
            _model.OnRotate += OnRotate;
            _model.OnSetSummaryRotation += OnLoad;
        }

        private void OnLoad(Vector3 euler)
        {
            var transformLocalEulerAngles = new Vector3(euler.x, 0, 0);
            if (transformLocalEulerAngles.x > 180)
            {
                transformLocalEulerAngles.x -= 360;
            }
            else
            {
                if (transformLocalEulerAngles.x < -180)
                {
                    transformLocalEulerAngles.x += 360;
                }
            }
            _rotationX = transformLocalEulerAngles.x;
            transform.localEulerAngles = transformLocalEulerAngles;
        }

        private void OnRotate(Vector2 rotation)
        {
            _rotationX += rotation.x;
            _rotationX = Mathf.Clamp(_rotationX, _model.MinAngle, _model.MaxAngle);

            transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
            _model.SummaryRotationVector = new Vector3(
                _rotationX, 
                _model.SummaryRotationVector.y,
                0);
        }
    }
}