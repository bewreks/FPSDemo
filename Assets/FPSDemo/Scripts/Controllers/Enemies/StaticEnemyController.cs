using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class StaticEnemyController : BaseEnemyController
    {
        public UnityAction<GameObject> OnPlayerFound;
        public UnityAction<GameObject> OnPlayerLost;

        private PlayerTrigger _playerTrigger;

        public LayerMask Mask;

        protected override void Initialize()
        {
            base.Initialize();
            _playerTrigger = GetComponentInChildren<PlayerTrigger>();
            if (_playerTrigger)
            {
                _playerTrigger.OnPlayerTriggered += OnPlayerTriggered;
            }
        }

        private void OnPlayerTriggerOut(Collider playerCollider)
        {
            PlayerLost(playerCollider.gameObject, 1);
            _playerTrigger.OnPlayerTriggered += OnPlayerTriggered;
            _playerTrigger.OnPlayerTriggerStay -= OnPlayerTriggerStay;
            _playerTrigger.OnPlayerTriggerOut -= OnPlayerTriggerOut;
        }

        private void OnPlayerTriggerStay(Collider playerCollider)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, playerCollider.gameObject.transform.position, out hit, Mask))
            {
                if (hit.collider == playerCollider)
                {
                    OnPlayerFound?.Invoke(playerCollider.gameObject);
                }
                else
                {
                    PlayerLost(playerCollider.gameObject, 2);
                }
            }
            else
            {
                PlayerLost(playerCollider.gameObject, 3);
            }
        }

        private void PlayerLost(GameObject obj, int i)
        {
            Debug.Log($"Lost {i}");
            OnPlayerLost?.Invoke(obj);
        }

        private void OnPlayerTriggered(Collider playerCollider)
        {
            _playerTrigger.OnPlayerTriggered -= OnPlayerTriggered;
            _playerTrigger.OnPlayerTriggerStay += OnPlayerTriggerStay;
            _playerTrigger.OnPlayerTriggerOut += OnPlayerTriggerOut;
        }

        public override void Move(Vector3 position)
        {
        }

        protected override void OnRandomPatrol()
        {
        }

        protected override void OnPatrol()
        {
        }

        protected override void OnTracking()
        {
            if (_model.TrackingObject)
            {
                _model.OnTurn?.Invoke(_model.TrackingObject.position);
            }
        }

        protected override void OnChasing()
        {
        }

        protected override void OnStay()
        {
        }
    }
}