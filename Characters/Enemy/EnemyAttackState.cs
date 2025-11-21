using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
	[Export] private Timer attackTimer;

	private Node3D target;
	private Vector3 targetPosition;
	private bool isPlayerNearby = false;

	public override void _Ready()
	{
		base._Ready();
		attackTimer.Timeout += HandleAttackTimerTimeout;
	}

	protected override void OnStateEnter()
	{
		isPlayerNearby = true;
		characterNode.AttackArea.BodyExited += HandleAttackAreaBodyExited;
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_ATTACK);
		characterNode.AnimationPlayer.AnimationFinished += HandleAnimationFinished;

		target = characterNode.AttackArea.GetOverlappingBodies().First();
		targetPosition = target.GlobalPosition;

		attackTimer.Start();
	}

	protected override void OnStateExit()
	{
		characterNode.AttackArea.BodyExited -= HandleAttackAreaBodyExited;
		characterNode.AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
		attackTimer.Stop();
	}

	private void HandleAttackAreaBodyExited(Node body)
	{
		isPlayerNearby = false;
	}

	private void HandleAnimationFinished(StringName animationName)
	{
		characterNode.ToggleHitbox(false);

		if (isPlayerNearby)
		{
			attackTimer.Start();
		}
		else
		{
			characterNode.StateMachine.SwitchState<EnemyChaseState>();
		}
	}

	private void HandleAttackTimerTimeout()
	{
		if (isPlayerNearby)
		{
			targetPosition = target.GlobalPosition;
			FaceTarget();
			characterNode.AnimationPlayer.Play(Constants.ANIMATION_ATTACK);
		}
		else
		{
			characterNode.StateMachine.SwitchState<EnemyChaseState>();
		}
	}

	private void FaceTarget()
	{
		var direction = characterNode.GlobalPosition.DirectionTo(target.GlobalPosition);
		characterNode.Sprite.FlipH = direction.X < 0;
	}

	public void PerformHit()
	{
		characterNode.Hitbox.GlobalPosition = targetPosition;
		characterNode.ToggleHitbox(true);
	}
}
