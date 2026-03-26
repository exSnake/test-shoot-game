using Godot;

namespace RunAndShoot.Entities.Projectiles;

/// <summary>
/// Base projectile. Moves in a direction, deals damage on contact,
/// then auto-destroys on collision or when it leaves the screen.
/// </summary>
public partial class Projectile : Area2D
{
    [Export] public int Damage = 1;

    public Vector2 Direction = Vector2.Right;
    public float Speed = 600f;

    private const float MaxLifetime = 5f;
    private float _lifetime;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        AreaEntered  += OnAreaEntered;
    }

    public override void _Process(double delta)
    {
        Position += Direction.Normalized() * Speed * (float)delta;

        _lifetime += (float)delta;
        if (_lifetime >= MaxLifetime)
            QueueFree();
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Interfaces.IDamageable damageable)
            damageable.TakeDamage(Damage);

        QueueFree();
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is Interfaces.IDamageable damageable)
            damageable.TakeDamage(Damage);

        QueueFree();
    }
}
