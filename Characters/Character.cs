using Godot;
using System;

public partial class Character : CharacterBody3D
{

	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimationPlayer { get; private set; }
	[Export] public Sprite3D Sprite { get; private set; }
	[Export] public StateMachine StateMachine { get; private set; }

	[Export(PropertyHint.Range, "0,20,1")] public float MovementSpeed { get; private set; } = Constants.DEFAULT_PLAYER_MOVEMENT_SPEED;

	public Vector2 direction = Vector2.Zero;

	public void Flip()
	{
		if (Velocity.X != 0)
		{
			Sprite.FlipH = Velocity.X < 0;
		}
	}

	public bool IsFlipped { get { return Sprite.FlipH; } }
}
