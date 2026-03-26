extends Node2D

## TestLevel.gd
## Genera un pavimento di test usando StaticBody2D + ColorRect
## così non serve nessun tileset/sprite esterno.

const TILE := 16
const FLOOR_Y := 200  # y del pavimento in pixel
const FLOOR_WIDTH := 80  # numero di tile orizzontali
const PLATFORM_COLOR := Color(0.35, 0.55, 0.25)  # verde scuro

func _ready() -> void:
	_build_floor()
	_build_platforms()

func _build_floor() -> void:
	var body := StaticBody2D.new()
	body.name = "Floor"
	add_child(body)

	# Collision
	var col := CollisionShape2D.new()
	var shape := RectangleShape2D.new()
	shape.size = Vector2(FLOOR_WIDTH * TILE, TILE)
	col.shape = shape
	col.position = Vector2((FLOOR_WIDTH * TILE) / 2.0, FLOOR_Y + TILE / 2.0)
	body.add_child(col)

	# Visual
	var rect := ColorRect.new()
	rect.color = PLATFORM_COLOR
	rect.size = Vector2(FLOOR_WIDTH * TILE, TILE)
	rect.position = Vector2(0, FLOOR_Y)
	body.add_child(rect)

func _build_platforms() -> void:
	# Piattaforma 1 - bassa a sinistra
	_add_platform(Vector2(160, 150), Vector2(96, TILE))
	# Piattaforma 2 - media al centro
	_add_platform(Vector2(400, 100), Vector2(128, TILE))
	# Piattaforma 3 - alta a destra
	_add_platform(Vector2(650, 50), Vector2(96, TILE))

func _add_platform(pos: Vector2, size: Vector2) -> void:
	var body := StaticBody2D.new()
	body.name = "Platform"
	add_child(body)

	var col := CollisionShape2D.new()
	var shape := RectangleShape2D.new()
	shape.size = size
	col.shape = shape
	col.position = pos + size / 2.0
	body.add_child(col)

	var rect := ColorRect.new()
	rect.color = PLATFORM_COLOR.darkened(0.15)
	rect.size = size
	rect.position = pos
	body.add_child(rect)
