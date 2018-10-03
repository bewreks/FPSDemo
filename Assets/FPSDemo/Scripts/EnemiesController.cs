using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    
    // Изначально робот не активен.
    // Если игрока заметит прожектор, тогда робот влючает режим преследования
    public class EnemiesController : MonoBehaviour
    {
        public StaticEnemyController Projector;
        public MovableEnemyController Robot;

        private void Awake()
        {
            if (!Projector.IsInited)
            {
                Projector.OnInit += OnProjectorInit;
            }
            else
            {
                OnProjectorInit();
            }
        }

        private void OnProjectorInit()
        {
            Projector.OnPlayerFound += OnPlayerFound;
        }

        private void OnPlayerLost(GameObject player)
        {
            Projector.SetPreviousTarget();
            Robot.SetPreviousTarget();
            Robot.SetPreviousBehaviour();
            Projector.OnPlayerFound += OnPlayerFound;
            Projector.OnPlayerLost -= OnPlayerLost;
        }

        private void OnPlayerFound(GameObject player)
        {
            Projector.OnPlayerFound -= OnPlayerFound;
            Projector.OnPlayerLost += OnPlayerLost;
            Projector.SetNewTarget(player.transform);
            Robot.SetNewTarget(player.transform);
            Robot.SetNewBehaviour(EnemyBehaviour.CHASING);
        }
    }
}