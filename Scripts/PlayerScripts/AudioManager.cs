using Godot;
using System;

public partial class AudioManager : Node3D
{
    private AudioStreamPlayer3D loops;
    private AudioStreamPlayer3D sounds;

    public override void _Ready()
    {
        loops = GetNode<AudioStreamPlayer3D>("Loops");
        sounds = GetNode<AudioStreamPlayer3D>("Sounds");


        // Connects all the movement state node animation change signals to this node
        Node state_machine = GetNode<Node>("../StateMachine");

        for (int i = 0; i < state_machine.GetChildCount(); ++i)
        {
            for (int j = 0; j < state_machine.GetChild(i).GetChildCount(); ++j)
            {
                Node state_group = state_machine.GetChild(i).GetChild(j);

                for (int k = 0; k < state_group.GetChildCount(); ++k)
                {
                    State state = state_group.GetChild(k) as State;
                    state.set_sound_loop += _set_sound_loop;
                    state.play_sound += _play_sound;
                }
            }
        }
    }

    private void _set_sound_loop(string sound)
    {
        loops.Set("parameters/switch_to_clip", sound);
    }

    private void _play_sound(string sound)
    {
        sounds.Set("parameters/switch_to_clip", sound);
        sounds.Play();
    }
}
