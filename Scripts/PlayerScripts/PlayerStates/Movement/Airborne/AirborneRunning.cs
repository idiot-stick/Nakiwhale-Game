using Godot;
using System;

public partial class AirborneRunning : Motion
{
    public override void enter()
    {
        GD.Print(Name);
        EmitSignal(SignalName.state_animation_change, "airborne", "running");
        EmitSignal(SignalName.change_fov, 3, 0.1);
    }

    public override void update(float delta)
    {
        get_direction();
        EmitSignal(SignalName.set_gravity);
        update_visuals(TURN_SPEED, delta);
        calculate_velocity(RUN_SPEED, ACCELERATION, delta);


        if (player.IsOnFloor())
        {   
            state_machine.change_state_movement("Running");
            EmitSignal(SignalName.play_sound, "landing");
        } else if (direction != Vector3.Zero && !Input.IsActionPressed("sprint"))
        {
            state_machine.change_state_movement("AirborneWalking");
        } else if (direction == Vector3.Zero)
        {
            state_machine.change_state_movement("AirborneIdle");
        }
    }

    public override void physics_update(float delta)
    {
        jump(JUMP_POWER, JUMP_WEIGHT);
    }
}
