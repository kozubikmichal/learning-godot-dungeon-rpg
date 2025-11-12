using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.inputDirection != Vector2.Zero)
		{
			characterNode.stateMachine.SwitchState<PlayerMoveState>();
		}
	}

	protected override void OnStateEnter()
	{
		characterNode.animationPlayer.Play(Constants.ANIMATION_IDLE);
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Dash"))
		{
			characterNode.stateMachine.SwitchState<PlayerDashState>();
		}
	}
}
