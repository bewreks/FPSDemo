using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo {
	public class EnemyRobotView : BaseView<EnemyModel>
	{
		private Animator _animator;
		
		protected override void Initialize()
		{
			_animator = GetComponent<Animator>();
			_model.OnAttack += OnAttack;
			_model.OnTurn += transform.LookAt;
		}

		private void OnAttack()
		{
			if (_animator)
			{
				_animator.SetTrigger("Attack");
			}
		}

		private void Update()
		{
			var velocity = Vector3.zero;
			if (_model.NavAgent.remainingDistance > _model.NavAgent.stoppingDistance)
			{
				velocity = _model.NavAgent.desiredVelocity;
			}

			if (_animator)
			{
				_animator.SetFloat("Speed", velocity.magnitude);
			}
		}
	}
}
