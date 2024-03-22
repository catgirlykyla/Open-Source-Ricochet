extends KinematicBody2D

export(NodePath) var target_node_path
export (int) var speed = 100

onready var target_node = $target_node_path

var movement_direction = Vector2.ZERO

func _physics_process(delta):
    if is_on_floor():
        if target_node.is_visible():
            movement_direction = (target_node.position - position).normalized() * speed
            move_and_slide(movement_direction)

        # Dodge incoming projectiles
        dodge_projectiles()

func dodge_projectiles():
    # Detect nearby projectiles
    var projectiles = get_nearest_projectiles(150)

    if projectiles:
        movement_direction = get_furthest_direction(projectiles)
        move_and_slide(movement_direction)

func get_nearest_projectiles(range):
    var projectiles = []
    var hit_count = 0

    for i in range(2):
        for bullet in get_world_2d().get_projectiles():
            var to_player = (target_node.position - bullet.position).length()

            if to_player < range:
                projectiles.append(bullet)

    return projectiles

func get_furthest_direction(projectiles):
    var movement = Vector2.ZERO
    var max_distance = 0

    for projectile in projectiles:
        direction = (projectile.position - position).normalized()

        if direction.length() > max_distance:
            movement = direction
            max_