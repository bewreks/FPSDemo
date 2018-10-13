namespace FPSDemo
{
    internal class CurrentWeaponReloadCommand : Command
    {
        protected override void InternalExecute()
        {
            if (Main.Instance.WeaponsController.CurrentWeapon.IsActive())
            {
                Main.Instance.WeaponsController.CurrentWeapon.Reload();
            }
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }
    }
}