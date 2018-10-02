using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class TestMainScript : MonoBehaviour
    {
        public Camera Camera;
        public EnemyController enemy;
        
        private void Start()
        {
            enemy = FindObjectOfType<EnemyController>();
        }

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                var screenPointToRay = Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(screenPointToRay, out hit))
                {
                    //enemy.Move(hit.point);
                    enemy.TrackingTarget(hit.collider.gameObject.transform);
                }
            }

            if (Input.GetButton("Fire2"))
            {
                enemy.Attack();
            }
        }
    }
}