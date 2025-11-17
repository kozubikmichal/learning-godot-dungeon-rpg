using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
	[Export] private Timer attackTimer;
	protected override void OnStateEnter()
	{
		characterNode.AttackArea.BodyExited += HandleAttackAreaBodyExited;
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_ATTACK);

		attackTimer.Start();
		attackTimer.Timeout += HandleAttackTimerTimeout;
	}

	protected override void OnStateExit()
	{
		characterNode.AttackArea.BodyExited -= HandleAttackAreaBodyExited;
		attackTimer.Timeout -= HandleAttackTimerTimeout;
	}

	private void HandleAttackAreaBodyExited(Node body)
	{
		characterNode.StateMachine.SwitchState<EnemyChaseState>();
	}

	private void HandleAttackTimerTimeout()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_ATTACK);
	}
}
