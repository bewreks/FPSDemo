using System.Collections;
using System.Collections.Generic;
using FPSDemo;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Ammo
{
    enum AmmoTypeEnum
    {
        Melee,
        Firearms,
        Throwable
    }

    enum ColliderTypeEnum
    {
        Box,
        Capsule,
        Mesh
    }

    public class FPSEditorCreateAmmoWindow : FPSEditorBaseWindow
    {
        private IAmmoCreationState _state;

        private AmmoCreationState<MeleeAmmoModel, MeleeAmmoController> _meleeState = new MeleeAmmoCreationState();
        private AmmoCreationState<ThrowableAmmoModel, ThrowableAmmoController> _throwableState = new ThrowableAmmoCreationState();
        private AmmoCreationState<FirearmsAmmoModel, FirearmsAmmoController> _firearmsState = new FirearmsAmmoCreationState();

        private GameObject _ammoContainer;
        private GameObject _visualModel;
        
        private AmmoTypeEnum _ammoType;
        private ColliderTypeEnum _colliderType;

        private bool _needCollider;
        private bool _needLight;
        private bool _needParticleSystem;

        private MonoScript _view;

        protected override void OnGui()
        {
            SetSelected(ref _ammoContainer);
            _ammoContainer = CreateField("Ammo container", _ammoContainer);
            CreateNewField("Create ammo container", "Ammo_container");

            
            ShowDelim();
            _visualModel = CreateField("Visual model", _visualModel);
            
            ShowDelim();
            _ammoType = (AmmoTypeEnum) EditorGUILayout.EnumPopup(_ammoType, EditorStyles.toolbarPopup);
            switch (_ammoType)
            {
                case AmmoTypeEnum.Melee:
                    _state = _meleeState;
                    break;
                case AmmoTypeEnum.Throwable:
                    _state = _throwableState;
                    break;
                case AmmoTypeEnum.Firearms:
                default:
                    _state = _firearmsState;
                    break;
            }
            _state.Show();
            
            ShowDelim();
            _needLight = EditorGUILayout.ToggleLeft("Add light", _needLight);
            
            
            _needParticleSystem = EditorGUILayout.ToggleLeft("Add particle system", _needParticleSystem);
            _needCollider = EditorGUILayout.BeginToggleGroup("Add collider", _needCollider);
            _colliderType = (ColliderTypeEnum) EditorGUILayout.EnumPopup(_colliderType, EditorStyles.toolbarPopup);
            EditorGUILayout.EndToggleGroup();
            
            
            ShowDelim();
            ShowViewPicker(_ammoContainer, ref _view);
            
            ShowDelim();
            if (GUILayout.Button("CreateAmmo"))
            {
                if (!_ammoContainer)
                {
                    ShowMessage("Container is missing", MessageType.Error);
                    return;
                }
                
                _state.Create(_ammoContainer);

                if (_visualModel)
                {
                    Instantiate(_visualModel, Vector3.zero, Quaternion.identity, _ammoContainer.transform);
                }

                if (_needLight)
                {
                    _ammoContainer.AddComponent<Light>();
                }

                if (_needCollider)
                {
                    switch (_colliderType)
                    {
                        case ColliderTypeEnum.Box:
                            _ammoContainer.AddComponent<BoxCollider>();
                            break;
                        case ColliderTypeEnum.Capsule:
                            _ammoContainer.AddComponent<CapsuleCollider>();
                            break;
                        case ColliderTypeEnum.Mesh:
                            _ammoContainer.AddComponent<MeshCollider>();
                            break;
                    }
                }

                if (_needParticleSystem)
                {
                    _ammoContainer.AddComponent<ParticleSystem>();
                }

                if (_view)
                {
                    _ammoContainer.AddComponent(_view.GetClass());
                }
                ShowMessage("Ammo succesfully created", MessageType.Info);
            }
        }
    }
}