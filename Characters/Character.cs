using Godot;
using System;
using System.Linq;

public partial class Character : CharacterBody3D
{
	[Export] private StatResource[] stats;

	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimationPlayer { get; private set; }
	[Export] public Sprite3D Sprite { get; private set; }
	[Export] public StateMachine StateMachine { get; private set; }

	[Export(PropertyHint.Range, "0,20,1")] public float MovementSpeed { get; private set; } = Constants.DEFAULT_PLAYER_MOVEMENT_SPEED;
	[Export] public Area3D Hurtbox { get; private set; }
	[Export] public Area3D Hitbox { get; private set; }

	[ExportGroup("AI Nodes")]
	[Export] public Path3D PathNode { get; private set; }
	[Export] public NavigationAgent3D NavigationAgent { get; private set; }
	[Export] public Area3D ChaseArea { get; private set; }
	[Export] public Area3D AttackArea { get; private set; }

	public Vector2 direction = Vector2.Zero;

	public override void _Ready()
	{
		base._Ready();

		Hurtbox.AreaEntered += HandleHurtboxAreaEntered;
	}

	public void Flip()
	{
		if (Velocity.X != 0)
		{
			Sprite.FlipH = Velocity.X < 0;
		}
	}

	public bool IsFlipped { get { return Sprite.FlipH; } }

	private void HandleHurtboxAreaEntered(Area3D area)
	{
		StatResource health = GetStatResource(Stat.Health);

		Character player = area.GetOwner<Character>();

		health.StatValue -= player.GetStatResource(Stat.Strength).StatValue;
	}

	public StatResource GetStatResource(Stat stat)
	{
		return stats.Where(s => s.StatType == stat).FirstOrDefault();
	}

	public void ToggleHitbox(bool enabled)
	{
		Hitbox.GetChild<CollisionShape3D>(0).Disabled = !enabled;
	}
}
