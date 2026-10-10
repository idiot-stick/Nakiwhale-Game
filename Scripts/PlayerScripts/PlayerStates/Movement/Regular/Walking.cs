using Godot;
using System;

public partial class Walking : Motion
{
    public override void enter()
    {
        GD.Print(Name);
        
        EmitSignal(SignalName.state_animation_change, "regular", "walking");
        EmitSignal(SignalName.change_fov, 1, 0.1);
    }

    public override void update(float delta)
    {
        get_direction();
        update_visuals(TURN_SPEED, delta);
        calculate_velocity(WALK_SPEED, ACCELERATION, delta);

        if (!player.IsOnFloor())
        {
            state_machine.change_state_movement("AirborneIdle");
        } else if (direction == Vector3.Zero)
        {
            state_machine.change_state_movement("Idle");
        }

    }

    public override void handle_input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("jump"))
        {
            jump(JUMP_POWER);
        }

        if (Input.IsActionJustPressed("crouch"))
        {
            state_machine.change_state_movement("CrouchingIdle");
        }

        if (Input.IsActionPressed("sprint"))
        {
            detect_dash();
        }

        if (Input.IsActionJustReleased("sprint"))
        {
            detect_dash();
        }
    }
}
