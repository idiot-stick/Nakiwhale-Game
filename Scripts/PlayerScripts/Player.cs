using Godot;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody3D
{
    private Node3D visuals = null;
    private Godot.Vector3 gravity;

    public override void _Ready()
    {  
        Input.MouseMode = Input.MouseModeEnum.Captured;

        visuals = GetNode<Node3D>("Visuals");

        Position = new Godot.Vector3 (0, 0.5f, 0);

        Node state_machine = GetNode<Node>("StateMachine");
        for (int i = 0; i < state_machine.GetChild(0).GetChildCount(); ++i) // First child is movement, second child is actions
        {
            var state_group = state_machine.GetChild(0).GetChild(i);

            for (int j = 0; j < state_group.GetChildCount(); ++j)
            {
                Motion state = state_group.GetChild(j) as Motion;

                if (state is Motion)
                {
                    state.velocity_update += _on_velocity_update;
                    state.visuals_update += _on_visuals_update;
                    state.set_gravity += _on_set_gravity;
                }
            }
            
        }
    }

    public override void _PhysicsProcess(double delta)
    {   
        Velocity += gravity;
        gravity = Godot.Vector3.Zero;
        MoveAndSlide();
    }

    private void _on_camera_rotate(float value)
    {
        RotateY(value);
    }

    private void _on_velocity_update(Godot.Vector3 velocity)
    {
        Velocity = velocity;
    }

    private void _on_visuals_update(Godot.Vector3 rotation)
    {
        visuals.Rotation = rotation;
    }

    private void _on_set_gravity()
    {
        gravity = GetGravity();
    }

}
