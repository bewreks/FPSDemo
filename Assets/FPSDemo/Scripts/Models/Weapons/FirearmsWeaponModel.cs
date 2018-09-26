using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class FirearmsWeaponModel : BaseWeaponModel
    {
        public UnityAction OnEmptyShoot;
        public UnityAction OnReload;
        public UnityAction OnEmptyReload;
        
        public float MuzzleSpeed;
        public float ReloadTime;
        
        public int BulletsCountMax;

        public float StartReloadTime;

        public bool IsReloading => Time.time < StartReloadTime + ReloadTime;
        public bool IsFull => BulletsCountCurrent >= BulletsCountMax;
    }
}