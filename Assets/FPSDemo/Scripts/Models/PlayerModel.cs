using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class PlayerModel : BaseModel
    {
        public float HorizontalSensivity = 2;
        public float VerticalSensivity = 2;

        public float Speed = 10;

        public float MinAngle = -10;
        public float MaxAngle = 80;

        public float Gravity = -9.8f;
        
        public UnityAction<Vector3> OnMove;
        public UnityAction<Vector2> OnRotate;
        
        private Vector3 _moveVector;
        private Vector2 _rotateVector;

        public Vector2 RotateVector
        {
            get { return _rotateVector; }
            set
            {
                _rotateVector = value; 
                OnRotate?.Invoke(_rotateVector);
            }
        }

        public Vector3 MoveVector
        {
            get { return _moveVector; }
            set
            {
                _moveVector = value;
                OnMove?.Invoke(_moveVector);
            }
        }
    }
}