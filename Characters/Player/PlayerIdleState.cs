using Godot;
using System;

public partial class PlayerIdleState : CharacterState
{
	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.direction != Vector2.Zero)
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
