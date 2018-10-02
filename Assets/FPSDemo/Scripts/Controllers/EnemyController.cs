using System;
using UnityEngine;
using UnityEngine.AI;

namespace FPSDemo
{
    public class EnemyController : BaseController<EnemyModel>, IDamagable
    {
        private bool can;
        
        protected override void Initialize()
        {
            _model.NavAgent = GetComponent<NavMeshAgent>();
            can = true;
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
                _model.NavAgent.SetDestination(hit.position);
            }
        }

        public void Attack()
        {
            if (can)
            {
                _model.OnAttack?.Invoke();
                can = false;
                Invoke("ResetCan", 0.5f);
            }
        }

        public void ResetCan()
        {
            can = true;
        }
    }
}