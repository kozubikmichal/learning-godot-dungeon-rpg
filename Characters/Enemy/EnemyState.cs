using Godot;
using System;

public abstract partial class EnemyState : CharacterState
{
	protected Vector3 destination;

	public override void _Ready()
	{
		base._Ready();

		characterNode.GetStatResource(Stat.Health).OnZero += HandleHealthZero;
	}

	protected Vector3 GetPointGlobalPosition(int pointIndex)
	{
		var localPosition = characterNode.PathNode.Curve.GetPointPosition(pointIndex);
		var globalPosition = characterNode.PathNode.GlobalPosition;
		return globalPosition + localPosition;
	}

	protected void Move()
	{
		characterNode.NavigationAgent.GetNextPathPosition();
		characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);

		characterNode.DebugLog($"Moving towards {destination} with velocity {characterNode.Velocity} from position {characterNode.GlobalPosition}");

		characterNode.MoveAndSlide();
		characterNode.Flip();
	}

	protected void HandleChaseAreaBodyEntered(Node3D body)
	{
		characterNode.StateMachine.SwitchState<EnemyChaseState>();
	}

	private void HandleHealthZero()
	{
		characterNode.StateMachine.SwitchState<EnemyDeathState>();
	}
}
