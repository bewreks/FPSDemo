﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class FlashlightView : BaseView<FlashlightModel>
    {
        protected Light _light;

        protected override void Initialize()
        {
            _light = GetComponent<Light>();
            _model.OnSwitch += OnSwitch;
            _model.OnIntensityChange += OnIntensityChange;
            OnIntensityChange(_model.Intensity);
            OnSwitch(_model.IsOn);
        }

        private void OnIntensityChange(float intensity)
        {
            _light.intensity = intensity;
        }

        private void OnSwitch(bool isOn)
        {
            _light.enabled = isOn;
        }
    }
}