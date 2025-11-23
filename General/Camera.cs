using Godot;
using System;

public partial class Camera : Camera3D
{
	[Export] private Node target { get; set; }

	[Export] private Vector3 positionFromTarget { get; set; }

	public override void _Ready()
	{
		GameEvents.OnStartGame += HandleStartGame;
		GameEvents.OnEndGame += HandleEndGame;
	}

	private void HandleStartGame()
	{
		Reparent(target);
		Position = positionFromTarget;
	}

	private void HandleEndGame()
	{
		Reparent(GetTree().CurrentScene);
	}
}
