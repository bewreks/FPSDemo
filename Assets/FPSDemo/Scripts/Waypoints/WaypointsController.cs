using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class WaypointsController : MonoBehaviour
    {
        public Waypoint[] Waypoints;
        private int _currentWaypoint = -1;

        public Waypoint Waypoint => Waypoints[_currentWaypoint];

        public float CoolDown = 0;

        public void Next()
        {
            if (CoolDown <= 0f)
            {
                if (++_currentWaypoint >= Waypoints.Length)
                {
                    _currentWaypoint = 0;
                }

                CoolDown = Waypoint.WaitTime;
            }
            else
            {
                CoolDown -= Time.deltaTime;
            }
        }
    }
}