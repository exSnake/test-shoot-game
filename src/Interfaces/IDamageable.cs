namespace RunAndShoot.Interfaces;

/// <summary>
/// Implemented by any entity that can receive damage (Player, Enemies, Boss).
/// </summary>
public interface IDamageable
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    bool IsDead { get; }

    void TakeDamage(int amount);
    void Heal(int amount);
    void Die();
}
