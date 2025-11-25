using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
	public override void _Ready()
	{
		base._Ready();
	}

	override protected void OnStateEnter()
	{
		characterNode.DebugLog($"Enter Return State");
		destination = GetPointGlobalPosition(0);
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_MOVE);
		characterNode.NavigationAgent.TargetPosition = destination;

		characterNode.ChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
	}

	protected override void OnStateExit()
	{
		characterNode.ChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.NavigationAgent.IsNavigationFinished())
		{
			characterNode.StateMachine.SwitchState<EnemyPatrolState>();
			return;
		}

		Move();
	}
}
