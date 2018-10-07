namespace FPSDemoEditor.Waypoints
{
    internal abstract class WaypointsWaittimeSetterState
    {
        private static readonly WaypointsWaittimeSetterState TotalState = new WaypointsTotalWaittimeSetterState();
        private static readonly WaypointsWaittimeSetterState RandomState = new WaypointsRandomWaittimeSetterState();
        
        public static WaypointsWaittimeSetterState GetState(WaypointsWaittimeSetterEnum waittimeSetter)
        {
            switch (waittimeSetter)
            {
                case WaypointsWaittimeSetterEnum.Random:
                    return RandomState;
                case WaypointsWaittimeSetterEnum.Total:
                default:
                    return TotalState;
            }
        }
        
        public abstract void Show();
        public abstract float GetWaitTime();
    }
}