using Godot;
using HellGroundArcade.Scenes.Instances.Abilities.Ranged.Bullets;

namespace HellGroundArcade.Scenes.Instances.Abilities.Ranged;

public class BaseRangedAbility : AbilityBase
{

    [Export()] private PackedScene bullet;
    
    public override void DoAbility()
    {
        base.DoAbility();
        if (bullet == null) return;
        var newBullet = bullet.Instance<BulletBase>();
        newBullet.Setup(EquppiedOn);
        GetTree().CurrentScene.AddChild(newBullet);
    }
}