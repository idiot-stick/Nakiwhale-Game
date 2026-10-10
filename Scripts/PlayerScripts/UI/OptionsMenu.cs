using Godot;
using System;

public partial class OptionsMenu : UI
{
    Control pause_menu;

    public override void _Ready()
    {
        pause_menu = GetNode<Control>("../PauseMenu");
    }

    private void _on_back_pressed()
    {
        toggle_visibility(pause_menu);
        toggle_visibility(this);
    }
}
