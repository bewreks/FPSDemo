using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class DoorModel : BaseModel
    {
        public UnityAction OnSwitch;
        
        public Vector3 Offset;
        private bool _isOpened;

        public bool IsOpened
        {
            get { return _isOpened; }
            set
            {
                if (_isOpened != value)
                {
                    _isOpened = value;
                    OnSwitch?.Invoke();
                }
            }
        }
    }
}