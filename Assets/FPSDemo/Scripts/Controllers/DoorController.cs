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
            _model.IsOpened = true;
            _counter++;
        }

        private void OnTriggerExit(Collider other)
        {
            _counter--;
            if (_counter == 0)
            {
                _model.IsOpened = false;
            }
        }
    }
}