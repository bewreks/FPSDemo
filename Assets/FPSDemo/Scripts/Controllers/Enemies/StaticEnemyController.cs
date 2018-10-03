using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class StaticEnemyController : BaseEnemyController
    {
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