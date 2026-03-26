namespace RunAndShoot.Interfaces;

/// <summary>
/// Implemented by any entity that can shoot projectiles (Player, some Enemies).
/// </summary>
public interface IShooter
{
	float FireRate { get; }
	bool CanShoot { get; }

	void Shoot();
}
