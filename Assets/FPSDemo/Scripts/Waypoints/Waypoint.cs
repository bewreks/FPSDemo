using System;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    [Serializable]
    public class Waypoint : MonoBehaviour
    {
        public float WaitTime;
        public UnityEvent OnReach;
    }
}