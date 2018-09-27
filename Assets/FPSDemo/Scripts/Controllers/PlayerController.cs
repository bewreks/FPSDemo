using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class PlayerController : BaseController<PlayerModel>
    {
        protected GameObject _camera;

        public IWeapon CurrentWeapon => _model.CurrentWeaponController;

        protected override void Initialize()
        {
            _model.Weapons = new List<IWeapon>();
            _camera = transform.GetChild(0).gameObject;
            for (int i = 0; i < _camera.transform.childCount; i++)
            {
                var child = _camera.transform.GetChild(i);
                if (child.CompareTag("Weapon"))
                {
                    _model.Weapons.Add(child.transform.GetComponent<IWeapon>());
                    _model.Weapons[_model.WeaponsCount].GameObject.SetActive(_model.WeaponsCount == _model.CurrentWeapon);
                }
            }
        }

        public void SwitchWeapon(bool direction)
        {
            if (_model.IsTimeout)
            {
                return;
            }

            _model.LastSwitchWeapon = Time.time;
            CurrentWeapon.GameObject.SetActive(false);
            if (direction)
            {
                if (++_model.CurrentWeapon >= _model.WeaponsCount + 1)
                {
                    _model.CurrentWeapon = 0;
                }
            }
            else
            {
                if (--_model.CurrentWeapon < 0)
                {
                    _model.CurrentWeapon = _model.WeaponsCount;
                }
            }
            CurrentWeapon.GameObject.SetActive(true);
        }
    }
}