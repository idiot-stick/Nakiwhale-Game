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
            state_machine.change_state_movement("AirborneWalking");
        } else if (direction == Vector3.Zero)
        {
            state_machine.change_state_movement("Idle");
        }

    }

    public override void handle_input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("jump"))
        {
            EmitSignal(SignalName.state_animation_change, "actions", "jumping");
            jump_multiplier = 1.00f;
            jump(JUMP_POWER, JUMP_WEIGHT);
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
