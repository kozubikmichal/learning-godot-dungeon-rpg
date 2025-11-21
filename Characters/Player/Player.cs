using Godot;
using System;

public partial class Player : Character
{
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{

	}

	public override void _Input(InputEvent @event)
	{
		direction = Input.GetVector(Constants.INPUT_MOVE_LEFT, Constants.INPUT_MOVE_RIGHT, Constants.INPUT_MOVE_FORWARD, Constants.INPUT_MOVE_BACKWARD);
	}
}
