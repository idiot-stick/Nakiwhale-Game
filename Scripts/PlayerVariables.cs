using Godot;
using System;

public partial class PlayerVariables : Node
{
    public static PlayerVariables Instance { get; private set; }

    public float camera_sensitivity_horizontal;
    public float camera_sensitivity_vertical;
    public float camera_sensitivity_zoom;
    public float base_fov;
    public float camera3D_position_x;
    public float camera3D_position_y;

    public override void _Ready()
    {
        Instance = this;
    }

}
