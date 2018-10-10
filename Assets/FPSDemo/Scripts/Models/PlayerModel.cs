using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class PlayerModel : BaseModel
    {
        public UnityAction OnHpChanged;
        public float HorizontalSensivity = 2;
        public float VerticalSensivity = 2;

        public float Speed = 10;

        public float MinAngle = -10;
        public float MaxAngle = 80;

        public float Gravity = -9.8f;
        
        public Armor Armor = new Armor{ArmorType = 3, BaseArmor = 100};
        
        public float BaseHp = 100;
        public float CurrentHP => BaseHp + _hp;
        public bool IsDead => CurrentHP <= 0;

        public float Hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                OnHpChanged?.Invoke();
            }
        }

        public UnityAction<Vector3> OnMove;
        public UnityAction<Vector2> OnRotate;
        public UnityAction<Vector3> OnSetSummaryRotation;

        private Vector3 _moveVector;
        private Vector2 _rotateVector;

        private Quaternion _summaryRotation;
        private float _hp;

        public Quaternion SummaryRotation
        {
            get { return _summaryRotation; }
            set
            {
                _summaryRotation = value;
                OnSetSummaryRotation?.Invoke(_summaryRotation.eulerAngles);
            }
        }

        public Vector3 SummaryRotationVector
        {
            get { return _summaryRotation.eulerAngles; }
            set { _summaryRotation.eulerAngles = value; }
        }


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