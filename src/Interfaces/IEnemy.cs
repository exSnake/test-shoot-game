namespace RunAndShoot.Interfaces;

/// <summary>
/// Implemented by all enemy types. Extends IDamageable with enemy-specific behaviour.
/// </summary>
public interface IEnemy : IDamageable
{
    int ScoreValue { get; }
    int ContactDamage { get; }

    void Activate();
    void Deactivate();
}
