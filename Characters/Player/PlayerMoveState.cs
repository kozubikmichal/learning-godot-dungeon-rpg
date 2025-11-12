using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.inputDirection == Vector2.Zero)
		{
			characterNode.stateMachine.SwitchState<PlayerIdleState>();
			return;
		}

		characterNode.Velocity = new(characterNode.inputDirection.X, 0, characterNode.inputDirection.Y);
		characterNode.Velocity *= characterNode.MovementSpeed;

		characterNode.Flip();
		characterNode.MoveAndSlide();
	}

	protected override void OnStateEnter()
	{
		characterNode.animationPlayer.Play(Constants.ANIMATION_MOVE);
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Dash"))
		{
			characterNode.stateMachine.SwitchState<PlayerDashState>();
		}
	}
}
