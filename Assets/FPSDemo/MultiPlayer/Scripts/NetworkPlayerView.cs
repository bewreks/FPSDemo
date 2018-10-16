using UnityEngine;
using UnityEngine.Networking;

namespace FPSDemo.MP
{
    public class NetworkPlayerView : NetworkBehaviour
    {
        private NetworkPlayerModel _model;
        private CharacterController _char;
        private Animator _animator;

        private void Awake()
        {
            _model = GetComponent<NetworkPlayerModel>();
            _model.OnMove += OnMove;
            _model.OnRotate += OnRotate;
            _model.OnFire += OnFire;
            _model.OnHpChangedEvent += OnHpChangedEvent;
            
            _char = GetComponent<CharacterController>();

            _animator = GetComponent<Animator>();
        }

        private void OnHpChangedEvent()
        {
            if (_model.IsDead)
            {
                _animator.SetBool("IsDead", true);
                _char.enabled = false;
            }
        }

        private void OnFire()
        {
            _animator.SetTrigger("Attack");
        }

        private void OnRotate(Vector2 rotation)
        {
            transform.Rotate(0, rotation.y, 0);
        }

        private void OnMove(Vector3 movement)
        {
            _char.Move(movement);
            movement.y = 0;
            _animator.SetFloat("Speed", movement.magnitude);
        }
    }
}