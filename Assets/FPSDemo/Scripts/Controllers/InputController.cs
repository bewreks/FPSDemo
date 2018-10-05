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
                if (Main.Instance.WeaponsController.CurrentWeapon.IsActive())
                {
                    Main.Instance.WeaponsController.CurrentWeapon.Fire();
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (Main.Instance.WeaponsController.CurrentWeapon.IsActive())
                {
                    Main.Instance.WeaponsController.CurrentWeapon.TakeAim();
                }
            }
            if (Input.GetButtonUp("Fire2"))
            {
                if (Main.Instance.WeaponsController.CurrentWeapon.IsActive())
                {
                    Main.Instance.WeaponsController.CurrentWeapon.RealizeAim();
                }
            }

            if (Input.GetButtonDown("Reload"))
            {
                if (Main.Instance.WeaponsController.CurrentWeapon.IsActive())
                {
                    Main.Instance.WeaponsController.CurrentWeapon.Reload();
                }
            }

            if (Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }

            var axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis > 0)
            {
                Main.Instance.WeaponsController.SwitchWeapon(true);
            }
            else if (axis < 0)
            {
                Main.Instance.WeaponsController.SwitchWeapon(false);
            }

            Main.Instance.PlayerController.Move(Input.GetAxis("Horizontal"),
                                                Input.GetAxis("Vertical"));

            Main.Instance.PlayerController.Rotate(Input.GetAxis("Mouse X"),
                                                  Input.GetAxis("Mouse Y"));
            

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