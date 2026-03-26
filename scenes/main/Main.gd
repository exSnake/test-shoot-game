extends Node

## Main.gd
## Entry point scene. Boots the game and handles global input (pause).

func _ready() -> void:
	# Start directly in gameplay for now;
	# later this will show the main menu.
	pass

func _unhandled_input(event: InputEvent) -> void:
	if event.is_action_pressed("pause"):
		get_node("/root/GameManager").TogglePause()
