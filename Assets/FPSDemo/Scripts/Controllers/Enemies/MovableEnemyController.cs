using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FPSDemo
{
    [RequireComponent(typeof(WaypointsController))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovableEnemyController : BaseEnemyController
    {
        private WaypointsController _waypointsController;
        private MeleeWeaponController _meleeWeaponController;

        protected override void Initialize()
        {
            _waypointsController = GetComponent<WaypointsController>();
            _meleeWeaponController = GetComponentInChildren<MeleeWeaponController>();

            _model.NavAgent = GetComponent<NavMeshAgent>();
            base.Initialize();
        }

        public override void Move(Vector3 position)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 50f, -1))
            {
                _model.NavAgent.SetDestination(hit.position);
            }
        }

        protected override void OnRandomPatrol()
        {
            if (!_model.NavAgent.hasPath || _model.NavAgent.remainingDistance <= _model.NavAgent.stoppingDistance)
            {
                Move(GetRandomPoint());
            }

            Moving();
        }

        protected override void OnPatrol()
        {
            if (!_model.NavAgent.hasPath || _model.NavAgent.remainingDistance <= _model.NavAgent.stoppingDistance)
            {
                _waypointsController.Next();
                Move(_waypointsController.Waypoint.transform.position);
            }

            Moving();
        }

        protected override void OnTracking()
        {
            Move(transform.position);
            Moving();
            if (_model.TrackingObject)
            {
                _model.OnTurn?.Invoke(_model.TrackingObject.position);
            }
        }

        protected override void OnChasing()
        {
            if (_model.TrackingObject)
            {
                Move(_model.TrackingObject.position);
            }

            Moving(true);
        }

        protected override void OnStay()
        {
            Move(transform.position);
            Moving();
        }

        private void Moving(bool needAttack = false)
        {
            var velocity = Vector3.zero;
            if (_model.NavAgent.remainingDistance > _model.NavAgent.stoppingDistance)
            {
                velocity = _model.NavAgent.desiredVelocity;
            }
            else
            {
                if (needAttack && _model.TrackingObject != null)
                {
                    Attack();
                    _meleeWeaponController.Fire();
                }
            }

            _model.OnSpeedChanged?.Invoke(velocity.magnitude);
        }
    }
}