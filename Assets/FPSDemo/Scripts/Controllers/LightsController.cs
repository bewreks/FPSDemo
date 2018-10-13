using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class LightsController : BaseController<LightsModel>
    {
        private static bool _isOn;
        private static UnityAction OnChange;

        public static bool IsOn
        {
            get { return _isOn; }
            set
            {
                _isOn = value; 
                OnChange?.Invoke();
            }
        }

        protected override void Initialize()
        {
            var lights = GetComponentsInChildren<Light>();
            foreach (var light in lights)
            {
                if (light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed) 
                {
                    _model.Lights.Add(light);
                }
            }
            OnChange += OnChangeLights;
        }

        private void OnChangeLights()
        {
            _model.Lights.ForEach(light => light.enabled = _isOn);
        }
    }
}