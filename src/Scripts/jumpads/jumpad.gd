extends KinematicBody2D

var speed = 400
var jump_angle = 45.0 # angle in degrees
var recommended_distance = 100.0 # recommended distance in pixels

func _physics_process(delta):
    look_at(get_global_mouse_position())
    move_and_slide(Vector2(0, -1) * speed)
    
func _on_JumpPad_area_entered(area):
    if area.is_in_group("JumpPads"):
        var distance = Vector2(area.position.x, area.position.y).distance_to(position)
        if distance < recommended_distance:
            # Calculate the launch vector for the jump pad
            var jump_force = 1000.0 # adjustable force value
            var angle_rad = jump_angle * deg2rad
            var jump_vector = Vector2(Math.cos(angle_rad) * jump_force, Math.sin(angle_rad) * jump_force)
            speed = jump_vector.length()
            apply_impulse(jump_vector, Vector2.ZERO)
        else:
            print("Recommended distance to JumpPad not reached")

func _on_Projectile_area_entered(area):
    if area.is_in_group("Projectiles"):
        queue_free()

# Set the jump angle
func set_jump_angle(angle):
    jump_angle = angle

# Get the recommended distance
func get_recommended_distance():
    return recommended_distance