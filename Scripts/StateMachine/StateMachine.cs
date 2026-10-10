using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    [Export] public NodePath initial_state_movement;
    [Export] public NodePath initial_state_action;
    
    public State previous_state_movement;
    public State previous_state_action;

    private Dictionary<string, Motion> state_list_movement;
    private Dictionary<string, State> state_list_action;
    private Motion current_state_movement;
    private State current_state_action;

    public override void _Ready()
    {
        state_list_movement = new Dictionary<string, Motion>();
        state_list_action = new Dictionary<string, State>();

        for (int i = 0; i < GetChild(0).GetChildCount(); ++i)
        {
            foreach (Node node in GetChild(0).GetChild(i).GetChildren()) // Gets movement nodes
            {
                if (node is Motion state)
                {
                    state_list_movement[node.Name] = state;
                    state.state_machine = this;
                    state.ready();
                    state.exit();
                }
            }
        }

        for (int i = 0; i < GetChild(1).GetChildCount(); ++i)
        {
            foreach (Node node in GetChild(1).GetChild(i).GetChildren()) // Gets action nodes
            {
                if (node is State state)
                {
                    state_list_action[node.Name] = state;
                    state.state_machine = this;
                    state.ready();
                    state.exit();
                }
            }
        }
        
        current_state_movement = GetNode<Motion>(initial_state_movement);
        current_state_movement.enter();

        current_state_action = GetNode<State>(initial_state_action);
        current_state_action.enter();
    }

    public override void _Process(double delta)
    {
        current_state_movement.update((float)delta);
        current_state_movement.update((float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        current_state_movement.physics_update((float)delta);
        current_state_action.physics_update((float)delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        current_state_movement.handle_input(@event);
        current_state_action.handle_input(@event);
    }

    public void change_state_movement(string key)
    {
        if (!state_list_movement.ContainsKey(key) || current_state_movement == state_list_movement[key])
        {
            return;
        }

        current_state_movement.exit();
        previous_state_movement = current_state_movement;
        current_state_movement = state_list_movement[key];
        current_state_movement.enter();
    }

    public void change_state_action(string key)
    {
        if (!state_list_action.ContainsKey(key) || current_state_action == state_list_action[key])
        {
            return;
        }

        current_state_action.exit();
        previous_state_action = current_state_action;
        current_state_action = state_list_action[key];
        current_state_action.enter();
    }
}
