using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Waypoints
{
    internal class WaypointRandomCreationBehaviourState : WaypointCreationBehaviourState
    {
        private float _max = 10;
        private float _min = -10;

        public override void Show()
        {
            
            EditorGUILayout.MinMaxSlider(" Random position", ref _min, ref _max, -50, 50);
            _min = EditorGUILayout.FloatField(" Min", _min);
            _max = EditorGUILayout.FloatField(" Max", _max);
        }

        public override Vector3 GetPosition(int pos, int count)
        {
            return new Vector3(Random.Range(_min, _max), 0, Random.Range(_min, _max));
        }
    }
}