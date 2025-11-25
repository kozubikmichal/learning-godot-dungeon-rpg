using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
	[Export] private Timer idleTimer;
	[Export(PropertyHint.Range, "0,20,0.1")] private float maxIdleTimer = 4.0f;

	private RandomNumberGenerator rng = new();
	private int pointIndex = 0;
	public override void _Ready()
	{
		base._Ready();

		idleTimer.Timeout += OnIdleTimerTimeout;
	}

	protected override void OnStateEnter()
	{
		characterNode.DebugLog($"Enter Patrol State");
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_MOVE);
		pointIndex = 1;

		destination = GetPointGlobalPosition(pointIndex);
		characterNode.NavigationAgent.TargetPosition = destination;

		characterNode.NavigationAgent.NavigationFinished += HandleNavigationFinished;
		characterNode.ChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
	}

	protected override void OnStateExit()
	{
		characterNode.NavigationAgent.NavigationFinished -= HandleNavigationFinished;
		characterNode.ChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		characterNode.DebugLog($"Patrol State: Moving to Point {pointIndex} at {destination}");
		if (!idleTimer.IsStopped())
		{
			return;
		}

		Move();
	}

	private void HandleNavigationFinished()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_IDLE);
		idleTimer.WaitTime = rng.RandfRange(0, maxIdleTimer);
		idleTimer.Start();
	}

	private void OnIdleTimerTimeout()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_MOVE);
		pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount);
		destination = GetPointGlobalPosition(pointIndex);
		characterNode.NavigationAgent.TargetPosition = destination;
	}
}
