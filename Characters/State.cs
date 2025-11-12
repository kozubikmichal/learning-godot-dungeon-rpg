using Godot;
using System;

public partial class State : Node
{
	protected virtual void OnStateEnter() { }
	protected virtual void OnStateExit() { }


	public override void _Ready()
	{
		DisableState();
	}

	private void EnableState()
	{
		SetPhysicsProcess(true);
		SetProcessInput(true);
	}

	private void DisableState()
	{
		SetPhysicsProcess(false);
		SetProcessInput(false);
	}

	public override void _Notification(int what)
	{
		base._Notification(what);

		switch (what)
		{
			case Constants.STATE_NOTIFICATION_ENTER:
				OnStateEnter();
				EnableState();
				break;
			case Constants.STATE_NOTIFICATION_EXIT:
				OnStateExit();
				DisableState();
				break;
		}
	}
}
