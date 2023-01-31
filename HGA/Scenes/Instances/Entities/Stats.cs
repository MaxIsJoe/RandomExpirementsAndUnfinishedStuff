using System;
using Godot;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scenes.Instances.Entities;

public class Stats : Node
{
    [Export()] private NodePath entityPath;
    private EntityBase entity;
    [Export(PropertyHint.ExpRange, "10, 10000000000000")] public float MaxCorruption = 10;
    [Export()] public float Power = 1;
    [Export()] public float SpeedMod = 50;
    [Export()] public bool CanMove = true;
    public bool IsStunned = false;
    private float _corruption = 0;

    public override void _Ready()
    {
        base._Ready();
        entity = GetNodeOrNull<EntityBase>(entityPath);
    }

    public void TakeCorruption(float damage)
    {
        _corruption += damage;
        if (_corruption < MaxCorruption) return;
        entity.Kill();
    }
    
    public void HealCorruption(float damage)
    {
        _corruption -= damage;
        _corruption = Math.Clamp(_corruption, 0, float.PositiveInfinity);
    }
}