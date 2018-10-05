using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseModelContainer<M> : MonoBehaviour where M : BaseModel
    {
        protected M _model;

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
                Initialize();
            }
            else
            {
                _model.OnInit += Initialize;
            }
        }

        protected abstract void Initialize();
    }
}