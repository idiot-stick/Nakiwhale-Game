using Godot;
using System;

public partial class UserInterface : UI
{
    [Export] Control pause_menu;
    [Export] Control debug_stats;
    [Export] Control options_menu;

    Node paused_ui;

    public override void _Ready()
    {
        paused_ui = GetNode<Node>("PausedUI");

        PlayerVariables.Instance.previously_selected_menu = GetPathTo(pause_menu);
    }


    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("pause_menu"))
        {
            if (GetTree().Paused == true) // If exiting the paused state
            {
                // Sets the visibility of all child nodes of the paused_ui to false
                for (int i = 0; i < paused_ui.GetChildCount(); ++i)
                {
                    Control child = paused_ui.GetChild(i) as Control;
                    if (child.Visible == true)
                    {
                        PlayerVariables.Instance.previously_selected_menu = GetPathTo(child);
                        child.Visible = false;
                    }
                }
            } else // If entering the paused state
            {
                toggle_visibility(GetNode(PlayerVariables.Instance.previously_selected_menu) as Control);
            }

            GetTree().Paused = !GetTree().Paused;
            toggle_mouse_mode();
        }
    }
}
