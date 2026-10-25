using Godot;
using System;

public partial class PlayerVariables : Node
{
    public static PlayerVariables Instance { get; private set; }

    public float camera3D_sensitivity_horizontal;
    public float camera3D_sensitivity_vertical;
    public float camera3D_sensitivity_zoom;
    public float camera3D_fov;
    public float camera3D_position_x;
    public float camera3D_position_y;

    public NodePath previously_selected_menu;

    public override void _Ready()
    {
        Instance = this;
    }

}
