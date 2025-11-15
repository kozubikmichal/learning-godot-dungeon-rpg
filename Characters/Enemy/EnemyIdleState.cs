using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
	protected override void OnStateEnter()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_IDLE);
	}

	public override void _PhysicsProcess(double delta)
	{
		characterNode.StateMachine.SwitchState<EnemyReturnState>();
	}
}
