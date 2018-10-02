using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace FPSDemo
{
    public enum EnemyBehaviour
    {
        PATROL,
        RANDOM_PATROL,
        CHASING,
        TRACKING,
        STAY
    }
    
    public class EnemyModel : BaseModel
    {
        public UnityAction OnHpChanged;
        public UnityAction OnAttack;
        public UnityAction<Vector3> OnTurn;

        public NavMeshAgent NavAgent;

        public EnemyBehaviour Behaviour = EnemyBehaviour.RANDOM_PATROL;
        public float MaxRandomSphereSize = 1;
        public Transform TrackingObject;

        public float BaseHp = 100;
        [SerializeField]public Armor Armor = new Armor{ArmorType = 1, BaseArmor = 0};

        private float _hp;

        public float AttackTime = 0.5f;
        public bool IsAttacking; 

        public float CurrentHP => BaseHp + _hp;
        public bool IsDead => CurrentHP <= 0;

        public float Hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                OnHpChanged?.Invoke();
            }
        }
    }
}