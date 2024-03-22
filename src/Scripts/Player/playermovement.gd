extends KinematicBody2D

var speed = 400
var bounce_amount = 200

func _physics_process(delta):
    look_at(get_global_mouse_position())
    move_and_slide(Vector2(0, -1) * speed)
    
func _on_Projectile_area_entered(area):
    if area.is_in_group("Projectiles"):
        speed = -speed * bounce_amount / Math.abs(speed)
        move_and_slide(Vector2(0, -1) * speed)