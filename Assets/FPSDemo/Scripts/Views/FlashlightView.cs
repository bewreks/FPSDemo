using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightView : BaseView<FlashlightModel>
{
    protected Light _light;
    
    protected override void Awake()
    {
        _light = GetComponent<Light>();
        base.Awake();
    }

    protected override void InitializeView()
    {
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