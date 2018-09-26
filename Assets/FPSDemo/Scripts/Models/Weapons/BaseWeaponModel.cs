using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class BaseWeaponModel : BaseModel
    {
        public UnityAction OnShoot;
        
        public GameObject AmmoPrefab;
        
        public float Power;
        public float Timeout;
        
        public float LastShootTime;
        public int BulletsCountCurrent;
        
        public bool IsTimeout => Time.time < LastShootTime + Timeout;
        public bool IsEmpty => BulletsCountCurrent <= 0;
        
    }
}