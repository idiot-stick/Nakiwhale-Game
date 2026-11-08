using Godot;
using System;

public partial class PlayerVariables : Node
{
    public static PlayerVariables Instance { get; private set; }

    // Player things

    public float camera3D_sensitivity_horizontal;
    public float camera3D_sensitivity_vertical;
    public float camera3D_sensitivity_zoom;
    public float camera3D_fov;
    public float camera3D_position_x;
    public float camera3D_position_y;

    public NodePath previously_selected_menu;


    // Sounds
    public string current_playlist;
    public float music_volume;
    public bool music_enabled;
    public float sfx_volume;

    public override void _Ready()
    {
        Instance = this;
    }

}
