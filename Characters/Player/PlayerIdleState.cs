using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.inputDirection != Vector2.Zero)
		{
			characterNode.StateMachine.SwitchState<PlayerMoveState>();
		}
	}

	protected override void OnStateEnter()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_IDLE);
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Dash"))
		{
			characterNode.StateMachine.SwitchState<PlayerDashState>();
		}
	}
}
