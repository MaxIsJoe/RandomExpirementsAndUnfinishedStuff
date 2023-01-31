using Godot;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scenes.Instances.Entities.Enemies;

public class EnemyLookBehavior : Node2D, ILookAtBehavior
{
    [Export()] private NodePath _entityPath;
    [Export()] private EntityBase _target;
    private EntityBase _entityBase;

    public override void _Ready()
    {
        base._Ready();
        _entityBase = GetNodeOrNull<EntityBase>(_entityPath);
        _target ??= GetTree().GetNodesInGroup("Player")[0] as EntityBase;
    }

    public Vector2 LookAtResult()
    {
        return _target?.Position ?? Vector2.Zero;
    }
}