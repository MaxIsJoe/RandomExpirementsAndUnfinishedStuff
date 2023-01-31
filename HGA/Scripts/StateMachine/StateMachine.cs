using System.Collections.Generic;
using System.Linq;
using Godot;
using HellGroundArcade.Scenes.Instances.Entities;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scripts.StateMachine;

public class StateMachine : Node
{
    private List<IEntityState> _states = new List<IEntityState>();

    [Export()] private NodePath _entityBasePath;
    private EntityBase _entity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _entity = GetNodeOrNull<EntityBase>(_entityBasePath);
        var possibleStates = GetChildren();
        foreach (var nodes in possibleStates)
        {
            if (nodes is not IEntityState c) continue;
            if (c.EnableOnReady == false) continue;
            AddState(c);
        }
    }

    public void AddState(IEntityState newState)
    {
        _states.Add(newState);
        newState.OnEnterState(_entity, this);
    }

    public void AddState(string id)
    {
        if (_states.Any(x => x.StateID == id)) return;
        var possibleStates = GetChildren();
        foreach (var nodes in possibleStates)
        {
            if (nodes is not IEntityState c) continue;
            if (c.StateID != id) continue;
            if (c.CanEnterState()) AddState(c);
        }
    }

    public void RemoveState(IEntityState targetState)
    {
        if (_states.Contains(targetState) == false)
        {
            GD.Print("[StateMachine/RemoveState] - Attempted to remove a state that did not exist.");
            return;
        }
        targetState.OnExitState(_entity);
        _states.Remove(targetState);
    }
    
    public void RemoveState(string id)
    {
        var possibleStates = GetChildren();
        foreach (var nodes in possibleStates)
        {
            if (nodes is not IEntityState c) continue;
            if (c.StateID != id) continue;
            RemoveState(c);
        }
    }
    
    public override void _Process(float delta)
    {
        foreach (var state in _states.Where(state => state.StateActive != false))
        {
            if (state.StateActive == false) continue;
            state.OnProcessState(delta);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        foreach (var state in _states.Where(state => state.StateActive != false))
        {
            if (state.StateActive == false) continue;
            state.OnPhysicsProcessState(delta);
        }
    }
}