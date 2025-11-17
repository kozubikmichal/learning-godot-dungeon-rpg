using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
	[Export] private Timer attackTimer;

	private bool isPlayerNearby = false;

	public override void _Ready()
	{
		base._Ready();
		attackTimer.Timeout += HandleAttackTimerTimeout;
	}

	protected override void OnStateEnter()
	{
		isPlayerNearby = true;
		characterNode.AttackArea.BodyExited += HandleAttackAreaBodyExited;
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_ATTACK);

		attackTimer.Start();
	}

	protected override void OnStateExit()
	{
		characterNode.AttackArea.BodyExited -= HandleAttackAreaBodyExited;
		attackTimer.Stop();
	}

	private void HandleAttackAreaBodyExited(Node body)
	{
		isPlayerNearby = false;
	}

	private void HandleAttackTimerTimeout()
	{
		if (isPlayerNearby)
		{
			characterNode.AnimationPlayer.Play(Constants.ANIMATION_ATTACK);
		}
		else
		{
			characterNode.StateMachine.SwitchState<EnemyChaseState>();
		}
	}
}
