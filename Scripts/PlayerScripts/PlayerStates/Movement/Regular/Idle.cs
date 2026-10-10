using Godot;
using System;

public partial class Idle : Motion
{
    public override void enter()
    {
        GD.Print(Name);

        EmitSignal(SignalName.state_animation_change, "regular", "idle");
        EmitSignal(SignalName.change_fov, 0, 0.1);
    }

    public override void update(float delta)
    {
        get_direction();
        calculate_velocity(RUN_SPEED, ACCELERATION, delta);

        if (!player.IsOnFloor())
        {
            state_machine.change_state_movement("AirborneIdle");
        } else if (direction != Vector3.Zero && Input.IsActionPressed("sprint"))
        {
            state_machine.change_state_movement("Running");
        } else if (direction != Vector3.Zero)
        {
            state_machine.change_state_movement("Walking");
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
    }

}
