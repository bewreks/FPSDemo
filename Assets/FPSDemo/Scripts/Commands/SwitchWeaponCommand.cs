namespace FPSDemo
{
    internal class SwitchWeaponCommand : Command
    {
        private bool _direction;

        public bool Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        
        protected override void InternalExecute()
        {
            Main.Instance.WeaponsController.SwitchWeapon(_direction);
        }

        protected override void InternalTypeSetter()
        {
            ChildType = GetType();
        }

        public void Type(float axis)
        {
            _direction = axis > 0;
        }
    }
}