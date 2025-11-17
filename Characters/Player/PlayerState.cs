using Godot;
using System;

public partial class PlayerState : CharacterState
{
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
}