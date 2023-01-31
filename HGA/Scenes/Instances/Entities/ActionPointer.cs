using Godot;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scenes.Instances.Entities
{
    public class ActionPointer : RayCast2D
    {
        [Export] private NodePath _pointIndicatorPath;
        [Export] private NodePath _lookAtBehaviorPath;
        public Sprite PointIndicator;
        private ILookAtBehavior _pointingBehavior;


        public override void _Ready()
        {
            PointIndicator = GetNode<Sprite>(_pointIndicatorPath);
            _pointingBehavior = GetNodeOrNull<ILookAtBehavior>(_lookAtBehaviorPath);
        }

        public override void _Process(float delta)
        {
            if (PointIndicator == null || _pointingBehavior == null) return;
            LookAt(_pointingBehavior.LookAtResult());
            PointIndicator.Position = CastTo / 2;
        }

    }
}
