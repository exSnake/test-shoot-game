using Godot;

namespace RunAndShoot.Core;

/// <summary>
/// Global game state manager (Autoload singleton).
/// Tracks score, lives, current level and game state machine.
/// </summary>
public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; } = null!;

    // ── State ─────────────────────────────────────────────────────────────
    public enum GameState { MainMenu, Playing, Paused, GameOver, LevelComplete }

    public GameState CurrentState { get; private set; } = GameState.MainMenu;
    public int Score { get; private set; }
    public int Lives { get; private set; } = 3;
    public int CurrentLevel { get; private set; } = 1;

    // ── Lifecycle ─────────────────────────────────────────────────────────
    public override void _Ready()
    {
        Instance = this;
        SubscribeToEvents();
    }

    // ── Public API ────────────────────────────────────────────────────────
    public void StartGame()
    {
        Score = 0;
        Lives = 3;
        CurrentLevel = 1;
        ChangeState(GameState.Playing);
    }

    public void AddScore(int amount)
    {
        Score += amount;
        EventBus.Instance.EmitSignal(EventBus.SignalName.ScoreChanged, Score);
    }

    public void TogglePause()
    {
        if (CurrentState == GameState.Playing)
        {
            ChangeState(GameState.Paused);
            GetTree().Paused = true;
            EventBus.Instance.EmitSignal(EventBus.SignalName.GamePaused);
        }
        else if (CurrentState == GameState.Paused)
        {
            ChangeState(GameState.Playing);
            GetTree().Paused = false;
            EventBus.Instance.EmitSignal(EventBus.SignalName.GameResumed);
        }
    }

    // ── Private ───────────────────────────────────────────────────────────
    private void SubscribeToEvents()
    {
        EventBus.Instance.PlayerDied += OnPlayerDied;
        EventBus.Instance.LevelCompleted += OnLevelCompleted;
        EventBus.Instance.EnemyDied += OnEnemyDied;
    }

    private void OnPlayerDied()
    {
        Lives--;
        if (Lives <= 0)
            ChangeState(GameState.GameOver);
    }

    private void OnLevelCompleted()
    {
        CurrentLevel++;
        ChangeState(GameState.LevelComplete);
    }

    private void OnEnemyDied(int scoreValue) => AddScore(scoreValue);

    private void ChangeState(GameState newState) => CurrentState = newState;
}
