using Godot;
using System;
using System.Buffers;

public partial class Motion : State
{
    [Signal] public delegate void velocity_updateEventHandler(Vector3 velocity);
    [Signal] public delegate void visuals_updateEventHandler(Vector3 rotation);
    [Signal] public delegate void set_gravityEventHandler();


    // Constants
    public const float AIRBORNE_SPEED = 4;
    public const float CROUCH_SPEED = 2;
    public const float WALK_SPEED = 4;
    public const float RUN_SPEED = 8;
    public const float DASH_SPEED = 30;
    
    public const float ACCELERATION = 50;
    public const float DASH_ACCELERATION = 25;

    public const float TURN_SPEED = 10;
    
    public const float JUMP_POWER = 30;

    public const int DOUBLE_TAP_DELAY = 200;
 

    // Velocity stuff
    static Vector2 input = Vector2.Zero;
    public static Vector3 direction = Vector3.Zero;
    public static Vector3 velocity = Vector3.Zero;


    // Uhhhh
    public CharacterBody3D player = null;
    public Node3D visuals = null;


    // For dash detection
    public int last_tap_time = 0;

    public override void _Ready()
    {
        player = Owner as CharacterBody3D;
        visuals = GetNode<Node3D>("../../../../Visuals");
    }

    public void get_direction()
    {
        input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        direction = (player.Transform.Basis * new Vector3(input.X, 0, input.Y)).Normalized();
    }
    
    public void jump(float jump_power) 
    {
        velocity.Y += jump_power;
    }

    public void update_visuals(float turn_speed, float delta)
    {
        if (direction != Vector3.Zero)
        {
            var global_target_angle = Mathf.Atan2(direction.X, direction.Z);
            var local_target_angle = global_target_angle - player.Rotation.Y + Mathf.Pi;

            Vector3 visuals_rotation = visuals.Rotation;
            visuals_rotation.Y = Mathf.LerpAngle(visuals.Rotation.Y, local_target_angle, turn_speed * delta);
            
            EmitSignal(SignalName.visuals_update, visuals_rotation);
        }
    }

    public void calculate_velocity(float speed, float acceleration, float delta)
    {
        velocity = velocity.MoveToward(direction * speed, acceleration * delta);
        EmitSignal(SignalName.velocity_update, velocity);
    }

    
    public void detect_dash()
    {
        if (Input.IsActionJustPressed("sprint"))
        {
            last_tap_time = (int)Time.GetTicksMsec();

            state_machine.change_state_movement("Running");
        }

        if (Input.IsActionJustReleased("sprint"))
        {
            if ((int)Time.GetTicksMsec() - last_tap_time < DOUBLE_TAP_DELAY)
            {
                state_machine.change_state_movement("Dashing");
            }
        }
    }

}
