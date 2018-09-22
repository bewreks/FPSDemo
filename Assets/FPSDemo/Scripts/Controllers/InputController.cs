using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public class InputController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("SwitchFlashlight"))
//                Main.Instance.FlashlightControllerGb.Switch();
                Main.Instance.FlashlightController.Switch();
        }
    }
}