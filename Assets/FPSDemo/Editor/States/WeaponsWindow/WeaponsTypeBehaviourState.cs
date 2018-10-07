using FPSDemo;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Weapons
{
    public abstract class WeaponsTypeBehaviourState<M, C> : IWeaponsTypeBehaviourState
        where M : BaseWeaponModel
        where C : BaseWeaponController<M>
    {
        private float _power;
        private float _timeout;
        private float _preparation;

        protected M _model;

        public void Show()
        {
            _power = EditorGUILayout.FloatField("Power", _power);
            _timeout = EditorGUILayout.FloatField("Timeout", _timeout);
            _preparation = EditorGUILayout.FloatField("Preparation", _preparation);

            OnShow();
        }

        public void Create(GameObject container, GameObject ammoPrefab)
        {
            _model = container.AddComponent<M>();
            container.AddComponent<C>();

            _model.AmmoPrefab = ammoPrefab;
            _model.Power = _power;
            _model.Timeout = _timeout;
            _model.Preparation = _preparation;

            OnCreate();
        }

        public void AddView(GameObject container, MonoScript view)
        {
            if (view != null)
            {
                var componentType = view.GetClass();
                container.AddComponent(componentType);
            }
        }

        protected abstract void OnShow();
        protected abstract void OnCreate();
    }
}