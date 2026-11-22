using Godot;
using System;

public partial class AirborneWalking : Motion
{
    public override void enter()
    {
        GD.Print(Name);
        EmitSignal(SignalName.state_animation_change, "airborne", "walking");
        EmitSignal(SignalName.change_fov, 1, 0.1);
    }

    public override void update(float delta)
    {
        get_direction();
        EmitSignal(SignalName.set_gravity);
        update_visuals(TURN_SPEED, delta);
        calculate_velocity(WALK_SPEED, ACCELERATION, delta);


        if (player.IsOnFloor())
        {   
            state_machine.change_state_movement("Walking");
            EmitSignal(SignalName.play_sound, "landing");
        } else if (direction == Vector3.Zero)
        {
            state_machine.change_state_movement("AirborneIdle");
        }
    }

    public override void handle_input(InputEvent @event)
    {
        if (direction != Vector3.Zero && Input.IsActionPressed("sprint"))
        {
            state_machine.change_state_movement("AirborneRunning");
            detect_dash();
        }
    }

    public override void physics_update(float delta)
    {
        jump(JUMP_POWER, JUMP_WEIGHT);
    }
}
