namespace FPSDemo
{
    internal class TeammateNextPositionCommand : Command
    {
        protected override void InternalExecute()
        {
            Main.Instance.TeammateController.NextPosition();
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }
    }
}