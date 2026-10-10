using Godot;
using System;

public partial class AirborneIdle : Motion
{
    public override void enter()
    {
        GD.Print(Name);
        EmitSignal(SignalName.state_animation_change, "airborne", "idle");
        EmitSignal(SignalName.change_fov, 0, 0.1);
    }

    public override void update(float delta)
    {
        get_direction();
        EmitSignal(SignalName.set_gravity);
        update_visuals(TURN_SPEED, delta);
        calculate_velocity(RUN_SPEED, ACCELERATION, delta);

        if (player.IsOnFloor())
        {   
            state_machine.change_state_movement("Idle");
        } else if (direction != Vector3.Zero)
        {
            state_machine.change_state_movement("AirborneWalking");
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

}
