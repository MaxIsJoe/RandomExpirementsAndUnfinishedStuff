using Godot;
using System.Collections.Generic;
using HellGroundArcade.Scenes.Instances.Abilities;
using HellGroundArcade.Scenes.Instances.Entities;
using HellGroundArcade.Scripts.Interfaces;

public class PickBehavior : Node
{
    [Export()] private NodePath entityPath;
    private EntityBase entity;
    public readonly Queue<IPickupable> Pickupables = new Queue<IPickupable>();

    public override void _Ready()
    {
        base._Ready();
        entity = GetNode<EntityBase>(entityPath);
    }

    private void OnAreaEnter(Area2D area)
    {
        if (area is not IPickupable item) return;
        ShowTextLabel(area);
        if (item.PickupWithNoInput())
        {
            item.Pickup(entity);
        }
        else
        {
            Pickupables.Enqueue(item);
            GD.Print($"{area} is now queued.");
        }
    }

    private void OnAreaExit(Area2D area)
    { 
        if (area is not IPickupable c) return;
        if (Pickupables.Count != 0) Pickupables.Dequeue();
        HideTextLabel(area);
        GD.Print($"{area} is no longer queued.");
    }

    private void ShowTextLabel(Area2D area)
    {
        if (area is not AbilityBase item) return;
        item.NameText.Visible = true;
    }
    
    private void HideTextLabel(Area2D area)
    {
        if (area is not AbilityBase item) return;
        item.NameText.Visible = false;
    }
}
