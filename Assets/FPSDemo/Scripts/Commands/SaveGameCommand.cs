namespace FPSDemo
{
    internal class SaveGameCommand : Command
    {
        protected override void InternalExecute()
        {
            Main.Instance.Save();
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
            _isEnabledInPause = true;
        }
    }
}