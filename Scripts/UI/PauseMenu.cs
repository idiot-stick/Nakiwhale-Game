using Godot;
using System;

public partial class PauseMenu : UI
{
    Control options_menu;

    public override void _Ready()
    {
        options_menu = GetNode<Control>("../OptionsMenu");
    }

    private void _on_options_pressed()
    {
        toggle_visibility(options_menu);
        toggle_visibility(this);
    }
}
