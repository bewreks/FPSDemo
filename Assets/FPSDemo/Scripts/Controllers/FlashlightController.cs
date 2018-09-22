using System.Collections;
using FPSDemo;
using UnityEngine;

public class FlashlightController : BaseController<FlashlightModel>
{
    private const float Rate = 0.01f;
    
    protected override void Awake()
    {
        _model = FindObjectOfType<FlashlightModel>();
        Off();
    }

    public override void Off()
    {
        base.Off();
        _model.IsOn = false;
        CancelInvoke("ChargeReduction");
        InvokeRepeating("ChargeIncrease", 0, Rate);
    }

    public override void On()
    {
        if (_model.TimerCoef <= _model.MinRate)
        {
            return;
        }
        base.On();
        _model.IsOn = true;
        CancelInvoke("ChargeIncrease");
        InvokeRepeating("ChargeReduction", 0, Rate);
    }

    public void Switch()
    {
        if (IsEnabled)
            Off();
        else
            On();
    }

    public void ChargeReduction()
    {
        // TODO: изменение интенсивности
        if (_model.TimerCoef <= 0)
        {
            CancelInvoke("ChargeReduction");
            Off();
        }
        else
        {
            _model.Timer -= Rate;
        }
    }

    public void ChargeIncrease()
    {
        if (_model.TimerCoef >= 1)
        {
            CancelInvoke("ChargeIncrease");
        }
        else
        {
            _model.Timer += Rate;
        }
    }
}