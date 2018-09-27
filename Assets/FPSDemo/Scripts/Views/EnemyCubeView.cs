using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class EnemyCubeView : BaseView<EnemyModel>
    {
        protected EnemyCubeSceneObject Cube;
        
        protected override void Initialize()
        {
            Cube = GetComponent<EnemyCubeSceneObject>();
            _model.OnHpChanged += OnHpChanged;
        }

        private void OnHpChanged()
        {
            Cube.Material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            if (_model.IsDead)
            {
                Cube.Collider.enabled = false;
            }
        }
    }
}