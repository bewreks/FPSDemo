using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace FPSDemo
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class TeammateModel : BaseModel
    {
        public UnityAction OnSwtichBehaviour;
        
        public Vector3 TargetPostion;
        public GameObject Target;
        public NavMeshAgent NavAgent;

        private bool _toPosition;

        public bool ToPosition
        {
            get { return _toPosition; }
            set
            {
                _toPosition = value; 
                OnSwtichBehaviour?.Invoke();
            }
        }

        protected override void OnAwake()
        {
            NavAgent = GetComponent<NavMeshAgent>();
//            Target = Main.Instance.PlayerController.gameObject;
        }
    }
}