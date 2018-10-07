using UnityEngine;

namespace FPSDemoEditor.Waypoints
{
    internal abstract class WaypointCreationBehaviourState
    {
        private static readonly WaypointCircleCreationBehaviourState CircleCreation = new WaypointCircleCreationBehaviourState();
        private static readonly WaypointRandomCreationBehaviourState RandomCreation = new WaypointRandomCreationBehaviourState();

        public static WaypointCreationBehaviourState GetState(WaypointsCreationBehaviourEnum @enum)
        {
            switch (@enum)
            {
                case WaypointsCreationBehaviourEnum.Random:
                    return RandomCreation;
                case WaypointsCreationBehaviourEnum.Circle:
                default:
                    return CircleCreation;
            }
        }

        public abstract void Show();
        public abstract Vector3 GetPosition(int pos, int count);
    }
}