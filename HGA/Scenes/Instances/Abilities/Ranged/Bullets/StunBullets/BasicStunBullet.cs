using Godot;
using HellGroundArcade.Scenes.Instances.Entities;
using HellGroundArcade.Scripts.Constants;

namespace HellGroundArcade.Scenes.Instances.Abilities.Ranged.Bullets.StunBullets;

public class BasicStunBullet : BulletBase
{
    [Export()] private bool canDamage = false;
    protected override void OnHitBehavior(EntityBase entity)
    {
        entity.EnableState(StateMachineConstants.STATE_STUN);
        if(canDamage) base.OnHitBehavior(entity);
    }
}