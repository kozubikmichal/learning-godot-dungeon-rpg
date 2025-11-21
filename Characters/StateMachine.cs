using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
	[Export] private Node currentState;
	[Export] private Node[] states;

	public override void _Ready()
	{
		currentState.Notification(5001);
	}

	public void SwitchState<T>()
	{
		if (currentState is T)
		{
			return;
		}

		var state = states.Where(s => s is T).FirstOrDefault();

		if (state != null)
		{
			currentState.Notification(5002);
			currentState = state;
			currentState.Notification(5001);
		}
	}
}
