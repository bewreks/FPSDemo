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
            {
                Main.Instance.FlashlightController.Switch();
            }

            if (Input.GetButton("Fire1"))
            {
                Main.Instance.WeaponController.Fire();
            }

            if (Input.GetButtonDown("Reload"))
            {
                Main.Instance.WeaponController.Reload();
            }

            if (Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }
    }
}