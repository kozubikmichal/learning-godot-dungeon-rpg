using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
	[Export] private Timer dashTimer;
	[Export(PropertyHint.Range, "0,20,1")] private float speed = 10;

	public override void _Ready()
	{
		base._Ready();
		dashTimer.Timeout += OnDashTimerTimeout;
	}

	public override void _PhysicsProcess(double delta)
	{
		characterNode.MoveAndSlide();
		characterNode.Flip();
	}

	protected override void OnStateEnter()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_DASH);
		characterNode.Velocity = new(characterNode.inputDirection.X, 0, characterNode.inputDirection.Y);

		if (characterNode.Velocity == Vector3.Zero)
		{
			characterNode.Velocity = characterNode.IsFlipped ? Vector3.Left : Vector3.Right;
		}

		characterNode.Velocity *= speed;
		dashTimer.Start();
	}

	private void OnDashTimerTimeout()
	{
		characterNode.Velocity = Vector3.Zero;
		characterNode.StateMachine.SwitchState<PlayerIdleState>();
	}
}
