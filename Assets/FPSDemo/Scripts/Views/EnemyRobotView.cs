using UnityEngine;

namespace FPSDemo {
	public class EnemyRobotView : BaseView<EnemyModel>
	{
		private Animator _animator;
		
		protected override void Initialize()
		{
			_animator = GetComponent<Animator>();
			_model.OnAttack += OnAttack;
			_model.OnSpeedChanged += OnSpeedChanged;
			_model.OnTurn += transform.LookAt;
		}

		private void OnSpeedChanged(float speed)
		{
			_animator.SetFloat("Speed", speed);
		}

		private void OnAttack()
		{
			if (_animator)
			{
				_animator.SetTrigger("Attack");
			}
		}
	}
}
