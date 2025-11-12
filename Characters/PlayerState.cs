using Godot;
using System;

public partial class PlayerState : State
{
	protected Player characterNode;
	public override void _Ready()
	{
		base._Ready();
		characterNode = GetOwner<Player>();
	}
}