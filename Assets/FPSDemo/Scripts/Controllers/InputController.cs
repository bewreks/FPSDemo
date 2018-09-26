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
            Debug.Log(axis);
            if (axis > 0)
            {
                Main.Instance.PlayerController.SwitchWeapon(true);
            }
            else if (axis < 0)
            {
                Main.Instance.PlayerController.SwitchWeapon(false);
            }
        }
    }
}