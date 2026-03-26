using Godot;

namespace RunAndShoot.Entities.Enemies;

/// <summary>
/// Simple walking enemy that patrols left/right on a platform.
/// Reverses direction when hitting a wall or reaching the patrol boundary.
/// </summary>
public partial class EnemyWalker : BaseEnemy
{
    [Export]
    public float MoveSpeed = 60f;

    [Export]
    public float PatrolDistance = 100f;

    private Vector2 _startPosition;
    private float _direction = -1f;

    protected override void OnReady()
    {
        _startPosition = GlobalPosition;
        IsActive = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsActive || IsDead)
            return;

        Velocity = new Vector2(_direction * MoveSpeed, Velocity.Y);

        if (!IsOnFloor())
            Velocity += new Vector2(0, 980f * (float)delta);

        MoveAndSlide();

        // Reverse direction at patrol boundaries or walls
        float distFromStart = GlobalPosition.X - _startPosition.X;
        if (Mathf.Abs(distFromStart) > PatrolDistance || IsOnWall())
        {
            _direction *= -1f;
        }
    }
}
