using Godot;
using System;

public partial class PlayerDeathState : PlayerState
{
	protected override void OnStateEnter()
	{
		characterNode.AnimationPlayer.Play(Constants.ANIMATION_DEATH);
		characterNode.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animationName)
	{
		characterNode.QueueFree();
	}
}
