using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class DoorView : BaseView<DoorModel>
    {
        protected Transform Door;
        
        protected override void Initialize()
        {
            _model.OnSwitch += OnSwitch;
            Door = transform.GetChild(0);
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
            Door.Translate(-_model.Offset);
        }

        private void Open()
        {
            Door.Translate(_model.Offset);
        }
    }
}