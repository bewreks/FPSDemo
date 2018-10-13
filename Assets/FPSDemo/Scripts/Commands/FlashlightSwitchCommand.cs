namespace FPSDemo
{
    internal class FlashlightSwitchCommand : Command
    {
        protected override void InternalExecute()
        {
            Main.Instance.FlashlightController.Switch();
//            Debug.Log("Test");
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }
    }
}