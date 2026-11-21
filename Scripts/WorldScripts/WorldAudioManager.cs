using Godot;
using System;

public partial class WorldAudioManager : Node
{
    [Export] AudioStreamPlayer background_audio_player;

    private int master_bus_index = AudioServer.GetBusIndex("Master");
    private int music_bus_index = AudioServer.GetBusIndex("music");
    private int sfx_bus_index = AudioServer.GetBusIndex("sfx");

    public override void _Ready()
    {
        PlayerVariables.Instance.master_volume = AudioServer.GetBusVolumeLinear(master_bus_index);
        PlayerVariables.Instance.music_volume = AudioServer.GetBusVolumeLinear(music_bus_index);
        PlayerVariables.Instance.sfx_volume = AudioServer.GetBusVolumeLinear(sfx_bus_index);
    }

    public override void _Process(double delta)
    {
        // Basically checks !(value - 0.01 < target_value < value + 0.01)

        if (
            !((AudioServer.GetBusVolumeLinear(master_bus_index) - 0.01) < PlayerVariables.Instance.master_volume &&
            PlayerVariables.Instance.master_volume < (AudioServer.GetBusVolumeLinear(master_bus_index) + 0.01))
        )
        {
            AudioServer.SetBusVolumeLinear(master_bus_index, PlayerVariables.Instance.master_volume);
        }

        if (
            !((AudioServer.GetBusVolumeLinear(music_bus_index) - 0.01) < PlayerVariables.Instance.music_volume &&
            PlayerVariables.Instance.music_volume < (AudioServer.GetBusVolumeLinear(music_bus_index) + 0.01)) 
        )
        {
            AudioServer.SetBusVolumeLinear(music_bus_index, PlayerVariables.Instance.music_volume);
        }

        if (
            !((AudioServer.GetBusVolumeLinear(sfx_bus_index) - 0.01) < PlayerVariables.Instance.sfx_volume &&
            PlayerVariables.Instance.sfx_volume < (AudioServer.GetBusVolumeLinear(sfx_bus_index) + 0.01)) 
        )
        {
            AudioServer.SetBusVolumeLinear(sfx_bus_index, PlayerVariables.Instance.sfx_volume);
        }
    }


    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.T)
            {
                if (PlayerVariables.Instance.current_playlist == "Test1")
                {
                    PlayerVariables.Instance.current_playlist = "Test2";
                } else
                {
                    PlayerVariables.Instance.current_playlist = "Test1";
                }

                switch_playlist();
            }
        }
    }

    private void switch_playlist()
    {
        background_audio_player.Set("parameters/switch_to_clip", PlayerVariables.Instance.current_playlist);

    }
}
