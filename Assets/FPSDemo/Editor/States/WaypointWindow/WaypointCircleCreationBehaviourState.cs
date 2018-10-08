using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Waypoints
{
    internal class WaypointCircleCreationBehaviourState : WaypointCreationBehaviourState
    {
        private float _radius = 10;

        public override void Show()
        {
            _radius = EditorGUILayout.Slider(" Radius of circle", _radius, 10, 50);
        }

        public override Vector3 GetPosition(int pos, int count)
        {
            var angle = pos * Mathf.PI * 2 / count;
            return new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _radius;
        }
    }
}