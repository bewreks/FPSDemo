using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class WeaponModel : BaseModel
    {
        public UnityAction OnReloaded;
        public UnityAction OnEmptyShoot;
        public UnityAction OnShoot;
        public UnityAction OnEmptyReload;
        public UnityAction OnReload;

        public GameObject AmmoPrefab;
        
        public float MuzzleSpeed;
        public float Timeout;
        public float ReloadTime;
        
        public int BulletsCountMax;
        public int BulletsCountCurrent;

        public float LastShootTime;
        public float StartReloadTime;

        public bool IsReloading => Time.time < StartReloadTime + ReloadTime;
        public bool IsTimeout => Time.time < LastShootTime + Timeout;
        public bool IsEmpty => BulletsCountCurrent <= 0;
        public bool IsFull => BulletsCountCurrent >= BulletsCountMax;
    }
}