using Godot;
using HellGroundArcade.Scripts.Constants;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scenes.Instances.Entities.Player
{
    public class PlayerMovement : Node, IMovementInput
    {
        [Export] private NodePath EntityPath;
        private EntityBase entity;

        public override void _Ready()
        {
            entity = GetNode<EntityBase>(EntityPath);
        }

        public Vector2 MovementDirection()
        {
            if(entity.EntityStats.CanMove == false) return Vector2.Zero;
            var newInput = new Vector2(0,0)
            {
                x = Input.GetActionStrength(InputConstants.RIGHT_KEY) - Input.GetActionStrength(InputConstants.LEFT_KEY),
                y = Input.GetActionStrength(InputConstants.DOWN_KEY) - Input.GetActionStrength(InputConstants.UP_KEY)
            };
            return newInput;
        }
    }
}
