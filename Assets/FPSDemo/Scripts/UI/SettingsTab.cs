using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PostProcessing;
using UnityEngine.UI;


namespace FPSDemo
{
    public class SettingsTab : BaseTab
    {
        public UnityAction OnBack;

        private Dropdown _presets;
        private Dropdown _resolution;
        private Dropdown _texturesQuality;
        private Dropdown _shadowsQuality;
        private Dropdown _antialias;
        private Toggle _vsync;
        private Toggle _postprocess;

        private PostProcessingBehaviour _postProcessingBehaviour;
        private Toggle _dynamicLights;
        private Button _backButton;

        public override void Init()
        {
            CheckPrefs();

            var scroll = GetComponentInChildren<ScrollRect>();
            for (int i = 0; i < scroll.content.childCount; i++)
            {
                var child = scroll.content.GetChild(i);
                switch (child.name)
                {
                    case "PresetDropdown":
                        _presets = child.GetComponent<Dropdown>();
                        _presets.value = QualitySettings.GetQualityLevel();
                        _presets.onValueChanged.AddListener(OnPresetChanged);
                        break;
                    case "ResolutionDropdown":
                        _resolution = child.GetComponent<Dropdown>();
                        _resolution.value = GetResolutionID();
                        _resolution.onValueChanged.AddListener(OnResolutionChanged);
                        break;
                    case "TexturesQualityDropdown":
                        _texturesQuality = child.GetComponent<Dropdown>();
                        CheckTexturesQuality();
                        _texturesQuality.onValueChanged.AddListener(OnTexturesQualityChanged);
                        break;
                    case "VSyncToggle":
                        _vsync = child.GetComponent<Toggle>();
                        CheckVsync();
                        _vsync.onValueChanged.AddListener(OnVsyncChange);
                        break;
                    case "PostProcessToggle":
                        _postprocess = child.GetComponent<Toggle>();
                        _postprocess.isOn = _postProcessingBehaviour.isActiveAndEnabled;
                        _postprocess.onValueChanged.AddListener(OnPostprocessChange);
                        break;
                    case "DynamicLightToggle":
                        _dynamicLights = child.GetComponent<Toggle>();
                        _dynamicLights.isOn = PlayerPrefs.GetInt("DynamicLight") == 1;
                        _dynamicLights.onValueChanged.AddListener(OnDynamicLightsChange);
                        break;
                    case "ShadowsQualityDropdown":
                        _shadowsQuality = child.GetComponent<Dropdown>();
                        CheckShadowsQuality();
                        _shadowsQuality.onValueChanged.AddListener(OnShandowQualityChanged);
                        break;
                    case "Anti-AlialingDropdown":
                        _antialias = child.GetComponent<Dropdown>();
                        CheckAntiAlias();
                        _antialias.onValueChanged.AddListener(OnAntialiasChanged);
                        break;
                    case "BackButton":
                        _backButton = child.GetComponent<Button>();
                        _backButton.onClick.AddListener(OnBack);
                        break;
                }
            }
        }

        private void CheckAntiAlias()
        {
            _antialias.value = QualitySettings.antiAliasing;
        }

        private void CheckShadowsQuality()
        {
            _shadowsQuality.value = PlayerPrefs.GetInt("ShadowQuality");
        }

        private void CheckVsync()
        {
            _vsync.isOn = QualitySettings.vSyncCount != 0;
        }

        private void CheckTexturesQuality()
        {
            _texturesQuality.value = 4 - QualitySettings.masterTextureLimit;
        }

        private void CheckPrefs()
        {
            if (!PlayerPrefs.HasKey("ShadowQuality"))
            {
                OnShandowQualityChanged(0);
            }
            
            _postProcessingBehaviour = Camera.main.GetComponent<PostProcessingBehaviour>();
            OnDynamicLightsChange(PlayerPrefs.GetInt("PostProcessing", 0) == 1);
            OnDynamicLightsChange(PlayerPrefs.GetInt("DynamicLight", 0) == 1);
        }

        private void OnShandowQualityChanged(int value)
        {
            switch (value)
            {
                case 0:
                    QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
                    QualitySettings.shadows = ShadowQuality.Disable;
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    QualitySettings.shadowDistance = 15;
                    QualitySettings.shadowCascades = 1;
                    break;
                case 1:
                    QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
                    QualitySettings.shadows = ShadowQuality.Disable;
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    QualitySettings.shadowDistance = 20;
                    QualitySettings.shadowCascades = 1;
                    break;
                case 2:
                    QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    QualitySettings.shadowDistance = 20;
                    QualitySettings.shadowCascades = 1;
                    break;
                case 3:
                    QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
                    QualitySettings.shadows = ShadowQuality.All;
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    QualitySettings.shadowDistance = 40;
                    QualitySettings.shadowCascades = 2;
                    break;
                case 4:
                    QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
                    QualitySettings.shadows = ShadowQuality.All;
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    QualitySettings.shadowDistance = 70;
                    QualitySettings.shadowCascades = 2;
                    break;
                case 5:
                    QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
                    QualitySettings.shadows = ShadowQuality.All;
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    QualitySettings.shadowDistance = 150;
                    QualitySettings.shadowCascades = 4;
                    break;
            }

            QualitySettings.shadowProjection = ShadowProjection.StableFit;
            QualitySettings.shadowNearPlaneOffset = 2;

            PlayerPrefs.SetInt("ShadowQuality", value);
        }

        private void OnDynamicLightsChange(bool value)
        {
            LightsController.IsOn = value;
            PlayerPrefs.SetInt("DynamicLight", value ? 1 : 0);
        }

        private void OnPostprocessChange(bool value)
        {
            _postProcessingBehaviour.enabled = value;
            PlayerPrefs.SetInt("PostProcessing", value ? 1 : 0);
        }

        private void OnAntialiasChanged(int value)
        {
            QualitySettings.antiAliasing = value;
        }

        private void OnVsyncChange(bool value)
        {
            QualitySettings.vSyncCount = value ? 1 : 0;
        }

        private void OnTexturesQualityChanged(int value)
        {
            QualitySettings.masterTextureLimit = 4 - value;
        }

        private void OnResolutionChanged(int value)
        {
            switch (value)
            {
                case 0:
                    Screen.SetResolution(800, 600, true);
                    break;
                case 1:
                    Screen.SetResolution(1280, 720, true);
                    break;
                case 2:
                    Screen.SetResolution(1280, 800, true);
                    break;
                case 3:
                    Screen.SetResolution(1680, 1050, true);
                    break;
                case 4:
                    Screen.SetResolution(1920, 1080, true);
                    break;
            }
        }

        private void OnPresetChanged(int value)
        {
            QualitySettings.SetQualityLevel(value);
            CheckTexturesQuality();
            CheckVsync();
            PlayerPrefs.SetInt("ShadowQuality", value);
            CheckShadowsQuality();
            CheckAntiAlias();
        }

        private int GetResolutionID()
        {
            var value = -1;
            if (Screen.width == 800 && Screen.height == 600)
            {
                value = 0;
            }
            else if (Screen.width == 1280 && Screen.height == 720)
            {
                value = 1;
            }
            else if (Screen.width == 1280 && Screen.height == 800)
            {
                value = 2;
            }
            else if (Screen.width == 1680 && Screen.height == 1050)
            {
                value = 3;
            }
            else if (Screen.width == 1920 && Screen.height == 1080)
            {
                value = 4;
            }

            return value;
        }
    }
}