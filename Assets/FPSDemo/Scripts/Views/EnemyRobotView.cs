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
			_model.OnHpChanged += OnHpChanged;
		}

		private void OnHpChanged()
		{
			if (_model.IsDead)
			{
				_animator.SetFloat("Speed", 0);
				_animator.SetBool("IsDead", true);
			}
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
