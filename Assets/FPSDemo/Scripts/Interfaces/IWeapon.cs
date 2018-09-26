using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public interface IWeapon
    {
        void Fire();
        void Reload();
        bool IsActive();
        GameObject GameObject { get; }
    }
}