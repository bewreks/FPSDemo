using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class FirearmsWeaponModel : BaseWeaponModel
    {
        public UnityAction OnEmptyShoot;
        public UnityAction OnReload;
        public UnityAction OnEmptyReload;
        public UnityAction OnTakeAim;
        public UnityAction OnRealizeAim;
        
        public float MuzzleSpeed;
        public float ReloadTime;
        
        public int BulletsCountMax;

        public float StartReloadTime;

        public AnimationCurve HorizontalBob;
        public AnimationCurve VerticalBob;

        private bool _isAim;

        public bool IsAim
        {
            get { return _isAim; }
            set
            {
                _isAim = value;
                if (_isAim)
                {
                    OnTakeAim?.Invoke();
                }
                else
                {
                    OnRealizeAim?.Invoke();
                }
            }
        }

        public bool IsReloading => Time.time < StartReloadTime + ReloadTime;
        public bool IsFull => BulletsCountCurrent >= BulletsCountMax;

    }
}