using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace FPSDemo.MP
{
    [Serializable]
    public struct Transforms
    {
        public Vector3 CameraPosition;
        public Vector3 CameraRotation;
    }
    
    public class NetworkPlayerModel : NetworkBehaviour
    {
        public UnityAction OnHpChangedEvent;
        public UnityAction<Vector3> OnMove;
        public UnityAction<Vector2> OnRotate;
        public UnityAction OnFire;

        private Vector3 _moveVector;
        private Vector2 _rotateVector;
        
        public float HorizontalSensivity = 2;
        public float VerticalSensivity = 2;

        public float Speed = 10;
        public float Gravity = 9.8f;

        public float MaxHp;
        
        public Transforms PlayCamera = new Transforms
        {
            CameraPosition = new Vector3(0, 2.39f, -1.5f),
            CameraRotation = new Vector3(21.12f, 0, 0)
        };
        
        public Transforms LobbyCamera = new Transforms
        {
            CameraPosition = new Vector3(3.58f, 2.37f, -1.5f),
            CameraRotation = new Vector3(0, 144.27f, 0)
        };

        [SerializeField] [SyncVar(hook = "OnHpChanged")]
        private float _hp;

        public float CurrentHP => MaxHp + Hp;
        public bool IsDead => CurrentHP <= 0;
        public float HpRatio => CurrentHP / MaxHp;

        public GameObject BulletPrefab;
        public Transform Firepoint;

        public float Hp
        {
            get { return _hp; }
            set { OnHpChanged(value); }
        }

        private void OnHpChanged(float hp)
        {
            _hp = hp;
            OnHpChangedEvent?.Invoke();
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