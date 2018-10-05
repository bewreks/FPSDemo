using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class TestMainScript : MonoBehaviour
    {
        public Camera Camera;
        public MovableEnemyController MovableEnemy;

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                var screenPointToRay = Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(screenPointToRay, out hit))
                {
                    //enemy.Move(hit.point);
                    MovableEnemy.SetNewTarget(hit.collider.gameObject.transform);
                }
            }

            if (Input.GetButton("Fire2"))
            {
                MovableEnemy.Attack();
            }
        }
    }
}