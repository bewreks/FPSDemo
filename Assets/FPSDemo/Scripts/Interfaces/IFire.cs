using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public interface IFire
    {
        void Fire(float force, GameObject owner);
    }
}