using Godot;
using System;

public partial class TestOptionsMenu : Control
{
    [Export] HSlider camera_position_x_slider;
    [Export] HSlider camera_position_y_slider;

    public override void _Ready()
    {
        camera_position_x_slider.Value = PlayerVariables.Instance.camera3D_position_x * 10;
        camera_position_y_slider.Value = PlayerVariables.Instance.camera3D_position_y * 10;
    }

    private void _on_camera_position_x_drag_ended(bool valueChanged)
    {
        PlayerVariables.Instance.camera3D_position_x = (float)camera_position_x_slider.Value * 0.1f;
    }

    private void _on_camera_position_y_drag_ended(bool valueChanged)
    {
        PlayerVariables.Instance.camera3D_position_y = (float)camera_position_y_slider.Value * 0.1f;
    }
}
