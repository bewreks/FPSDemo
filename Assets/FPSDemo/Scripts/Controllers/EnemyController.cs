using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FPSDemo
{
    [RequireComponent(typeof(WaypointsController))]
    public class EnemyController : BaseController<EnemyModel>, IDamagable
    {
        private WaypointsController _waypointsController;
        
        protected override void Initialize()
        {
            _waypointsController = GetComponent<WaypointsController>();
            
            _model.NavAgent = GetComponent<NavMeshAgent>();
            _model.IsAttacking = false;
        }

        public void DoDamage(float damage)
        {
            if (_model.IsDead)
            {
                return;
            }
            _model.Hp -= _model.Armor.CalculateDamage(damage);
        }

        public void Move(Vector3 position)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 50f, -1))
            {
                if (_model.NavAgent)
                {
                    _model.NavAgent.SetDestination(hit.position);
                }
            }
        }

        public void Attack()
        {
            if (!_model.IsAttacking)
            {
                _model.OnAttack?.Invoke();
                _model.IsAttacking = true;
                Invoke("OnAttackEnd", _model.AttackTime);
            }
        }

        public void OnAttackEnd()
        {
            _model.IsAttacking = false;
        }

        private void Update()
        {
            switch (_model.Behaviour)
            {
                case EnemyBehaviour.RANDOM_PATROL:
                    if (!_model.NavAgent.hasPath || _model.NavAgent.remainingDistance <= _model.NavAgent.stoppingDistance)
                    {
                        Move(GetRandomPoint());
                    }
                    break;
                case EnemyBehaviour.PATROL:
                    if (!_model.NavAgent.hasPath || _model.NavAgent.remainingDistance <= _model.NavAgent.stoppingDistance)
                    {
                        _waypointsController.Next();
                        Move(_waypointsController.Waypoint.transform.position);
                    }
                    break;
                case EnemyBehaviour.TRACKING:
                    Move(transform.position);
                    if (_model.TrackingObject)
                    {
                        _model.OnTurn?.Invoke(_model.TrackingObject.position);
                    }
                    break;
                case EnemyBehaviour.CHASING:
                    if (_model.TrackingObject)
                    {
                        Move(_model.TrackingObject.position);
                    }
                    break;
                case EnemyBehaviour.STAY:
                    Move(transform.position);
                    break;
                    
            }
        }

        private Vector3 GetRandomPoint()
        {
            return Random.insideUnitSphere * _model.MaxRandomSphereSize;
        }

        public void TrackingTarget(Transform trackingObject)
        {
            _model.TrackingObject = trackingObject;
        }
    }
}