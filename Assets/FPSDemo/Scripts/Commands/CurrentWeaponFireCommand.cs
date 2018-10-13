namespace FPSDemo
{
    internal class CurrentWeaponFireCommand : Command
    {
        protected override void InternalExecute()
        {
            if (Main.Instance.WeaponsController.CurrentWeapon.IsActive())
            {
                Main.Instance.WeaponsController.CurrentWeapon.Fire();
            }
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }
    }
}