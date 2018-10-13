using FPSDemo;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Ammo
{
    internal abstract class AmmoCreationState<M, C> : IAmmoCreationState
        where M : BaseAmmoModel
        where C : BaseAmmoController<M>
    {

        private float _damage;
//        private LayerMask _mask;

        protected M _model;
        
        public void Show()
        {
            _damage = EditorGUILayout.FloatField("Damage", _damage);
            //TODO: custom editor layer mask
            
            OnShow();
        }

        public void Create(GameObject container)
        {
            _model = container.AddComponent<M>();
            container.AddComponent<C>();

            _model.Damage = _damage;
//            _model.Mask = _mask;
            
            OnCreate();
        }

        protected abstract void OnShow();
        protected abstract void OnCreate();
    }
}