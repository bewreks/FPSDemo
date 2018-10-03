using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class PlayerTrigger : MonoBehaviour
    {
        public UnityAction<Collider> OnPlayerTriggered;
        public UnityAction<Collider> OnPlayerTriggerOut;
        public UnityAction<Collider> OnPlayerTriggerStay;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerTriggered?.Invoke(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerTriggerStay?.Invoke(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerTriggerOut?.Invoke(other);
            }
        }
    }
}