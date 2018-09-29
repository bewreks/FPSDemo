using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace FPSDemo
{
    public class TeammateView : BaseView<TeammateModel>
    {
        ThirdPersonCharacter _character;
        
        protected override void Initialize()
        {
            _character = GetComponent<ThirdPersonCharacter>();
        }

        private void Update()
        {
            if (_model.ToPosition)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(_model.TargetPostion, out hit, 50f, -1))
                {
                    _model.NavAgent.SetDestination(hit.position);
                }
                
            }
            else
            {
                if (_model.Target)
                {
                    _model.NavAgent.SetDestination(_model.Target.transform.position);
                }
            }
            
            if (_model.NavAgent.remainingDistance > _model.NavAgent.stoppingDistance)
                _character.Move(_model.NavAgent.desiredVelocity, false, false);
            else
                _character.Move(Vector3.zero, false, false);
        }
    }
}