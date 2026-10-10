using Godot;
using System;

public partial class Camera : Node3D
{
    [Signal] public delegate void camera_rotateEventHandler(float value);


    [Export] public float CameraZoomMaximum = 4;
    [Export] public float CameraZoomMinimum = 0.5f;

    [Export] public float interpolation_weight_zoom = 0.5f;

    private Vector3 camera_target_position = Vector3.Zero;

    private Camera3D camera3D = null;
    private SpringArm3D spring_arm = null;
    private Node3D visuals = null;
    
    private float camera3D_previous_position_x = -0.1f;
    private float camera3D_previous_position_y = -0.1f;

    public override void _Ready()
    {
        PlayerVariables.Instance.camera_sensitivity_horizontal = 0.4f;
        PlayerVariables.Instance.camera_sensitivity_vertical = 0.35f;
        PlayerVariables.Instance.camera_sensitivity_zoom = 0.5f;
        PlayerVariables.Instance.base_fov = 85;

        camera3D = GetNode<Camera3D>("SpringArm3D/Camera3D");
        spring_arm = GetNode<SpringArm3D>("SpringArm3D");
        visuals = GetNode<Node3D>("../Visuals");

        PlayerVariables.Instance.camera3D_position_x = camera3D.Position.X;
        PlayerVariables.Instance.camera3D_position_y = camera3D.Position.Y;
        camera3D_previous_position_x = PlayerVariables.Instance.camera3D_position_x;
        camera3D_previous_position_y = PlayerVariables.Instance.camera3D_position_y;

        set_spring_arm();

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
        /***********************************************************************************************************
        Put the clamping of the z value into a _Set() function so it doesn't clamp the value every single _Process()

        Maybe do something about the spring arm?

        Also, check the PlayerVariables.Instance.camera3D_position_x against the camera3D_previous_position_x and if
        the values don't match up, well, match em up, assign the camera3D position to them and call set_spring_arm()
        ***********************************************************************************************************/
        spring_arm.SpringLength = Mathf.Lerp(spring_arm.SpringLength, spring_arm.Position.DistanceTo(camera_target_position), interpolation_weight_zoom);
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

        if (Input.IsActionJustPressed("flip_camera"))
        { 
            Tween tween = CreateTween().SetParallel(true);
            tween.TweenProperty(spring_arm, "rotation", spring_arm.Rotation * new Vector3 (1, -1, 1), 0.1);
            tween.TweenProperty(camera3D, "rotation", camera3D.Rotation * new Vector3 (1, -1, -1), 0.1);            
        }
    }

    private void set_spring_arm()
    {
        camera_target_position = camera3D.Position;
        spring_arm.SpringLength = spring_arm.Position.DistanceTo(camera_target_position);

        Transform3D cameraGlobalTransform = camera3D.GlobalTransform;
        spring_arm.Rotation = new Vector3 (-Mathf.Atan(camera3D.GlobalPosition.Y / camera3D.GlobalPosition.Z), Mathf.Atan(camera3D.GlobalPosition.X / camera3D.GlobalPosition.Z), 0);
        camera3D.GlobalTransform = cameraGlobalTransform;
    }

    private void _on_change_fov(float amount, float time)
    {
        Tween tween = CreateTween();
        tween.TweenProperty(camera3D, "fov", PlayerVariables.Instance.base_fov + amount, time);
    }
}
