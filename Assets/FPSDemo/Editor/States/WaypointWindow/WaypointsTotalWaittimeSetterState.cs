using UnityEditor;

namespace FPSDemoEditor.Waypoints
{
    internal class WaypointsTotalWaittimeSetterState : WaypointsWaittimeSetterState
    {
        private float _waitTime;
        
        public override void Show()
        {
            _waitTime = EditorGUILayout.FloatField(" Wait time", _waitTime);
        }

        public override float GetWaitTime()
        {
            return _waitTime;
        }
    }
}