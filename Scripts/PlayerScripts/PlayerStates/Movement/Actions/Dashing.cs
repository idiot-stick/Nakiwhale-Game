using Godot;
using System;

public partial class Dashing : Motion
{
    public override void enter()
    {
        GD.Print(Name);

        EmitSignal(SignalName.state_animation_change, "actions", "dashing");
        EmitSignal(SignalName.change_fov, 10, 0.3);

        velocity = direction * DASH_SPEED;
    }

    public override void update(float delta)
    {
        calculate_velocity(0, DASH_ACCELERATION, delta);

        if (velocity.Length() < direction.Length() * WALK_SPEED)
        {
            state_machine.change_state_movement("Walking");
        }
    }

}
