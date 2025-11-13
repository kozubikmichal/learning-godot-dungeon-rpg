using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export(PropertyHint.Range, "0,20,1")] public float MovementSpeed { get; private set; } = Constants.DEFAULT_PLAYER_MOVEMENT_SPEED;

	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimationPlayer { get; private set; }
	[Export] public Sprite3D Sprite { get; private set; }
	[Export] public StateMachine StateMachine { get; private set; }

	public Vector2 inputDirection = Vector2.Zero;

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{

	}

	public override void _Input(InputEvent @event)
	{
		inputDirection = Input.GetVector(Constants.INPUT_MOVE_LEFT, Constants.INPUT_MOVE_RIGHT, Constants.INPUT_MOVE_FORWARD, Constants.INPUT_MOVE_BACKWARD);
	}

	public void Flip()
	{
		if (Velocity.X != 0)
		{
			Sprite.FlipH = Velocity.X < 0;
		}
	}

	public bool IsFlipped { get { return Sprite.FlipH; } }
}
