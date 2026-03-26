using Godot;
using RunAndShoot.Interfaces;

namespace RunAndShoot.Systems;

/// <summary>
/// Reusable health logic. Attach to any node that needs IDamageable behaviour
/// and delegate the interface calls here.
///
/// Example:
///   private readonly HealthSystem _health = new(100);
///   public void TakeDamage(int amount) => _health.TakeDamage(amount);
/// </summary>
public class HealthSystem
{
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    // Callbacks — wire these up in the owning node
    public event Action? OnDied;
    public event Action<int, int>? OnHealthChanged; // (current, max)
    public event Action<int>? OnDamageTaken;

    public HealthSystem(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnDamageTaken?.Invoke(amount);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (IsDead)
            OnDied?.Invoke();
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void Reset()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
