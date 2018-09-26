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
                if (Main.Instance.FirearmsWeaponController.gameObject.activeInHierarchy)
                {
                    Main.Instance.FirearmsWeaponController.Fire();
                }
                if (Main.Instance.ThrowableWeaponController.gameObject.activeInHierarchy)
                {
                    Main.Instance.ThrowableWeaponController.Fire();
                }
            }

            if (Input.GetButtonDown("Reload"))
            {
                if (Main.Instance.FirearmsWeaponController.gameObject.activeInHierarchy)
                {
                    Main.Instance.FirearmsWeaponController.Reload();
                }
            }

            if (Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }
    }
}