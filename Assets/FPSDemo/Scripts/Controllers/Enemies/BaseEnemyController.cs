using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseEnemyController : BaseController<EnemyModel>, IDamagable
    {
        private Transform _previousTarget;
        
        protected override void Initialize()
        {
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

        public void Attack()
        {
            if (_model.IsDead)
            {
                return;
            }
            if (!_model.IsAttacking)
            {
                _model.OnAttack?.Invoke();
                _model.IsAttacking = true;
                Invoke("OnAttackEnd", _model.AttackTime);
            }
        }

        public void SetNewTarget(Transform target)
        {
            _previousTarget = _model.TrackingObject;
            _model.TrackingObject = target;
        }

        public void SetPreviousTarget()
        {
            SetNewTarget(_previousTarget);
        }

        protected Vector3 GetRandomPoint()
        {
            return Random.insideUnitSphere * _model.MaxRandomSphereSize;
        }
        
        private void OnAttackEnd()
        {
            _model.IsAttacking = false;
        }

        private void Update()
        {
            if (_model.IsDead)
            {
                return;
            }
            
            switch (_model.Behaviour)
            {
                case EnemyBehaviour.RANDOM_PATROL:
                    OnRandomPatrol();
                    break;
                case EnemyBehaviour.PATROL:
                    OnPatrol();
                    break;
                case EnemyBehaviour.CHASING:
                    OnChasing();
                    break;
                case EnemyBehaviour.TRACKING:
                    OnTracking();
                    break;
                case EnemyBehaviour.STAY:
                    OnStay();
                    break;
                default:
                    OnUnknownBahaviour();
                    break;
            }
        }

        protected virtual void OnUnknownBahaviour()
        {
            Debug.LogError("Unknown behaviour");
        }

        public abstract void Move(Vector3 position);
        protected abstract void OnRandomPatrol();
        protected abstract void OnPatrol();
        protected abstract void OnTracking();
        protected abstract void OnChasing();
        protected abstract void OnStay();
    }
}