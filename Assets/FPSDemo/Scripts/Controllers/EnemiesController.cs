using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class EnemiesController : BaseController<EnemiesModel>, ISerializable
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

        public string SerializedName => "Enemies";
        public SerializableObject Serialize()
        {
            // Костыли, потому что класс должен переделываться на более расширяемый и функциональный
            var serializableObject = new SerializableObject(SerializedName);
            serializableObject.AddSerialable(_model.Projector.Serialize());
            serializableObject.AddSerialable(_model.ProjectorTracker.Serialize());
            serializableObject.AddSerialable(_model.Robot.Serialize());
            return serializableObject;
        }

        public void Unserialize(SerializableObject serializableObject)
        {
            _model.Projector.Unserialize(serializableObject.GetSerialable(_model.Projector.SerializedName));
            _model.ProjectorTracker.Unserialize(serializableObject.GetSerialable(_model.ProjectorTracker.SerializedName));
            _model.Robot.Unserialize(serializableObject.GetSerialable(_model.Robot.SerializedName));
        }
    }
}