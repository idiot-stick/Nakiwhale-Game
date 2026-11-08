using Godot;
using System;

public partial class UI : Control
{
    public void toggle_visibility(Control ui_element)
    {
        ui_element.Visible = !ui_element.Visible;
    }

    public void toggle_mouse_mode()
    {
        if (Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        } else
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
    }
}
