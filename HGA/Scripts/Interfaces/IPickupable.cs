using HellGroundArcade.Scenes.Instances.Entities;

namespace HellGroundArcade.Scripts.Interfaces
{
    public interface IPickupable
    {
        bool PickupWithNoInput()
        {
            return false;
        }
        void Pickup(EntityBase entity);
    }
}