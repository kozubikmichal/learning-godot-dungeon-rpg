using Godot;
using System;

public abstract partial class EnemyState : State
{
	protected Enemy characterNode;

	public override void _Ready()
	{
		base._Ready();
		characterNode = GetOwner<Enemy>();
	}
}
