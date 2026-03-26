using Godot;
using RunAndShoot.Core;
using RunAndShoot.Interfaces;
using RunAndShoot.Systems;

namespace RunAndShoot.Entities.Player;

/// <summary>
/// Core player controller. Handles movement, jump and delegates
/// shooting to PlayerShooter and health to HealthSystem.
///
/// Node path: res://scenes/player/Player.tscn
/// </summary>
public partial class PlayerController : CharacterBody2D, IDamageable
{
	// ── Exported parameters (editable in Godot Inspector) ─────────────────
	[Export]
	public float MoveSpeed = 200f;

	[Export]
	public float JumpForce = -450f;

	[Export]
	public int InitialMaxHealth = 5;

	[Export]
	public float InvincibilityDuration = 1.0f;

	// ── IDamageable ───────────────────────────────────────────────────────
	public int CurrentHealth => _health.CurrentHealth;
	public int MaxHealth => _health.MaxHealth;
	public bool IsDead => _health.IsDead;

	// ── Private state ─────────────────────────────────────────────────────
	private HealthSystem _health = null!;
	private float _gravity;
	private bool _isInvincible;
	private float _invincibilityTimer;
	private bool _facingRight;

	// ── Child node refs (assigned in _Ready) ──────────────────────────────
	private PlayerShooter _shooter = null!;
	private AnimatedSprite2D _animSprite = null!;

	// ── Lifecycle ─────────────────────────────────────────────────────────
	public override void _Ready()
	{
		_health = new HealthSystem(InitialMaxHealth);
		_shooter = GetNode<PlayerShooter>("PlayerShooter");
		_animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		_gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

		_health.OnDied += Die;
		_health.OnHealthChanged += (cur, max) =>
			EventBus.Instance.EmitSignal(EventBus.SignalName.PlayerHealthChanged, cur, max);
		_health.OnDamageTaken += dmg =>
			EventBus.Instance.EmitSignal(EventBus.SignalName.PlayerHit, dmg);
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleInvincibility((float)delta);
		ApplyGravity((float)delta);
		HandleMovement();
		HandleJump();
		HandleShoot();
		MoveAndSlide();
		UpdateAnimation();
	}

	// ── IDamageable impl ──────────────────────────────────────────────────
	public void TakeDamage(int amount)
	{
		if (_isInvincible)
			return;
		_health.TakeDamage(amount);
		_isInvincible = true;
		_invincibilityTimer = InvincibilityDuration;
	}

	public void Heal(int amount) => _health.Heal(amount);

	public void Die()
	{
		_animSprite.Play("idle");
		SetPhysicsProcess(false);
		EventBus.Instance.EmitSignal(EventBus.SignalName.PlayerDied);
	}

	// ── Private helpers ───────────────────────────────────────────────────
	private void ApplyGravity(float delta)
	{
		if (!IsOnFloor())
		{
			var v = Velocity;
			v.Y += _gravity * delta;
			Velocity = v;
		}
	}

	private void HandleMovement()
	{
		float direction = Input.GetAxis("move_left", "move_right");
		var v = Velocity;
		v.X = direction * MoveSpeed;
		Velocity = v;

		if (direction != 0)
			_facingRight = direction > 0;
	}

	private void HandleJump()
	{
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			var v = Velocity;
			v.Y = JumpForce;
			Velocity = v;
		}
	}

	private void HandleShoot()
	{
		if (Input.IsActionJustPressed("shoot"))
			_shooter.Shoot();
	}

	private void HandleInvincibility(float delta)
	{
		if (!_isInvincible)
			return;
		_invincibilityTimer -= delta;
		if (_invincibilityTimer <= 0)
			_isInvincible = false;
	}

	private void UpdateAnimation()
	{
		string anim;
		if (!IsOnFloor())
			anim = "jump";
		else if (Mathf.Abs(Velocity.X) > 0)
			anim = "run";
		else
			anim = "idle";

		// idle/walk/run sono disegnati verso sinistra → flip quando guarda destra
		// jump è disegnato verso destra → flip quando guarda sinistra
		bool flipForJump = anim == "jump" ? !_facingRight : _facingRight;
		_animSprite.FlipH = flipForJump;

		if (_animSprite.Animation != anim)
			_animSprite.Play(anim);
	}
}
