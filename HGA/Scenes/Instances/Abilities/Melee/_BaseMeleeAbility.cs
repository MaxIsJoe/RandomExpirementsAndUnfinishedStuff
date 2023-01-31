using Godot;
using HellGroundArcade.Scenes.Instances.Entities;

namespace HellGroundArcade.Scenes.Instances.Abilities.Melee;

public class _BaseMeleeAbility : AbilityBase
{
    public override void DoAbility()
    {
        base.DoAbility();
        var target = Pointer.GetCollider() as EntityBase;
        if (target == null || target == EquppiedOn) return;
        GD.Print($"{target.Name} will take {BaseDamage}. Their max corruption is at {target.EntityStats.MaxCorruption}.");
        target.EntityStats.TakeCorruption(BaseDamage);
    }
}