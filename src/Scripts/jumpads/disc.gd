extends KinematicBody2D

var speed = 600

onready var shape = $CollisionShape2D

var collision_radius = 10

func _ready():
    shape.set_circle(Vector2.ZERO, collision_radius)

func _physics_process(delta):
    look_at(position + direction.normalized())
    move_and_slide(direction * speed)
