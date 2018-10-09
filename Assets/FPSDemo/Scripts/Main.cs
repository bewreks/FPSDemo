using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class Main : MonoBehaviour
    {
        public static UnityAction OnInitialize;
        public static bool _isInitialized = false;
        public static bool IsInitialized => _isInitialized;
        
        public static Main Instance { get; private set; }

        public InputController InputController { get; private set; }
        public FlashlightController FlashlightController { get; private set; }
        public WeaponsController WeaponsController { get; private set; }
        public TeammateController TeammateController { get; private set; }
        public PlayerController PlayerController { get; private set; }
        
        
        private Texture2D _screenshot;
        private int _uninitedControllersCounter;

        private void Awake()
        {
            if (Instance)
                DestroyImmediate(this);
            else
                Instance = this;
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            WeaponsController = FindObjectOfType<WeaponsController>();
            RegisterController<WeaponsController, WeaponsModel>(WeaponsController);
            TeammateController = FindObjectOfType<TeammateController>();
            RegisterController<TeammateController, TeammateModel>(TeammateController);
            PlayerController = FindObjectOfType<PlayerController>();
            RegisterController<PlayerController, PlayerModel>(PlayerController);
            
            InputController = gameObject.AddComponent<InputController>();
            FlashlightController = gameObject.AddComponent<FlashlightController>();
            
            CheckInit();
        }

        private void RegisterController<C, M>(C controller)
            where C : BaseController<M>
            where M : BaseModel
        {
            if (!controller.IsInitialized)
            {
                _uninitedControllersCounter++;
                controller.OnControllerInitialize += OnControllerInitialize;
            }
        }

        private void OnControllerInitialize()
        {
            _uninitedControllersCounter--;
            CheckInit();
        }

        private void CheckInit()
        {
            if (_uninitedControllersCounter <= 0)
            {
                _isInitialized = true;
                OnInitialize?.Invoke();
            }
        }

        public void TakeScreenShot()
        {
            _screenshot = ScreenCapture.CaptureScreenshotAsTexture();
            Invoke("SaveTextureToFile", 0);
        }

        private void SaveTextureToFile()
        {
            var filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
            var bytes = _screenshot.EncodeToPNG();
            File.WriteAllBytes(Path.Combine(Application.dataPath, "Screenshots", filename), bytes);
        }
    }
}