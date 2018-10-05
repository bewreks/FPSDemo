using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FPSDemo;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class FPSEditorCreateWeaponWindow : FPSEditorBaseWindow
{
    enum WeaponTypeEnum
    {
        Melee,
        Firearms,
        Throwable
    }

    private GameObject _weaponContainer;
    private GameObject _ammoPrefab;
    private WeaponTypeEnum _weaponType;
    private bool _isShowModelData = true;

    // Общее
    private float _power;
    private float _timeout;
    private float _preparation;

    // Огнестрел
    private float _muzzleSpeed;
    private float _reloadTime;
    private int _bulletsCountMax;

    private BaseWeaponModel _model;
    private IWeapon _controller;
    private Object _view;

    protected override void OnGui()
    {
        SetSelected(ref _weaponContainer);
        _weaponContainer = CreateField("Weapon container", _weaponContainer);
        _ammoPrefab = CreateField("AmmoPrefab", _ammoPrefab);
        CreateNewField("Create weapon container", "Weapon_Container");
        _weaponType = (WeaponTypeEnum) EditorGUILayout.EnumPopup(_weaponType, EditorStyles.popup);

        _isShowModelData = EditorGUILayout.Foldout(_isShowModelData, "Weapon data");
        if (_isShowModelData)
        {
            _power = EditorGUILayout.FloatField("Power", _power);
            _timeout = EditorGUILayout.FloatField("Timeout", _timeout);
            _preparation = EditorGUILayout.FloatField("Preparation", _preparation);
            switch (_weaponType)
            {
                case WeaponTypeEnum.Firearms:
                    _muzzleSpeed = EditorGUILayout.FloatField("Muzzle speed", _muzzleSpeed);
                    _reloadTime = EditorGUILayout.FloatField("Reload time", _reloadTime);
                    _bulletsCountMax = EditorGUILayout.IntField("Bullets max", _bulletsCountMax);
                    break;
            }
        }
        
        // TODO: добавить возможность добавить view
        /*EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select view"))
        {
            EditorGUIUtility.ShowObjectPicker<MonoBehaviour>(_view, true, "*View", 1);
        }

        if (GUILayout.Button("Create new view"))
        {
            var path = EditorUtility.OpenFolderPanel("Choose directory for save", Path.Combine(Application.dataPath, "FPSDemo", "Scripts", "Views"), "");
            FPSEditor.CreateScript(path, $"{_weaponContainer.name}View");
            ShowMessage("View created", MessageType.Info);
            AssetDatabase.Refresh();
            EditorGUIUtility.ShowObjectPicker<MonoBehaviour>(_view, true, "*View", 1);
        }

        EditorGUILayout.EndHorizontal();*/

        

        if (GUILayout.Button("Create weapon"))
        {
            if (!_weaponContainer)
            {
                ShowMessage("Container is null", MessageType.Error);
                return;
            }

            if (!_ammoPrefab)
            {
                ShowMessage("Prefab is empty", MessageType.Error);
                return;
            }
            
            CreateMVC();
            ShowMessage("Weapon created", MessageType.Info);
        }
    }

    private void CreateMVC()
    {
        switch (_weaponType)
        {
            case WeaponTypeEnum.Melee:
                _model = _weaponContainer.AddComponent<MeleeWeaponModel>();
                _controller = _weaponContainer.AddComponent<MeleeWeaponController>();
                break;
            case WeaponTypeEnum.Firearms:
                _model = _weaponContainer.AddComponent<FirearmsWeaponModel>();
                _controller = _weaponContainer.AddComponent<FirearmsWeaponController>();
                (_model as FirearmsWeaponModel).MuzzleSpeed = _muzzleSpeed;
                (_model as FirearmsWeaponModel).ReloadTime = _reloadTime;
                (_model as FirearmsWeaponModel).BulletsCountCurrent = _bulletsCountMax;
                break;
            case WeaponTypeEnum.Throwable:
                _model = _weaponContainer.AddComponent<ThrowableWeaponModel>();
                _controller = _weaponContainer.AddComponent<ThrowableWeaponController>();
                break;
        }

        _model.AmmoPrefab = _ammoPrefab;
        _model.Power = _power;
        _model.Timeout = _timeout;
        _model.Preparation = _preparation;
    }
}