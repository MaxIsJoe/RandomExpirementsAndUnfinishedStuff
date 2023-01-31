using Godot;
using HellGroundArcade.Scenes.Instances.Entities;

namespace HellGroundArcade.Scenes.Instances.Abilities.Ranged.Bullets;

public class BulletBase : KinematicBody2D
{
    [Export()] private int _damage = 5;
    [Export()] private float _travelSpeed = 600;
    [Export()] private bool _canHarmOwner = true;
    private EntityBase _owner = null;
    private bool canMove = false;
    private Vector2 shootingDirection = Vector2.Zero;
    
    private const float MINIMUM_TRAVEL_SPEED = 200f;


    public void Setup(EntityBase shooter)
    {
        if (shooter == null)
        {
            GD.Print("[BulletBase/Setup] - No owner found! skipping setup..");
            return;
        }
        _owner = shooter;
        _damage += (int) _owner.EntityStats.Power;
        shootingDirection = (_owner.ActionPointer.PointIndicator.GlobalPosition -
                            _owner.GlobalPosition).Normalized();
        GlobalPosition = _owner.ActionPointer.PointIndicator.GlobalPosition;
        canMove = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if(canMove == false) return;
        MoveAndCollide(shootingDirection * (MINIMUM_TRAVEL_SPEED + _travelSpeed) * delta);
        LookAt(shootingDirection);
    }

    private void OnHit(Node body)
    {
        if (body is not EntityBase c) return;
        if (c == _owner && _canHarmOwner == false) return;
        OnHitBehavior(c);
        QueueFree();
    }

    protected virtual void OnHitBehavior(EntityBase entity)
    {
        entity.Damage(_damage);
    }
}