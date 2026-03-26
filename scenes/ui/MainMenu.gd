extends Control

## MainMenu.gd
## Handles button presses on the main menu screen.

@onready var start_button: Button = $VBoxContainer/StartButton
@onready var quit_button: Button  = $VBoxContainer/QuitButton

func _ready() -> void:
	start_button.pressed.connect(_on_start_pressed)
	quit_button.pressed.connect(_on_quit_pressed)

func _on_start_pressed() -> void:
	get_node("/root/GameManager").StartGame()
	get_node("/root/SceneManager").LoadLevel(1)

func _on_quit_pressed() -> void:
	get_tree().quit()
