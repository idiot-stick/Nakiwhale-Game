using Godot;
using System;

public partial class TestOptionsMenu : Control
{
    float initial_camera_position_x_value;
    [Export] HSlider camera_position_x_slider;
    [Export] TextureButton camera_position_x_reset_button;
    [Export] LineEdit camera_position_x_text;

    float initial_camera_position_y_value;
    [Export] HSlider camera_position_y_slider;
    [Export] TextureButton camera_position_y_reset_button;
    [Export] LineEdit camera_position_y_text;

    public override void _Ready()
    {
        camera_position_x_slider.Value = PlayerVariables.Instance.camera3D_position_x * 10;
        camera_position_x_text.Text = camera_position_x_slider.Value.ToString();
        initial_camera_position_x_value = (float)camera_position_x_slider.Value;

        camera_position_y_slider.Value = PlayerVariables.Instance.camera3D_position_y * 10;
        camera_position_y_text.Text = camera_position_y_slider.Value.ToString();
        initial_camera_position_y_value = (float)camera_position_y_slider.Value;

        camera_position_x_reset_button.Visible = false;
        camera_position_y_reset_button.Visible = false;
    }

    private void _on_camera_position_x_slider_value_changed(float value)
    {
        PlayerVariables.Instance.camera3D_position_x = value * 0.1f;
        camera_position_x_text.Text = value.ToString();

        camera_position_x_reset_button.Visible = true;
    }
    private void _on_camera_position_x_text_submitted(string new_text)
    {
        PlayerVariables.Instance.camera3D_position_x = (float)Mathf.Clamp(
            new_text.ToInt(), 
            camera_position_x_slider.MinValue, 
            camera_position_x_slider.MaxValue
        ) * 0.1f;

        camera_position_x_slider.Value = new_text.ToInt();

        camera_position_x_reset_button.Visible = true;
    }
    private void _on_camera_position_x_reset_pressed()
    {
        PlayerVariables.Instance.camera3D_position_x = initial_camera_position_x_value * 0.1f;
        camera_position_x_slider.Value = initial_camera_position_x_value;
        camera_position_x_text.Text = initial_camera_position_x_value.ToString();
        
        camera_position_x_reset_button.Visible = false;
    }


    private void _on_camera_position_y_slider_value_changed(float value)
    {
        PlayerVariables.Instance.camera3D_position_y = value * 0.1f;
        camera_position_y_text.Text = value.ToString();

        camera_position_y_reset_button.Visible = true;
    }
    private void _on_camera_position_y_text_submitted(string new_text)
    {
        PlayerVariables.Instance.camera3D_position_y = (float)Mathf.Clamp(
            new_text.ToInt(), 
            camera_position_y_slider.MinValue, 
            camera_position_y_slider.MaxValue
        ) * 0.1f;

        camera_position_y_slider.Value = new_text.ToInt();

        camera_position_y_reset_button.Visible = true;
    }
    private void _on_camera_position_y_reset_pressed()
    {        
        PlayerVariables.Instance.camera3D_position_y = initial_camera_position_y_value * 0.1f;
        camera_position_y_slider.Value = initial_camera_position_y_value;
        camera_position_y_text.Text = initial_camera_position_y_value.ToString();

        camera_position_y_reset_button.Visible = false;
    }
}
