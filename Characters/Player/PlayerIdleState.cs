using Godot;
using System;

public partial class PlayerIdleState : Node
{
	private Player characterNode;

	public override void _Ready()
	{
		characterNode = GetOwner<Player>();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.inputDirection != Vector2.Zero)
		{
			characterNode.stateMachine.SwitchState<PlayerMoveState>();
		}
	}

	public override void _Notification(int what)
	{
		base._Notification(what);

		if (what == 5001) // Custom notification for entering state
		{
			Run();
		}
	}

	private void Run()
	{
		characterNode.animationPlayer.Play(Constants.ANIMATION_IDLE);
	}
}
