using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class FlashlightControllerGB : MonoBehaviour
    {
        protected FlashlightModelGB _model;

        public bool IsEnabled { get; private set; }
        public void On()
        {
            IsEnabled = true;
            _model.On();
        }

        public void Off()
        {
            IsEnabled = false;
            _model.Off();
        }

        
        protected void Awake()
        {
            _model = FindObjectOfType<FlashlightModelGB>();
            Off();
        }

        public void Switch()
        {
            if (IsEnabled)
                Off();
            else
                On();
        }
    }
}