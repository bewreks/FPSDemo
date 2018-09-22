using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseController<M> : MonoBehaviour where M : Object
    {
        protected M _model;
        public bool IsEnabled { get; private set; }

        protected virtual void Awake()
        {
            _model = FindObjectOfType<M>();
        }

        public virtual void On()
        {
            IsEnabled = true;
        }

        public virtual void Off()
        {
            IsEnabled = false;
        }
    }
}