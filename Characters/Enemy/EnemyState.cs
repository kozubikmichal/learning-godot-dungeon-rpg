using Godot;
using System;

public abstract partial class EnemyState : CharacterState
{
	protected Vector3 destination;

	protected Vector3 GetPointGlobalPosition(int pointIndex)
	{
		var localPosition = characterNode.PathNode.Curve.GetPointPosition(pointIndex);
		var globalPosition = characterNode.PathNode.GlobalPosition;
		GD.Print("Point ", pointIndex, ": ", globalPosition, " + ", localPosition);
		return globalPosition + localPosition;
	}

	protected void Move()
	{
		characterNode.NavigationAgent.GetNextPathPosition();
		characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);

		characterNode.MoveAndSlide();
		characterNode.Flip();
	}

	protected void HandleChaseAreaBodyEntered(Node3D body)
	{
		characterNode.StateMachine.SwitchState<EnemyChaseState>();
	}
}
