using System;
using UnityEngine;

namespace FPSDemo
{
    public class EnemyController : BaseController<EnemyModel>, IDamagable
    {
        protected override void Initialize()
        {
            
        }

        public void DoDamage(float damage)
        {
            if (_model.IsDead)
            {
                return;
            }
            _model.Hp -= _model.Armor.CalculateDamage(damage);
        }
    }
}