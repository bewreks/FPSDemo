using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class ThrowableAmmoModel : BaseAmmoModel
    {
        public UnityAction OnExplosion;
        
        public float DamageRadius;
        public float FuseTimeout;
    }
}