using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public float MovementSpeed = Constants.DEFAULT_PLAYER_MOVEMENT_SPEED;

	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer animationPlayer;
	[Export] public Sprite3D _sprite;
	[Export] public StateMachine stateMachine;

	public Vector2 inputDirection = Vector2.Zero;

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = new(inputDirection.X, 0, inputDirection.Y);
		Velocity *= MovementSpeed;

		Flip();
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		inputDirection = Input.GetVector(Constants.INPUT_MOVE_LEFT, Constants.INPUT_MOVE_RIGHT, Constants.INPUT_MOVE_FORWARD, Constants.INPUT_MOVE_BACKWARD);
	}

	private void Flip()
	{
		if (Velocity.X != 0)
		{
			_sprite.FlipH = Velocity.X < 0;
		}
	}
}
