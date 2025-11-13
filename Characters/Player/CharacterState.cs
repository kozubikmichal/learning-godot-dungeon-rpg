using Godot;
using System;

public partial class CharacterState : State
{
	protected Character characterNode;
	public override void _Ready()
	{
		base._Ready();
		characterNode = GetOwner<Character>();
	}
}