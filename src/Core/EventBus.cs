using Godot;

namespace RunAndShoot.Core;

/// <summary>
/// Global event bus (Autoload singleton).
/// Decouples systems from each other — nobody holds direct references,
/// they just emit and subscribe to events here.
///
/// Usage (emit):   EventBus.Instance.EmitSignal(EventBus.SignalName.PlayerDied);
/// Usage (listen): EventBus.Instance.PlayerDied += OnPlayerDied;
/// </summary>
public partial class EventBus : Node
{
    public static EventBus Instance { get; private set; } = null!;

    // ── Player ────────────────────────────────────────────────────────────
    [Signal]
    public delegate void PlayerDiedEventHandler();

    [Signal]
    public delegate void PlayerHealthChangedEventHandler(int current, int max);

    [Signal]
    public delegate void PlayerHitEventHandler(int damage);

    // ── Enemies ───────────────────────────────────────────────────────────
    [Signal]
    public delegate void EnemyDiedEventHandler(int scoreValue);

    [Signal]
    public delegate void BossPhaseChangedEventHandler(int phase);

    [Signal]
    public delegate void BossDefeatedEventHandler();

    // ── Game state ────────────────────────────────────────────────────────
    [Signal]
    public delegate void GamePausedEventHandler();

    [Signal]
    public delegate void GameResumedEventHandler();

    [Signal]
    public delegate void LevelCompletedEventHandler();

    [Signal]
    public delegate void ScoreChangedEventHandler(int newScore);

    public override void _Ready()
    {
        Instance = this;
    }
}
