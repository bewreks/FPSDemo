using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseTab : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public abstract void Init();
    }
}