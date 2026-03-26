using Godot;
using RunAndShoot.Core;
using RunAndShoot.Interfaces;
using RunAndShoot.Systems;

namespace RunAndShoot.Entities.Enemies;

/// <summary>
/// Abstract base class for all enemies.
/// Concrete enemies (EnemyWalker, EnemyShooter, Boss…) extend this.
///
/// Subclass responsibility:
///   - override _PhysicsProcess for movement patterns
///   - override OnActivated() for enemy-specific setup
/// </summary>
public abstract partial class BaseEnemy : CharacterBody2D, IEnemy
{
    // ── Inspector params ──────────────────────────────────────────────────
    [Export]
    public int BaseHealth = 3;

    [Export]
    public int BaseScore = 100;

    [Export]
    public int BaseContactDmg = 1;

    // ── IEnemy ────────────────────────────────────────────────────────────
    public int ScoreValue => BaseScore;
    public int ContactDamage => BaseContactDmg;
    public int CurrentHealth => _health.CurrentHealth;
    public int MaxHealth => _health.MaxHealth;
    public bool IsDead => _health.IsDead;

    // ── Protected state ───────────────────────────────────────────────────
    protected bool IsActive;
    protected HealthSystem _health = null!;
    protected AnimationPlayer? _animPlayer;

    // ── Lifecycle ─────────────────────────────────────────────────────────
    public override void _Ready()
    {
        _health = new HealthSystem(BaseHealth);
        _health.OnDied += Die;
        _animPlayer = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        OnReady();
    }

    // ── IEnemy ────────────────────────────────────────────────────────────
    public void Activate()
    {
        IsActive = true;
        OnActivated();
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead)
            return;
        _health.TakeDamage(amount);
    }

    public void Heal(int amount) => _health.Heal(amount);

    public virtual void Die()
    {
        EventBus.Instance.EmitSignal(EventBus.SignalName.EnemyDied, ScoreValue);
        _animPlayer?.Play("die");
        SetPhysicsProcess(false);
        // QueueFree is called by the animation or after a short delay
        GetTree().CreateTimer(0.5f).Timeout += QueueFree;
    }

    // ── Extension points for subclasses ───────────────────────────────────
    protected virtual void OnReady() { }

    protected virtual void OnActivated() { }
}
