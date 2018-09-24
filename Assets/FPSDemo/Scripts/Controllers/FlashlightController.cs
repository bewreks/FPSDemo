using System.Collections;
using FPSDemo;
using UnityEngine;

namespace FPSDemo
{
    public class FlashlightController : BaseController<FlashlightModel>
    {
        private const float Rate = 0.01f;

        protected override void Initialize()
        {
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
            if (_model.TimerCoef <= _model.MinRate)
            {
                var time = _model.TimerCoef * (1 / _model.MinRate);
                _model.Intensity = _model.IntensityCurve.Evaluate(time);
            }
            else
            {
                _model.Intensity = 0;
            }

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
}