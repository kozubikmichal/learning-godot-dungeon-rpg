using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
	[Export] private Timer chaseTimer;

	private CharacterBody3D target;

	protected override void OnStateEnter()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_MOVE);

		target = characterNode.ChaseArea.GetOverlappingBodies().FirstOrDefault() as CharacterBody3D;

		chaseTimer.Timeout += HandleChaseTimerTimeout;
		characterNode.ChaseArea.BodyExited += HandleChaseAreaBodyExited;
		characterNode.AttackArea.BodyEntered += HandleAttackAreaBodyEntered;

		if (target == null)
		{
			characterNode.StateMachine.SwitchState<EnemyReturnState>();
		}
	}

	protected override void OnStateExit()
	{
		chaseTimer.Timeout -= HandleChaseTimerTimeout;
		characterNode.ChaseArea.BodyExited -= HandleChaseAreaBodyExited;
		characterNode.AttackArea.BodyEntered -= HandleAttackAreaBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		Move();
	}

	private void HandleChaseTimerTimeout()
	{
		destination = target.GlobalPosition;
		characterNode.NavigationAgent.TargetPosition = destination;
	}

	private void HandleChaseAreaBodyExited(Node body)
	{
		characterNode.StateMachine.SwitchState<EnemyReturnState>();
	}

	private void HandleAttackAreaBodyEntered(Node body)
	{
		characterNode.StateMachine.SwitchState<EnemyAttackState>();
	}
}
