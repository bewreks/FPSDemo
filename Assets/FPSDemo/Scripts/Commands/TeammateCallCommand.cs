namespace FPSDemo
{
    internal class TeammateCallCommand : Command
    {
        protected override void InternalExecute()
        {
            Main.Instance.TeammateController.Call();
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }
    }
}