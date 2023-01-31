using Godot;
using HellGroundArcade.Scenes.Instances.Abilities;
using HellGroundArcade.Scenes.Instances.Entities;

public class SmallSpeedBoost : AbilityBase
{

    [Export()] private float speedIncrease = 100;
    
    public override void OnAddAbility(EntityBase equppier)
    {
        base.OnAddAbility(equppier);
        equppier.EntityStats.SpeedMod += speedIncrease;
    }

    public override void OnRemoveAbility()
    {
        EquppiedOn.EntityStats.SpeedMod -= speedIncrease;
        base.OnRemoveAbility();
    }
}
