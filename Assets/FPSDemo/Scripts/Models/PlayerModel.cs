using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class PlayerModel : BaseModel
    {
        public List<IWeapon> Weapons;
        public int CurrentWeapon = 0;

        public float SwitchWeaponTimeout = 0.5f;
        public float LastSwitchWeapon;

        public IWeapon CurrentWeaponController => Weapons[CurrentWeapon];
        public bool IsTimeout => Time.time < LastSwitchWeapon + SwitchWeaponTimeout;
        public int WeaponsCount => Weapons.Count - 1;
    }
}