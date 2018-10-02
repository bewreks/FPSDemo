using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace FPSDemo
{
    public class EnemyModel : BaseModel
    {
        public UnityAction OnHpChanged;
        public UnityAction OnAttack;

        public NavMeshAgent NavAgent;

        public float BaseHp = 100;
        [SerializeField]public Armor Armor = new Armor{ArmorType = 1, BaseArmor = 0};

        private float _hp;

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