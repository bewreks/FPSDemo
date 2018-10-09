using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class EnemiesController : BaseController<EnemiesModel>
    {
        protected override void Initialize()
        {
            if (!_model.Projector.IsInited)
            {
                _model.Projector.OnInit += OnProjectorInit;
            }
            else
            {
                OnProjectorInit();
            }
        }

        private void OnProjectorInit()
        {
            _model.Projector.OnPlayerFound += OnPlayerFound;
        }

        private void OnPlayerLost(GameObject player)
        {
            _model.Projector.SetPreviousTarget();
            _model.Robot.SetPreviousTarget();
            _model.Robot.SetPreviousBehaviour();
            _model.Projector.OnPlayerFound += OnPlayerFound;
            _model.Projector.OnPlayerLost -= OnPlayerLost;
        }

        private void OnPlayerFound(GameObject player)
        {
            _model.Projector.OnPlayerFound -= OnPlayerFound;
            _model.Projector.OnPlayerLost += OnPlayerLost;
            _model.Projector.SetNewTarget(player.transform);
            _model.Robot.SetNewTarget(player.transform);
            _model.Robot.SetNewBehaviour(EnemyBehaviour.CHASING);
        }
    }
}