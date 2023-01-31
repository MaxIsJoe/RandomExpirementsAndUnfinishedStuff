using Godot;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scenes.Instances.Entities.Player
{
    public class MousePointBehavior : Node2D, ILookAtBehavior
    {
        public Vector2 LookAtResult()
        {
            return GetGlobalMousePosition();
        }
    }
}
