using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Waypoints
{
    internal class WaypointsRandomWaittimeSetterState : WaypointsWaittimeSetterState
    {
        private float _min;
        private float _max;
        
        public override void Show()
        {
            EditorGUILayout.MinMaxSlider(" Wait time", ref _min, ref _max, 0, 180);
            _min = EditorGUILayout.FloatField(" Min", _min);
            _max = EditorGUILayout.FloatField(" Max", _max);
        }

        public override float GetWaitTime()
        {
            return Random.Range(_min, _max);
        }
    }
}