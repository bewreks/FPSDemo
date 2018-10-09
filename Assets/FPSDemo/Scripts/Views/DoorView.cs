using UnityEngine;

namespace FPSDemo
{
    public class DoorView : BaseView<DoorModel>
    {
        private Transform _door;
        private float _time;

        private DoorState _state;

        private readonly DoorState _openState = new DoorState();
        private readonly DoorState _closeState = new DoorState();

        protected override void Initialize()
        {
            _door = transform.GetChild(0);
            _model.OnSwitch += OnSwitch;
            var startPosition = _door.transform.position;
            var destinition = startPosition + _model.Offset;

            _openState.Init(startPosition, destinition);
            _closeState.Init(destinition, startPosition);

            _state = _closeState;
        }

        private void OnSwitch()
        {
            _state = _model.IsOpened ? _openState : _closeState;
            _time = _state.UpdateTime(_time);
        }

        private void Update()
        {
            if (_time <= 0)
            {
                return;
            }

            _time -= Time.deltaTime / (_model.TranslateTime == 0f ? Time.deltaTime : _model.TranslateTime);
            var transformPosition = _state.GetNextPosition(_time);
            _door.transform.position = transformPosition;
        }
    }
}