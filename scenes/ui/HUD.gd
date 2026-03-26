extends CanvasLayer

## HUD.gd
## Reacts to EventBus signals and updates UI labels/bars.
## No game logic lives here — only display.

@onready var health_bar: ProgressBar = $HealthBar
@onready var score_label: Label      = $ScoreLabel
@onready var lives_label: Label      = $LivesLabel

func _ready() -> void:
	get_node("/root/EventBus").connect("PlayerHealthChanged", _on_player_health_changed)
	get_node("/root/EventBus").connect("ScoreChanged", _on_score_changed)

func _on_player_health_changed(current: int, max_health: int) -> void:
	health_bar.max_value = max_health
	health_bar.value     = current

func _on_score_changed(new_score: int) -> void:
	score_label.text = "Score: %d" % new_score
