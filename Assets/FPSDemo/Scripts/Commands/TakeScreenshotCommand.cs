namespace FPSDemo
{
    internal class TakeScreenshotCommand : Command
    {
        protected override void InternalExecute()
        {
            Main.Instance.TakeScreenShot();
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
            _isEnabledInPause = true;
        }
    }
}