using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace FPSDemo.MP
{
    [RequireComponent(typeof(NetworkPlayerModel))]
    public class NetworkPlayerController : NetworkBehaviour, IDamagable
    {
        private NetworkPlayerModel _model;

        private IWeapon _weapon;
        private bool _controlable;

        private void Awake()
        {
            _model = GetComponent<NetworkPlayerModel>();
            _model.OnHpChangedEvent += OnHpChangedEvent;

            _weapon = GetComponentInChildren<IWeapon>();
            _weapon.OnBulletInstatiate += OnBulletInstatiate;
            _weapon.SetOwner(gameObject);
            _weapon.OnFireEnd += OnFireEnd;
            _controlable = true;
        }

        private void OnBulletInstatiate(GameObject bullet)
        {
            NetworkServer.Spawn(bullet);
        }

        private void OnHpChangedEvent()
        {
            var renderer = GetComponentInChildren<Renderer>();
            if (renderer)
            {
                renderer.material.color = Color.Lerp(Color.white, new Color(1, 0.5f, 0), 1 - _model.HpRatio);
            }
        }

        private void OnFireEnd()
        {
            _controlable = true;
        }

        private void Start()
        {
            if (isLocalPlayer)
            {
                var cameraTransform = Camera.main.transform;
                cameraTransform.parent = transform;
                cameraTransform.localPosition = _model.PlayCamera.CameraPosition;
                cameraTransform.localEulerAngles = _model.PlayCamera.CameraRotation;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void OnDisable()
        {
            if (isLocalPlayer)
            {
                if (Camera.main && Camera.main.isActiveAndEnabled)
                {
                    var cameraTransform = Camera.main.transform;
                    cameraTransform.parent = null;
                    cameraTransform.localPosition = _model.LobbyCamera.CameraPosition;
                    cameraTransform.localEulerAngles = _model.LobbyCamera.CameraRotation;
                }


                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void Move(float x, float y, float deltaTime)
        {
            if (_model.IsDead)
            {
                return;
            }

            var movement = new Vector3(x, 0, y) * _model.Speed;
            movement = Vector3.ClampMagnitude(movement, _model.Speed);

            movement.y = _model.Gravity;

            movement *= deltaTime;
            movement = transform.TransformDirection(movement);
            _model.MoveVector = movement;
        }

        public void Rotate(float x, float y)
        {
            if (_model.IsDead)
            {
                return;
            }

            var rotationY = x * _model.HorizontalSensivity;
            var rotationX = y * _model.VerticalSensivity;

            _model.RotateVector = new Vector3(-rotationX, rotationY);
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            float axisX = 0;
            float axisY = 0;

            if (_controlable)
            {
                axisX = CrossPlatformInputManager.GetAxis("Horizontal");
                axisY = CrossPlatformInputManager.GetAxis("Vertical");
            }

            Move(axisX, axisY, Time.deltaTime);

            if (_controlable)
            {
                axisX = CrossPlatformInputManager.GetAxis("Mouse X");
                axisY = CrossPlatformInputManager.GetAxis("Mouse Y");
            }

            Rotate(axisX, axisY);

            if (_controlable && CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                CmdFire();
                _model.OnFire();
            }

            if (CrossPlatformInputManager.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }

        [Command]
        private void CmdFire()
        {
            if (_model.IsDead)
            {
                return;
            }

            OnFire();
        }

        private void OnFire()
        {
            if (_weapon.Fire())
            {
                _controlable = false;
                _model.OnFire();
            }
        }

        public void DoDamage(float damage, GameObject owner)
        {
            if (!isServer)
            {
                return;
            }

            if (_model.IsDead)
            {
                return;
            }

            if (owner == gameObject)
            {
                return;
            }

            _model.Hp -= damage;
        }
    }
}