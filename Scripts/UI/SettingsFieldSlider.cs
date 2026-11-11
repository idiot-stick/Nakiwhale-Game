using Godot;
using Godot.NativeInterop;
using System;
using System.Threading.Tasks;

public partial class SettingsFieldSlider : Control
{
    [ExportGroup("Dependencies")]
    [Export] HSlider slider;
    [Export] LineEdit text;
    [Export] TextureButton reset_button;

    [ExportGroup("Modifiers")]
    [Export] string value_name;
    [Export] float value_multiplier;

    private float initial_value;
    private PlayerVariables player_variables;

    public override async void _Ready()
    {
        slider.ValueChanged += slider_value_changed;
        text.TextSubmitted += text_changed;
        reset_button.Pressed += reset_button_pressed;

        player_variables = GetNode<PlayerVariables>("/root/PlayerVariables");

        await set_value(); // Ensures this part of the function runs after every other _ready() call to prevent a race condition
        text.Text = slider.Value.ToString();
        initial_value = (float)slider.Value;

        reset_button.Visible = false;
    }

    private async Task set_value()
    {
        await ToSignal(GetTree().Root, SignalName.Ready);

        slider.Value = (float)player_variables.Get(value_name) * value_multiplier;
    }

    private void slider_value_changed(double value)
    {
        player_variables.Set(value_name, value / value_multiplier);
        text.Text = value.ToString();
        reset_button.Visible = true;
    }

    private void text_changed(string new_text)
    {
        player_variables.Set(value_name, (float)Mathf.Clamp(
            new_text.ToInt(),
            slider.MinValue,
            slider.MaxValue
        ) / value_multiplier);
        slider.Value = new_text.ToInt();

        reset_button.Visible = true;
    }

    private void reset_button_pressed()
    {
        player_variables.Set(value_name, initial_value / value_multiplier);
        slider.Value = initial_value;
        text.Text = initial_value.ToString();

        reset_button.Visible = false;
    }
}
