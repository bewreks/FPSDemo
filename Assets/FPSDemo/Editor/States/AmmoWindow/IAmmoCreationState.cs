using UnityEngine;

namespace FPSDemoEditor.Ammo
{
    internal interface IAmmoCreationState
    {
        void Show();
        void Create(GameObject container);
    }
}