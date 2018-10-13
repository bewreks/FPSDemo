using System;
using UnityEngine;

namespace FPSDemo
{
    public class PlayerController : BaseController<PlayerModel>, ISerializable, IDamagable
    {
        public string SerializedName => "Player";

        protected override void Initialize()
        {
        }

        public void Move(float x, float y, float deltaTime)
        {
            if (_model.IsDead)
            {
                return;
            }
            
            var movement = new Vector3(x, 0, y) * _model.Speed;
            movement = Vector3.ClampMagnitude(movement, _model.Speed);

            movement.y = _model.Gravity;

            movement *= deltaTime;
            movement = transform.TransformDirection(movement);
            _model.MoveVector = movement;
        }

        public void Rotate(float x, float y)
        {
            if (_model.IsDead)
            {
                return;
            }
            
            var rotationY = x * _model.HorizontalSensivity;
            var rotationX = y * _model.VerticalSensivity;

            _model.RotateVector = new Vector3(-rotationX, rotationY);
        }

        public SerializableObject Serialize()
        {
            var serializableObject = new SerializableObject(SerializedName);
            serializableObject.AddFloat("HorizontalSensivity", _model.HorizontalSensivity);
            serializableObject.AddFloat("VerticalSensivity", _model.VerticalSensivity);
            serializableObject.AddVector3("Position", _model.transform.position);
            serializableObject.AddQuaternion("Rotation", _model.SummaryRotation);
            serializableObject.AddFloat("Hp", _model.Hp);
            return serializableObject;
        }

        public void Unserialize(SerializableObject serializableObject)
        {
            if (serializableObject.Name != SerializedName)
            {
                return;
            }
            
            _model.HorizontalSensivity = serializableObject.GetFloat("HorizontalSensivity");
            _model.VerticalSensivity = serializableObject.GetFloat("VerticalSensivity");
            _model.transform.position = serializableObject.GetVector3("Position");
            _model.SummaryRotation = serializableObject.GetQuaternion("Rotation");
            _model.Hp = serializableObject.GetFloat("Hp");
        }

        public void DoDamage(float damage, GameObject owner)
        {
            if (_model.IsDead)
            {
                return;
            }
            
            _model.Hp -= _model.Armor.CalculateDamage(damage);
        }
    }
}