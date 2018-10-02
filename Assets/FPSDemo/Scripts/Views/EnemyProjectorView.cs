using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class EnemyProjectorView : BaseView<EnemyModel>
    {
        protected override void Initialize()
        {
            _model.OnTurn += transform.LookAt;
        }
    }
}