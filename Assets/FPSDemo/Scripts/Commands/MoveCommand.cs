using UnityEngine;

namespace FPSDemo
{
    internal class MoveCommand : Command
    {
        private float _x;
        private float _y;

        private float _deltaTime;

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public float DeltaTime
        {
            get { return _deltaTime; }
            set { _deltaTime = value; }
        }
        
        protected override void InternalExecute()
        {
            Main.Instance.PlayerController.Move(_x, _y, _deltaTime);            
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }

        public void Initialize(float x, float y)
        {
            _x = x;
            _y = y;
            _deltaTime = Time.deltaTime;
        }
    }
}