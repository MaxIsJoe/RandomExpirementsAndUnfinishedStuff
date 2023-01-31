using Godot;

namespace HellGroundArcade.Scripts;

public class HoverAnim : Node2D
{
    public override void _Process(float delta)
    {
        base._Process(delta);
        var pivPos = Position;
        pivPos.y = Mathf.Cos(Godot.Time.GetTicksMsec() * delta * 0.20f) * 10;
        Position = pivPos;
    }
}