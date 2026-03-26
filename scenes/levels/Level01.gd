extends Node2D

## Level01.gd
## First real level. Terrain is painted in the editor via TileMap.
## This script only handles enemy spawning.

const ENEMY_WALKER_SCENE := preload("res://scenes/enemies/EnemyWalker.tscn")

func _ready() -> void:
	_spawn_enemies()

func _spawn_enemies() -> void:
	_add_enemy(Vector2(250, 176))
	_add_enemy(Vector2(500, 80))
	_add_enemy(Vector2(700, 176))
	_add_enemy(Vector2(900, 80))
	_add_enemy(Vector2(1100, 176))
	_add_enemy(Vector2(1400, 48))

func _add_enemy(pos: Vector2) -> void:
	var enemy := ENEMY_WALKER_SCENE.instantiate()
	enemy.global_position = pos
	add_child(enemy)
