using Godot;
using System;

public partial class CrouchingWalking : Motion
{
    public override void enter()
    {
        GD.Print(Name);
        EmitSignal(SignalName.state_animation_change, "crouching", "walking");
        EmitSignal(SignalName.change_fov, 1, 0.1);
    }

    public override void update(float delta)
    {
        get_direction();
        update_visuals(TURN_SPEED, delta);
        calculate_velocity(CROUCH_SPEED, ACCELERATION, delta);

        if (!player.IsOnFloor())
        {
            state_machine.change_state_movement("AirborneWalking");
        } else if (direction == Vector3.Zero)
        {
            state_machine.change_state_movement("CrouchingIdle");
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

        if (Input.IsActionJustPressed("crouch") || Input.IsActionJustPressed("sprint"))
        {
            state_machine.change_state_movement("Idle");
        }
    }
}
