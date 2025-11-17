using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.direction == Vector2.Zero)
		{
			characterNode.StateMachine.SwitchState<PlayerIdleState>();
			return;
		}

		characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
		characterNode.Velocity *= characterNode.MovementSpeed;

		characterNode.Flip();
		characterNode.MoveAndSlide();
	}

	protected override void OnStateEnter()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_MOVE);
	}

	public override void _Input(InputEvent @event)
	{
		CheckForAttackInput();
		CheckForDashInput();
	}
}
