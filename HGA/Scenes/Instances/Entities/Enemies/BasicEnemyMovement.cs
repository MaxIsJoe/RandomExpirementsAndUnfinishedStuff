using Godot;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scenes.Instances.Entities.Enemies;

public class BasicEnemyMovement : Node, IMovementInput
{
    [Export()] private NodePath _entityPath;
    private EntityBase _entityBase;

    public override void _Ready()
    {
        base._Ready();
        _entityBase = GetNodeOrNull<EntityBase>(_entityPath);
    }

    public Vector2 MovementDirection()
    {
        return _entityBase.ActionPointer.CastTo.IsEqualApprox(Vector2.Zero) ? 
            Vector2.Zero : _entityBase.Position.DirectionTo(_entityBase.ActionPointer.PointIndicator.GlobalPosition);
    }
}