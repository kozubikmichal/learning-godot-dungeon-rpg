using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
	private Dictionary<ContainerType, UIContainer> containers = new();

	public override void _Ready()
	{
		InitContainers();

		containers[ContainerType.Start].Visible = true;
		containers[ContainerType.Start].Button.Pressed += HandleStartPressed;
	}
	private void InitContainers()
	{
		containers = GetChildren().OfType<UIContainer>().ToDictionary(c => c.container);

		foreach (var container in containers.Values)
		{
			container.Visible = false;
		}
	}

	public void ShowContainer(ContainerType containerType)
	{
		foreach (var container in containers.Values)
		{
			container.Visible = false;
		}

		if (containers.ContainsKey(containerType))
		{
			containers[containerType].Visible = true;
		}
	}

	private void HandleStartPressed()
	{
		GetTree().Paused = false;
		containers[ContainerType.Start].Visible = false;
		GameEvents.RaiseStartGame();
	}
}
