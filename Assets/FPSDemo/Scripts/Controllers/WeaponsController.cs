using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class WeaponsController : BaseController<WeaponsModel>, ISerializable
    {
        protected GameObject _camera;
        public string SerializedName => "Weapons";

        public IWeapon CurrentWeapon => _model.CurrentWeaponController;

        protected override void Initialize()
        {
            _model.Weapons = new List<IWeapon>();
            _camera = gameObject.GetComponentInChildren<Camera>().gameObject;
            for (int i = 0; i < _camera.transform.childCount; i++)
            {
                var child = _camera.transform.GetChild(i);
                if (child.CompareTag("Weapon"))
                {
                    _model.Weapons.Add(child.transform.GetComponent<IWeapon>());
                    _model.Weapons[_model.WeaponsCount].GameObject.SetActive(_model.WeaponsCount == _model.CurrentWeapon);
                    _model.Weapons[_model.WeaponsCount].SetOwner(gameObject);
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

        public SerializableObject Serialize()
        {
            var serializableObject = new SerializableObject(SerializedName);
            serializableObject.AddInt("CurrentWeapon", _model.CurrentWeapon);
            return serializableObject;
        }

        public void Unserialize(SerializableObject serializableObject)
        {
            CurrentWeapon.GameObject.SetActive(false);
            _model.CurrentWeapon = serializableObject.GetInt("CurrentWeapon");
            CurrentWeapon.GameObject.SetActive(true);
        }
    }
}