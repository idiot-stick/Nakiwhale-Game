using Godot;
using System;

public partial class Running : Motion
{
    public override void enter()
    {
        GD.Print(Name);

        EmitSignal(SignalName.state_animation_change, "regular", "running");
        EmitSignal(SignalName.change_fov, 3, 0.2);

        detect_dash();
    }

    public override void update(float delta)
    {
        get_direction();
        update_visuals(TURN_SPEED, delta);
        calculate_velocity(RUN_SPEED, ACCELERATION, delta);

        if (!player.IsOnFloor())
        {
            state_machine.change_state_movement("AirborneIdle");
        } else if (direction != Vector3.Zero && !Input.IsActionPressed("sprint"))
        {
            state_machine.change_state_movement("Walking");
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
        
        if (Input.IsActionJustReleased("sprint"))
        {
            detect_dash();
        }
    }
}
