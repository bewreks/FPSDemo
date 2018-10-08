using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class DoorView : BaseView<DoorModel>
    {
        public float TranslateTime = 1;
        
        protected Transform Door;
        public float _time;

        private Vector3 _startPosition;
        private Vector3 _destinition;
        
        protected override void Initialize()
        {
            Door = transform.GetChild(0);
            _model.OnSwitch += OnSwitch;
            _destinition = Door.transform.position;
            _startPosition = _destinition + _model.Offset;
        }

        private void OnSwitch()
        {
            if (_model.IsOpened)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        private void Close()
        {
            SetTime();
            Swap();
        }

        private void Open()
        {
            SetTime();
            Swap();
        }

        private void SetTime()
        {
            _time = 1 - _time;
        }

        private void Swap()
        {
            var temp = _destinition;
            _destinition = _startPosition;
            _startPosition = temp;
        }

        private void Update()
        {
            if (_time <= 0)
            {
                return;
            }
            _time -= (Time.deltaTime / TranslateTime);
            var transformPosition = Vector3.Lerp(_startPosition, _destinition, 1 - _time);
            Debug.Log($"{transformPosition} - {1 - _time}");
            Door.transform.position = transformPosition;
        }
    }
}