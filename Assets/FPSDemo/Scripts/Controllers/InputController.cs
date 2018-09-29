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
                if (Main.Instance.PlayerController.CurrentWeapon.IsActive())
                {
                    Main.Instance.PlayerController.CurrentWeapon.Fire();
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (Main.Instance.PlayerController.CurrentWeapon.IsActive())
                {
                    Main.Instance.PlayerController.CurrentWeapon.TakeAim();
                }
            }
            if (Input.GetButtonUp("Fire2"))
            {
                if (Main.Instance.PlayerController.CurrentWeapon.IsActive())
                {
                    Main.Instance.PlayerController.CurrentWeapon.RealizeAim();
                }
            }

            if (Input.GetButtonDown("Reload"))
            {
                if (Main.Instance.PlayerController.CurrentWeapon.IsActive())
                {
                    Main.Instance.PlayerController.CurrentWeapon.Reload();
                }
            }

            if (Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }

            var axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis > 0)
            {
                Main.Instance.PlayerController.SwitchWeapon(true);
            }
            else if (axis < 0)
            {
                Main.Instance.PlayerController.SwitchWeapon(false);
            }

            if (Input.GetButtonDown("M8NextPosition"))
            {
                Main.Instance.TeammateController.NextPosition();
            }

            if (Input.GetButtonDown("CallM8"))
            {
                Main.Instance.TeammateController.Call();
            }
        }
    }
}