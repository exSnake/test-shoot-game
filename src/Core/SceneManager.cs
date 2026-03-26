using Godot;

namespace RunAndShoot.Core;

/// <summary>
/// Handles all scene transitions centrally.
/// Avoids scattered GetTree().ChangeSceneToFile() calls throughout the codebase.
/// </summary>
public partial class SceneManager : Node
{
    public static SceneManager Instance { get; private set; } = null!;

    // ── Scene paths ───────────────────────────────────────────────────────
    private const string MainMenuScene   = "res://scenes/ui/MainMenu.tscn";
    private const string GameOverScene   = "res://scenes/ui/GameOver.tscn";
    private const string Level01Scene    = "res://scenes/levels/Level01.tscn";

    public override void _Ready() => Instance = this;

    // ── Public API ────────────────────────────────────────────────────────
    public void GoToMainMenu()  => ChangeScene(MainMenuScene);
    public void GoToGameOver()  => ChangeScene(GameOverScene);
    public void LoadLevel(int levelNumber) =>
        ChangeScene($"res://scenes/levels/Level{levelNumber:D2}.tscn");

    // ── Private ───────────────────────────────────────────────────────────
    private void ChangeScene(string path)
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile(path);
    }
}
