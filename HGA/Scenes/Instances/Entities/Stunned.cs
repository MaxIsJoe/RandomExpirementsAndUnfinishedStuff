using Godot;
using HellGroundArcade.Scripts.Constants;
using HellGroundArcade.Scripts.Interfaces;
using HellGroundArcade.Scripts.StateMachine;

namespace HellGroundArcade.Scenes.Instances.Entities;

public class Stunned : Node, IEntityState
{
    [Export()] public bool IsStunned = false;
    [Export()] private NodePath _timerPath;
    private Timer _timer;

    public string StateID { get; set; } = StateMachineConstants.STATE_STUN;
    public bool StateActive { get; set; } = false;
    public bool EnableOnReady { get; set; } = false;
    public StateMachine Machine { get; set; }

    public override void _Ready()
    {
        base._Ready();
        _timer = GetNodeOrNull<Timer>(_timerPath);
    }

    private void Stun(EntityBase entity, float duration = 1.5f)
    {
        if (entity == null)
        {
            GD.PrintErr("[StateMachine/Stunned] - Attempted to stun an entity that is null!");
            return;
        }
        entity.EntityStats.IsStunned = true;
        _timer.Start(duration);
    }

    private void _on_StunTimer_timeout()
    {
        UnStun();
    }

    private void UnStun()
    {
        Machine.RemoveState(this);
    }

    public bool CanEnterState()
    {
        return StateActive != true;
    }

    public void OnEnterState(EntityBase entity, StateMachine machine)
    {
        Machine = machine;
        Stun(entity);
        StateActive = true;
    }

    public void OnExitState(EntityBase entity)
    {
        entity.EntityStats.IsStunned = false;
        StateActive = false;
    }

    public void OnProcessState(float delta) {}
    public void OnPhysicsProcessState(float delta) {}
}