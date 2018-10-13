namespace FPSDemo
{
    internal class CurrentWeaponAlternativeFireCommand : Command
    {
        public bool IdDown;
        
        protected override void InternalExecute()
        {
            if (Main.Instance.WeaponsController.CurrentWeapon.IsActive())
            {
                switch (IdDown)
                {
                    case true:
                        Main.Instance.WeaponsController.CurrentWeapon.TakeAim();
                        break;
                    default:
                        Main.Instance.WeaponsController.CurrentWeapon.RealizeAim();
                        break;
                }
            }
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }
    }
}