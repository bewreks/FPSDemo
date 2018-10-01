using System.Collections;
using System.Collections.Generic;
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
            InputController = gameObject.AddComponent<InputController>();
            FlashlightController = gameObject.AddComponent<FlashlightController>();
            WeaponsController = FindObjectOfType<WeaponsController>();
            TeammateController = FindObjectOfType<TeammateController>();
            PlayerController = FindObjectOfType<PlayerController>();
        }
    }
}