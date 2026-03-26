extends Control

## GameOver.gd
## Shown when the player has no lives left.

@onready var score_label: Label      = $VBoxContainer/ScoreLabel
@onready var retry_button: Button    = $VBoxContainer/RetryButton
@onready var main_menu_button: Button = $VBoxContainer/MainMenuButton

func _ready() -> void:
	score_label.text = "Final Score: %d" % get_node("/root/GameManager").Score
	retry_button.pressed.connect(_on_retry_pressed)
	main_menu_button.pressed.connect(_on_main_menu_pressed)

func _on_retry_pressed() -> void:
	get_node("/root/GameManager").StartGame()
	get_node("/root/SceneManager").LoadLevel(1)

func _on_main_menu_pressed() -> void:
	get_node("/root/SceneManager").GoToMainMenu()
