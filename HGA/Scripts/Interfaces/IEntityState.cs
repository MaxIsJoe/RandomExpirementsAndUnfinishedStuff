using Godot;
using HellGroundArcade.Scenes.Instances.Entities;

namespace HellGroundArcade.Scripts.Interfaces;

public interface IEntityState
{
    [Export()] public string StateID { get; set; }
    [Export()] public bool StateActive { get; set; }
    [Export()] public bool EnableOnReady { get; set; }
    public StateMachine.StateMachine Machine { get; set; }


    public bool CanEnterState();
    public void OnEnterState(EntityBase entity, StateMachine.StateMachine machine);
    public void OnExitState(EntityBase entity);
    public void OnProcessState(float delta);
    public void OnPhysicsProcessState(float delta);
}