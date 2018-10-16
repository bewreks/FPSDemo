﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public interface IWeapon
    {
        UnityAction OnFireEnd { get; set; }
        UnityAction<GameObject> OnBulletInstatiate { get; set; }
        GameObject GameObject { get; }
        
        bool Fire();
        void Reload();
        void TakeAim();
        void RealizeAim();
        bool IsActive();
        void SetOwner(GameObject owner);
    }
}