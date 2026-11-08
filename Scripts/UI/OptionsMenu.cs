using Godot;
using System;

public partial class OptionsMenu : UI
{
    [Export] MarginContainer menus;
    private Control pause_menu;

    public override void _Ready()
    {
        pause_menu = GetNode<Control>("../PauseMenu");
    }

    private void toggle_menu_visibility(String menu_name)
    {
        for (int i = 0; i < menus.GetChildCount(); ++i)
        {
            Control menu = menus.GetChild(i) as Control;

            if (menu.Name == menu_name)
            {
                menu.Visible = true;
            } else
            {
                menu.Visible = false;
            }
        }
    }

    private void _on_back_pressed()
    {
        toggle_visibility(pause_menu);
        toggle_visibility(this);
    }

    private void _on_camera_button_pressed()
    {
        toggle_menu_visibility("CameraOptionsMenu");
    }

    private void _on_audio_button_pressed()
    {
        toggle_menu_visibility("AudioOptionsMenu");
    }

    private void _on_wawa_button_pressed()
    {
        toggle_menu_visibility("Blank");
    }
}