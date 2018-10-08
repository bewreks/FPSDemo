using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FPSDemo
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        public InputController InputController { get; private set; }
        public FlashlightController FlashlightController { get; private set; }
        public WeaponsController WeaponsController { get; private set; }
        public TeammateController TeammateController { get; private set; }
        public PlayerController PlayerController { get; private set; }

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

            InputController = gameObject.AddComponent<InputController>();
            FlashlightController = gameObject.AddComponent<FlashlightController>();
            WeaponsController = FindObjectOfType<WeaponsController>();
            TeammateController = FindObjectOfType<TeammateController>();
            PlayerController = FindObjectOfType<PlayerController>();
        }

        private Texture2D _screenshot;
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