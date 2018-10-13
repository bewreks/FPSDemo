namespace FPSDemo
{
    internal class LoadGameCommand : Command
    {
        protected override void InternalExecute()
        {
            Main.Instance.Load();
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
            _isEnabledInPause = true;
        }
    }
}