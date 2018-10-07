﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FPSDemo;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FPSDemoEditor.Weapons
{
    public class FPSEditorCreateWeaponWindow : FPSEditorBaseWindow
    {
        enum WeaponTypeEnum
        {
            Melee,
            Firearms,
            Throwable
        }

        private IWeaponsTypeBehaviourState _state;

        private WeaponsTypeBehaviourState<MeleeWeaponModel, MeleeWeaponController> _meleeState =
            new MeleeBehaviourState();

        private WeaponsTypeBehaviourState<ThrowableWeaponModel, ThrowableWeaponController> _throwableState =
            new ThrowableBehaviourState();

        private WeaponsTypeBehaviourState<FirearmsWeaponModel, FirearmsWeaponController> _firearmsState =
            new FirearmsBehaviourState();

        private GameObject _weaponContainer;
        private GameObject _ammoPrefab;
        private GameObject _firepointPrefab;
        private WeaponTypeEnum _weaponType;

        private MonoScript _view;

        protected override void OnGui()
        {
            SetSelected(ref _weaponContainer);
            _weaponContainer = CreateField("Weapon container", _weaponContainer);
            CreateNewField("Create weapon container", "Weapon_Container");

            ShowDelim();
            _ammoPrefab = CreateField("AmmoPrefab", _ammoPrefab);
            _firepointPrefab = CreateField("FirepointPrefab", _firepointPrefab);

            ShowDelim();
            _weaponType = (WeaponTypeEnum) EditorGUILayout.EnumPopup(_weaponType, EditorStyles.toolbarPopup);
            switch (_weaponType)
            {
                case WeaponTypeEnum.Melee:
                    _state = _meleeState;
                    break;
                case WeaponTypeEnum.Throwable:
                    _state = _throwableState;
                    break;
                case WeaponTypeEnum.Firearms:
                    _state = _firearmsState;
                    break;
            }


            EditorGUILayout.LabelField("Weapon data");
            _state.Show();

            ShowDelim();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select view"))
            {
                EditorGUIUtility.ShowObjectPicker<MonoScript>(_view, false, "*View", 1);
            }

            if (GUILayout.Button("Create new view"))
            {
                if (!_weaponContainer)
                {
                    ShowMessage("Container is null", MessageType.Error);
                    return;
                }

                var path = EditorUtility.OpenFolderPanel("Choose directory for save",
                    Path.Combine(Application.dataPath, "FPSDemo", "Scripts", "Views"), "");
                string mask = FPSEditor.CreateScript(path, $"{_weaponContainer.name}View");
                ShowMessage("View created", MessageType.Info);
                AssetDatabase.Refresh();
                EditorGUIUtility.ShowObjectPicker<MonoScript>(_view, false, mask, 1);
            }

            EditorGUILayout.EndHorizontal();

            ShowDelim();
            if (GUILayout.Button("Create weapon"))
            {
                CreateMVC();
            }
        }

        private void CreateMVC()
        {
            if (!_weaponContainer)
            {
                ShowMessage("Container is missing", MessageType.Error);
                return;
            }

            if (!_ammoPrefab)
            {
                ShowMessage("Ammo prefab is missing", MessageType.Error);
                return;
            }

            if (!_firepointPrefab)
            {
                ShowMessage("Firepoint prefab is missing", MessageType.Error);
            }

            _state.Create(_weaponContainer, _ammoPrefab);
            _state.AddView(_weaponContainer, _view);

            Instantiate(_firepointPrefab, Vector3.zero, Quaternion.identity, _weaponContainer.transform);

            ShowMessage("Weapon created", MessageType.Info);
        }
    }
}