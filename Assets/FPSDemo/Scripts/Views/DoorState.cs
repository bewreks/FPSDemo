using UnityEngine;

namespace FPSDemo
{
    internal class DoorState
    {
        private Vector3 _startPosition;
        private Vector3 _endPosition;

        public void Init(Vector3 start, Vector3 end)
        {
            _startPosition = start;
            _endPosition = end;
        }

        public float UpdateTime(float time)
        {
            return 1 - time;
        }

        public Vector3 GetNextPosition(float time)
        {
            return Vector3.Lerp(_startPosition, _endPosition, 1 - time);
        }
    }
}