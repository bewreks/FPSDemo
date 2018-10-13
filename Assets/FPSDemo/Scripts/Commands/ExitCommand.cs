using UnityEngine;

namespace FPSDemo
{
    internal class ExitCommand : Command
    {
        protected override void InternalExecute()
        {
#if UNITY_EDITOR
            Debug.Break();
#else
            Application.Quit();
#endif
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
            _isEnabledInPause = true;
        }
    }
}