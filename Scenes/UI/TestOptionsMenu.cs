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

    float initial_camera_sensitivity_horizontal_value;
    [Export] HSlider camera_sensitivity_horizontal_slider;
    [Export] TextureButton camera_sensitivity_horizontal_reset_button;
    [Export] LineEdit camera_sensitivity_horizontal_text;

    float initial_camera_sensitivity_vertical_value;
    [Export] HSlider camera_sensitivity_vertical_slider;
    [Export] TextureButton camera_sensitivity_vertical_reset_button;
    [Export] LineEdit camera_sensitivity_vertical_text;

    float initial_camera_sensitivity_zoom_value;
    [Export] HSlider camera_sensitivity_zoom_slider;
    [Export] TextureButton camera_sensitivity_zoom_reset_button;
    [Export] LineEdit camera_sensitivity_zoom_text;

    float initial_fov_value;
    [Export] HSlider fov_slider;
    [Export] TextureButton fov_reset_button;
    [Export] LineEdit fov_text;

    public override void _Ready()
    {
        camera_position_x_slider.Value = PlayerVariables.Instance.camera3D_position_x * 10;
        camera_position_x_text.Text = camera_position_x_slider.Value.ToString();
        initial_camera_position_x_value = (float)camera_position_x_slider.Value;

        camera_position_y_slider.Value = PlayerVariables.Instance.camera3D_position_y * 10;
        camera_position_y_text.Text = camera_position_y_slider.Value.ToString();
        initial_camera_position_y_value = (float)camera_position_y_slider.Value;

        camera_sensitivity_horizontal_slider.Value = PlayerVariables.Instance.camera3D_sensitivity_horizontal * 100;
        camera_sensitivity_horizontal_text.Text = camera_sensitivity_horizontal_slider.Value.ToString();
        initial_camera_sensitivity_horizontal_value = (float)camera_sensitivity_horizontal_slider.Value;

        camera_sensitivity_vertical_slider.Value = PlayerVariables.Instance.camera3D_sensitivity_vertical * 100;
        camera_sensitivity_vertical_text.Text = camera_sensitivity_vertical_slider.Value.ToString();
        initial_camera_sensitivity_vertical_value = (float)camera_sensitivity_vertical_slider.Value;

        camera_sensitivity_zoom_slider.Value = PlayerVariables.Instance.camera3D_sensitivity_zoom * 100;
        camera_sensitivity_zoom_text.Text = camera_sensitivity_zoom_slider.Value.ToString();
        initial_camera_sensitivity_zoom_value = (float)camera_sensitivity_zoom_slider.Value;

        fov_slider.Value = PlayerVariables.Instance.camera3D_fov;
        fov_text.Text = fov_slider.Value.ToString();
        initial_fov_value = (float)fov_slider.Value;

        camera_position_x_reset_button.Visible = false;
        camera_position_y_reset_button.Visible = false;
        camera_sensitivity_horizontal_reset_button.Visible = false;
        camera_sensitivity_vertical_reset_button.Visible = false;
        camera_sensitivity_zoom_reset_button.Visible = false;
        fov_reset_button.Visible = false;
    }

    // Camera position x
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


    // Camera position y
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


    // Camera sensitivity horizontal
    private void _on_camera_sensitivity_horizontal_slider_value_changed(float value)
    {
        PlayerVariables.Instance.camera3D_sensitivity_horizontal = value * 0.01f;
        camera_sensitivity_horizontal_text.Text = value.ToString();

        camera_sensitivity_horizontal_reset_button.Visible = true;
    }
    private void _on_camera_sensitivity_horizontal_text_submitted(string new_text)
    {
        PlayerVariables.Instance.camera3D_sensitivity_horizontal = (float)Mathf.Clamp(
            new_text.ToInt(),
            camera_sensitivity_horizontal_slider.MinValue,
            camera_sensitivity_horizontal_slider.MaxValue
        ) * 0.01f;

        camera_sensitivity_horizontal_slider.Value = new_text.ToInt();

        camera_sensitivity_horizontal_reset_button.Visible = true;
    }
    private void _on_camera_sensitivity_horizontal_reset_pressed()
    {
        PlayerVariables.Instance.camera3D_sensitivity_horizontal = initial_camera_sensitivity_horizontal_value * 0.01f;
        camera_sensitivity_horizontal_slider.Value = initial_camera_sensitivity_horizontal_value;
        camera_sensitivity_horizontal_text.Text = initial_camera_sensitivity_horizontal_value.ToString();

        camera_sensitivity_horizontal_reset_button.Visible = false;
    }


    // Camera sensitivity vertical
    private void _on_camera_sensitivity_vertical_slider_value_changed(float value)
    {
        PlayerVariables.Instance.camera3D_sensitivity_vertical = value * 0.01f;
        camera_sensitivity_vertical_text.Text = value.ToString();

        camera_sensitivity_vertical_reset_button.Visible = true;
    }
    private void _on_camera_sensitivity_vertical_text_submitted(string new_text)
    {
        PlayerVariables.Instance.camera3D_sensitivity_vertical = (float)Mathf.Clamp(
            new_text.ToInt(),
            camera_sensitivity_vertical_slider.MinValue,
            camera_sensitivity_vertical_slider.MaxValue
        ) * 0.01f;

        camera_sensitivity_vertical_slider.Value = new_text.ToInt();

        camera_sensitivity_vertical_reset_button.Visible = true;
    }
    private void _on_camera_sensitivity_vertical_reset_pressed()
    {
        PlayerVariables.Instance.camera3D_sensitivity_vertical = initial_camera_sensitivity_vertical_value * 0.01f;
        camera_sensitivity_vertical_slider.Value = initial_camera_sensitivity_vertical_value;
        camera_sensitivity_vertical_text.Text = initial_camera_sensitivity_vertical_value.ToString();

        camera_sensitivity_vertical_reset_button.Visible = false;
    }


    // Camera sensitivity zoom
    private void _on_camera_sensitivity_zoom_slider_value_changed(float value)
    {
        PlayerVariables.Instance.camera3D_sensitivity_zoom = value * 0.01f;
        camera_sensitivity_zoom_text.Text = value.ToString();

        camera_sensitivity_zoom_reset_button.Visible = true;
    }
    private void _on_camera_sensitivity_zoom_text_submitted(string new_text)
    {
        PlayerVariables.Instance.camera3D_sensitivity_zoom = (float)Mathf.Clamp(
            new_text.ToInt(),
            camera_sensitivity_zoom_slider.MinValue,
            camera_sensitivity_zoom_slider.MaxValue
        ) * 0.01f;

        camera_sensitivity_zoom_slider.Value = new_text.ToInt();

        camera_sensitivity_zoom_reset_button.Visible = true;
    }
    private void _on_camera_sensitivity_zoom_reset_pressed()
    {
        PlayerVariables.Instance.camera3D_sensitivity_zoom = initial_camera_sensitivity_zoom_value * 0.01f;
        camera_sensitivity_zoom_slider.Value = initial_camera_sensitivity_zoom_value;
        camera_sensitivity_zoom_text.Text = initial_camera_sensitivity_zoom_value.ToString();

        camera_sensitivity_zoom_reset_button.Visible = false;
    }


    // fov
    private void _on_fov_slider_value_changed(float value)
    {
        PlayerVariables.Instance.camera3D_fov = value;
        fov_text.Text = value.ToString();

        fov_reset_button.Visible = true;
    }
    private void _on_fov_text_submitted(string new_text)
    {
        PlayerVariables.Instance.camera3D_fov = (float)Mathf.Clamp(
            new_text.ToInt(),
            fov_slider.MinValue,
            fov_slider.MaxValue
        );

        fov_slider.Value = new_text.ToInt();

        fov_reset_button.Visible = true;
    }
    private void _on_fov_reset_pressed()
    {
        PlayerVariables.Instance.camera3D_fov = initial_fov_value;
        fov_slider.Value = initial_fov_value;
        fov_text.Text = initial_fov_value.ToString();

        fov_reset_button.Visible = false;
    }
}
