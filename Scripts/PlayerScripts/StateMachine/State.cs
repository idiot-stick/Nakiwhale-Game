using Godot;
using System;

public partial class State : Node
{
    [Signal] public delegate void state_animation_changeEventHandler(string category, string state);
    [Signal] public delegate void set_sound_loopEventHandler(string sound);
    [Signal] public delegate void play_soundEventHandler(string sound);
    [Signal] public delegate void change_fovEventHandler(float amount, float time);
    
    public StateMachine state_machine;

    public virtual void enter() {}
    public virtual void exit() {}
    
    public virtual void ready() {}
    public virtual void update(float delta) {}
    public virtual void physics_update(float delta) {}
    public virtual void handle_input(InputEvent @event) {}
}
