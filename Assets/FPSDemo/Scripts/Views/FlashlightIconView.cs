using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace FPSDemo
{
	public class FlashlightIconView : BaseView<FlashlightModel>
	{
		[SerializeField] private Image _background;
		private Image _image;

		private bool _isOn;
		private float _coef;

		protected override void Initialize()
		{
			_image = GetComponent<Image>();
			_model.OnSwitch += OnSwitch;
			_model.OnTimerChange += OnTimerChange;
			_coef = _model.TimerCoef;
			_isOn = _model.IsOn;
			UpdateView();
		}

		private void OnTimerChange(float coef)
		{
			_coef = coef;
			UpdateView();
		}

		private void OnSwitch(bool isOn)
		{
			_isOn = isOn;
			UpdateView();
		}

		private void UpdateView()
		{
			_image.fillAmount = _coef;

			if (_coef <= _model.MinRate)
			{
				_image.color = Color.red;
			}
			else
			{
				_image.color = Color.white;
			}

			if (_isOn)
			{
				_image.enabled = _isOn;
			}
			else
			{
				_image.enabled = _coef < 1;
			}

			_background.enabled = _image.enabled;

		}
	}
}
