using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public abstract class BaseModelContainer<M> : MonoBehaviour where M : BaseModel
    {
        public UnityAction OnControllerInitialize;
        public bool IsInitialized => _isInitialized;
        
        protected M _model;

        private bool _isInitialized;

        private void Awake()
        {
            _model = GetComponent<M>();
            if (!_model)
            {
                _model = GetComponentInParent<M>();
            }
            if (!_model)
            {
                _model = FindObjectOfType<M>();
            }
			
            if (_model.IsInited)
            {
                InternalInitialize();
            }
            else
            {
                _model.OnInit += InternalInitialize;
            }
        }

        private void InternalInitialize()
        {
            Initialize();
            _isInitialized = true;
            OnControllerInitialize?.Invoke();
            OnControllerInitialize = null;
        }

        protected abstract void Initialize();
    }
}