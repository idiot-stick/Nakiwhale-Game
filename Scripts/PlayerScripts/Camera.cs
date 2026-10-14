using Godot;
using System;

public partial class Camera : Node3D
{
    [Signal] public delegate void camera_rotateEventHandler(float value);


    [Export] public float CameraZoomMaximum = 4;
    [Export] public float CameraZoomMinimum = 0.5f;

    [Export] public float interpolation_weight_zoom = 0.5f;

    private Vector3 camera_target_position = Vector3.Zero;
    private Vector3 previous_camera_target_position = Vector3.Zero;

    private Camera3D camera3D;
    private RayCast3D raycast;
    private Node3D visuals;

    private Vector3 raycast_collision_point;

    public override void _Ready()
    {
        PlayerVariables.Instance.camera_sensitivity_horizontal = 0.4f;
        PlayerVariables.Instance.camera_sensitivity_vertical = 0.35f;
        PlayerVariables.Instance.camera_sensitivity_zoom = 0.5f;
        PlayerVariables.Instance.base_fov = 85;

        camera3D = GetNode<Camera3D>("Camera3D");
        raycast = GetNode<RayCast3D>("RayCast3D");
        visuals = GetNode<Node3D>("../Visuals");

        raycast_collision_point = camera3D.GlobalPosition;
        camera_target_position = camera3D.Position;
        previous_camera_target_position = camera_target_position;

        PlayerVariables.Instance.camera3D_position_x = camera3D.Position.X;
        PlayerVariables.Instance.camera3D_position_y = camera3D.Position.Y;

        //set_spring_arm();

        // Connecting signals

        Node state_machine = GetNode<Node>("../StateMachine");
        
        for (int i = 0; i < state_machine.GetChildCount(); ++i)
        {
            for (int j = 0; j < state_machine.GetChild(i).GetChildCount(); ++j)
            {
                Node state_group = state_machine.GetChild(i).GetChild(j);

                for (int k = 0; k < state_group.GetChildCount(); ++k)
                {
                    State state = state_group.GetChild(k) as State;
                    state.change_fov += _on_change_fov;
                }
            }
        }
        
    }

    public override void _Process(double delta)
    {
        camera3D.GlobalPosition = camera3D.GlobalPosition.Lerp(raycast_collision_point, 0.4f);
    }


    public override void _PhysicsProcess(double delta)
    {
        if (previous_camera_target_position != camera_target_position)
        {
            raycast.TargetPosition = camera_target_position;
            previous_camera_target_position = camera_target_position;
        }

        if (raycast.IsColliding())
        {
            raycast_collision_point = raycast.GetCollisionPoint();
        } else
        {
            raycast_collision_point = ToGlobal(camera_target_position);
        }
    }


    public override void _Input(InputEvent @event)
    {
        // Camera rotation
        if (@event is InputEventMouseMotion mouseEvent)
        {   
            // The player itself needs to be rotated so sending a signal to the player
            EmitSignal(SignalName.camera_rotate, Mathf.DegToRad(-mouseEvent.Relative.X * PlayerVariables.Instance.camera_sensitivity_horizontal));

            visuals.RotateY(Mathf.DegToRad(mouseEvent.Relative.X * PlayerVariables.Instance.camera_sensitivity_horizontal)); // Offset visuals rotation from player's actual rotation

            // X rotation
            RotateX(Mathf.DegToRad(-mouseEvent.Relative.Y * PlayerVariables.Instance.camera_sensitivity_vertical));
            RotationDegrees = new Vector3 (Mathf.Clamp(RotationDegrees.X, -90, 90),  RotationDegrees.Y, RotationDegrees.Z);
        }


        if (Input.IsActionPressed("camera_zoom_out"))
        {
            camera_target_position += new Vector3 (0, 0, PlayerVariables.Instance.camera_sensitivity_zoom);
            camera_target_position = new Vector3(camera_target_position.X, camera_target_position.Y, Mathf.Clamp(camera_target_position.Z, CameraZoomMinimum, CameraZoomMaximum));
        }

        if (Input.IsActionPressed("camera_zoom_in"))
        {
            camera_target_position -= new Vector3 (0, 0, PlayerVariables.Instance.camera_sensitivity_zoom);
            camera_target_position = new Vector3(camera_target_position.X, camera_target_position.Y, Mathf.Clamp(camera_target_position.Z, CameraZoomMinimum, CameraZoomMaximum));
        }

    }

    private void _on_change_fov(float amount, float time)
    {
        Tween tween = CreateTween();
        tween.TweenProperty(camera3D, "fov", PlayerVariables.Instance.base_fov + amount, time);
    }
}
