using Godot;
using RunAndShoot.Entities.Projectiles;
using RunAndShoot.Interfaces;

namespace RunAndShoot.Entities.Player;

/// <summary>
/// Handles projectile spawning for the player.
/// Separated from PlayerController following Single Responsibility Principle.
///
/// Node path: Player/PlayerShooter (child of Player scene)
/// </summary>
public partial class PlayerShooter : Node2D, IShooter
{
	[Export]
	public float FireRate { get; set; } = 0.3f; // seconds between shots

	[Export]
	public float BulletSpeed = 600f;

	[Export]
	public PackedScene? BulletScene;

	[Export]
	public NodePath? BulletSpawnPoint;

	public bool CanShoot => _cooldownTimer <= 0;

	private float _cooldownTimer;
	private Node2D _spawnPoint = null!;

	public override void _Ready()
	{
		_spawnPoint = BulletSpawnPoint != null ? GetNode<Node2D>(BulletSpawnPoint) : this;
	}

	public override void _Process(double delta)
	{
		if (_cooldownTimer > 0)
			_cooldownTimer -= (float)delta;
	}

	public void Shoot()
	{
		if (!CanShoot || BulletScene is null)
			return;

		var bullet = BulletScene.Instantiate<Projectile>();
		GetTree().CurrentScene.AddChild(bullet);

		bullet.GlobalPosition = _spawnPoint.GlobalPosition;
		bullet.Direction = new Vector2(GetParent<Node2D>().Scale.X, 0); // faces player direction
		bullet.Speed = BulletSpeed;

		_cooldownTimer = FireRate;
	}
}
