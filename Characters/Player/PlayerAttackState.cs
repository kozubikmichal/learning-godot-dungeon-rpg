using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
	[Export] private Timer comboResetTimer;

	private int comboCounter = 1;
	private int maxComboCount = 2;

	public override void _Ready()
	{
		base._Ready();
		comboResetTimer.Timeout += HandleComboResetTimeout;
	}

	protected override void OnStateEnter()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_ATTACK + comboCounter, -1, 1.5f);
		characterNode.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
	}

	protected override void OnStateExit()
	{
		characterNode.AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
		comboResetTimer.Start();
	}

	private void HandleAnimationFinished(StringName animationName)
	{
		comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);
		characterNode.ToggleHitbox(false);
		characterNode.StateMachine.SwitchState<PlayerIdleState>();
	}

	private void HandleComboResetTimeout()
	{
		comboCounter = 1;
	}

	private void PerformHit()
	{
		Vector3 newPosition = characterNode.IsFlipped ? Vector3.Left : Vector3.Right;
		var distanceMultiplier = 0.75f;

		characterNode.Hitbox.Position = newPosition * distanceMultiplier;
		characterNode.ToggleHitbox(true);
	}
}
