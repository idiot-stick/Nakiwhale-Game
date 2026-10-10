using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class Animation : AnimationTree
{
    private CharacterBody3D player;

    public override void _Ready()
    {
        player = GetNode<CharacterBody3D>("..");

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
                    state.state_animation_change += _on_state_animation_change;
                }
            }
        }
    }

    void _on_state_animation_change(string category, string state)
    {
        if (state == "dashing")
        {
            Vector3 player_velocity_3d = player.Velocity.Normalized() * player.Basis;
            GD.Print(new Vector2(player_velocity_3d.X, player_velocity_3d.Z));
            Set("parameters/dashing/blend_position", new Vector2(player_velocity_3d.X, player_velocity_3d.Y));
        }

        Set("parameters/controller/transition_request", category);

        Set("parameters/"+category+"/transition_request", state);
    }

}
