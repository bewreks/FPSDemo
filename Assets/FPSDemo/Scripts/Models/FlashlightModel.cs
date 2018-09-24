using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{

    public class FlashlightModel : BaseModel
    {
        public UnityAction<bool> OnSwitch;
        public UnityAction<float> OnIntensityChange;
        public UnityAction<float> OnTimerChange;

        public float MinRate = 0.2f;
        public AnimationCurve IntensityCurve;

        [SerializeField] private float _maxTimer;
        [SerializeField] private float _baseIntensity;

        private bool _isOn;
        private float _intensity;
        private float _timer;

        protected void Awake()
        {
            _isOn = false;
            _intensity = _baseIntensity;
            _timer = 0;
            base.Awake();
        }

        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                _isOn = value;
                OnSwitch?.Invoke(_isOn);
            }
        }

        public float Intensity
        {
            get { return _intensity + _baseIntensity; }
            set
            {
                _intensity = value;
                OnIntensityChange?.Invoke(Intensity);
            }
        }

        public float Timer
        {
            get { return _timer; }
            set
            {
                _timer = value;
                OnTimerChange?.Invoke(TimerCoef);
            }
        }

        public float TimerCoef => (_maxTimer + _timer) / _maxTimer;
    }
}