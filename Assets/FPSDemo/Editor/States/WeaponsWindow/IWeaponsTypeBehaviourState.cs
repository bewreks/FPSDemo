using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Weapons
{
    internal interface IWeaponsTypeBehaviourState
    {
        void Show();
        void Create(GameObject container, GameObject ammoPrefab);
        void AddView(GameObject container, MonoScript view);
    }
}