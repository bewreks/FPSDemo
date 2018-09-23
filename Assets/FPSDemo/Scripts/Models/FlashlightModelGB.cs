using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class FlashlightModelGB : MonoBehaviour
    {
        private Light _light;

        public bool IsOn
        {
            get
            {
                if (!_light) return false;
                return _light.enabled;
            }
        }

        private void Awake()
        {
            _light = GetComponent<Light>();
        }

        public void On()
        {
            _light.enabled = true;
        }

        public void Off()
        {
            _light.enabled = false;
        }

        public void Switch()
        {
            if (IsOn)
                Off();
            else
                On();
        }
    }
}