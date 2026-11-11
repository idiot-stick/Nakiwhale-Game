using Godot;
using System;

public partial class WorldAudioManager : Node
{
    [Export] AudioStreamPlayer background_audio_player;

    public override void _Ready()
    {
        PlayerVariables.Instance.music_volume = Mathf.DbToLinear(background_audio_player.VolumeDb);
    }

    public override void _Process(double delta)
    {
        if (PlayerVariables.Instance.music_volume != Mathf.DbToLinear(background_audio_player.VolumeDb))
        {
            background_audio_player.VolumeDb = Mathf.LinearToDb(PlayerVariables.Instance.music_volume);
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
