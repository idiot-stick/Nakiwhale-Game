using Godot;
using System;

public partial class UserInterface : UI
{
    [Export] Control pause_menu;
    [Export] Control debug_stats;
    [Export] Control options_menu;

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("pause_menu"))
        {
            if (GetTree().Paused == true)
            {
                pause_menu.Visible = false;
                options_menu.Visible = false;
            } else
            {
                toggle_visibility(pause_menu);
            }

            GetTree().Paused = !GetTree().Paused;
            toggle_mouse_mode();
        }
    }
}
