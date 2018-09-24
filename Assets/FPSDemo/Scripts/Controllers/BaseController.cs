using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public abstract class BaseController<M> : BaseModelContainer<M> where M : BaseModel
    {
        public bool IsEnabled { get; private set; }

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