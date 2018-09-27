using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public interface IWeapon
    {
        void Fire();
        void Reload();
        void TakeAim();
        void RealizeAim();
        bool IsActive();
        GameObject GameObject { get; }
    }
}