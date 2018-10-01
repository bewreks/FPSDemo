using UnityEngine;

namespace FPSDemo
{
    public class TeammateController : BaseController<TeammateModel>
    {
        protected override void Initialize()
        {
            
        }

        public void NextPosition()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                _model.TargetPostion = hit.point;
            }
            _model.ToPosition = true;
        }

        public void Call()
        {
            _model.Target = Main.Instance.WeaponsController.gameObject;
            _model.ToPosition = false;
        }
    }
}