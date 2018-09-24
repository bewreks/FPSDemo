using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class Torch : BaseSceneObject
    {
        protected override void OnAwake()
        {
            
        }

        private void FixedUpdate()
        {
            _light.intensity = 1 + _animationCurve.Evaluate(Time.time);
        }

        public void LightningOff()
        {
            _light.enabled = false;
            _particleSystem.Stop();
        }

        public void LightningOn()
        {
            _light.enabled = true;
            _particleSystem.Play();
        }
    }

}