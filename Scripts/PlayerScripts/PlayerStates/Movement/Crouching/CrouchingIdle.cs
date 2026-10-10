using Godot;
using System;

public partial class CrouchingIdle : Motion
{
    public override void enter()
    {
        GD.Print(Name);
        EmitSignal(SignalName.state_animation_change, "crouching", "idle");
        GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D").Play();
    }

    public override void update(float delta)
    {
        get_direction();
        calculate_velocity(CROUCH_SPEED, ACCELERATION, delta);

        if (!player.IsOnFloor())
        {
            state_machine.change_state_movement("AirborneIdle");
        } else if (direction != Vector3.Zero)
        {
            state_machine.change_state_movement("CrouchingWalking");
        }
    }

    public override void handle_input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("jump"))
        {
            jump(JUMP_POWER);
        }

        if (Input.IsActionJustPressed("crouch") || Input.IsActionPressed("sprint"))
        {
            state_machine.change_state_movement("Idle");
        }
    }
}