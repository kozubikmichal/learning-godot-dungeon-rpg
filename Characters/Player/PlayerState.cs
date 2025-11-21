using Godot;
using System;

public partial class PlayerState : CharacterState
{
	public override void _Ready()
	{
		base._Ready();
		characterNode.GetStatResource(Stat.Health).OnZero += HandleHealthZero;
	}

	protected void CheckForAttackInput()
	{
		if (Input.IsActionJustPressed(Constants.INPUT_ATTACK))
		{
			characterNode.StateMachine.SwitchState<PlayerAttackState>();
		}
	}

	protected void CheckForDashInput()
	{
		if (Input.IsActionJustPressed(Constants.INPUT_DASH))
		{
			characterNode.StateMachine.SwitchState<PlayerDashState>();
		}
	}

	private void HandleHealthZero()
	{
		characterNode.StateMachine.SwitchState<PlayerDeathState>();
	}
}