namespace FPSDemo
{
    internal class PauseCommand : Command
    {
        protected override void InternalExecute()
        {
            Main.Instance.PauseController.SwitchPause();
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
            _isEnabledInPause = true;
        }
    }
}