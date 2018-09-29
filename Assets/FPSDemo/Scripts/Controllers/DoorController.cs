using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class DoorController : BaseController<DoorModel>
    {
        private int _counter;
        
        protected override void Initialize()
        {
            _counter = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Enter {_model.IsOpened}");
            _model.IsOpened = true;
            _counter++;
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"Exit {_model.IsOpened}");
            _counter--;
            if (_counter == 0)
            {
                _model.IsOpened = false;
            }
        }
    }
}