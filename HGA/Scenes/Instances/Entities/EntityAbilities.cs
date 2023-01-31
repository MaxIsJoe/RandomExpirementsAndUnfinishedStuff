using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using HellGroundArcade.Scenes.Instances.Abilities;
using HellGroundArcade.Scripts.Constants;

namespace HellGroundArcade.Scenes.Instances.Entities
{
    public class EntityAbilities : Node2D
    {
        [Export()] private NodePath _entityParentPath;
        [Export()] private NodePath _passiveAbilityHolderPath;
        [Export()] private NodePath _mainAbilityHolderPath;
        [Export()] private NodePath _secondaryAbilityHolderPath;
        private EntityBase _entity;
        private Node2D _passiveAbilityHolder;
        private Node2D _mainAbilityHolder;
        private Node2D _secondaryAbilityHolder;
        private readonly List<AbilityBase> _abilities = new List<AbilityBase>();

        public override void _Ready()
        {
            base._Ready();
            _entity = GetNode<EntityBase>(_entityParentPath);
            _passiveAbilityHolder = GetNode<Node2D>(_passiveAbilityHolderPath);
            _mainAbilityHolder = GetNode<Node2D>(_mainAbilityHolderPath);
            _secondaryAbilityHolder = GetNode<Node2D>(_secondaryAbilityHolderPath);
        }

        public bool HasAbilityType(AbilityBase.AbilityType abilityType)
        {
            return _abilities.Any(ability => ability.Type == abilityType);
        }

        public void AddAbility(AbilityBase ability)
        {
            if (_abilities.Any(x => x.Type == ability.Type)) return;
            _abilities.Add(ability);
            ability.OnAddAbility(_entity);
            if (ability.GetParent() != null)
            {
                ability.GetParent().RemoveChild(ability);
            }
            switch (ability.Type)
            {
                case AbilityBase.AbilityType.MainAction:
                    _mainAbilityHolder.AddChild(ability);
                    break;
                case AbilityBase.AbilityType.PassiveAction:
                    _passiveAbilityHolder.AddChild(ability);
                    break;
                case AbilityBase.AbilityType.SecondaryAction:
                    _secondaryAbilityHolder.AddChild(ability);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ability.Position = Vector2.Zero;
        }

        public void RemoveAbility(AbilityBase ability)
        {
            _abilities.Remove(ability);
            ability.OnRemoveAbility();
        }
        
        public void RemoveAbility(AbilityBase.AbilityType type)
        {
            foreach (var ability in _abilities.Where(ability => type == ability.Type).Reverse())
            {
                _abilities.Remove(ability);
                ability.OnRemoveAbility();
                if (ability.GetParent() == null) continue;
                ability.GetParent().RemoveChild(ability);
                _entity.GetParent().AddChild(ability);
                ability.Position = _entity.ActionPointer.PointIndicator.GlobalPosition;
            }
        }

        private void UsePrimaryAbility()
        {
            // Despite the apparent design allowing only for one main ability
            // This approach lets me sneak in extra abilities without anyone noticing.
            foreach (var ability in _abilities.Where(ability => ability.Type == AbilityBase.AbilityType.MainAction))
            {
                ability.DoAbility();
            }
        }

        private void UseSecondaryAbility()
        {
            foreach (var ability in _abilities.Where(ability => ability.Type == AbilityBase.AbilityType.SecondaryAction))
            {
                ability.DoAbility();
            }
        }

        
        public override void _Process(float delta)
        {
            if (_entity.IsPlayer == false) return;
            if (Input.IsActionJustPressed(InputConstants.ACTION_KEY)) UsePrimaryAbility();
            if (Input.IsActionJustPressed(InputConstants.SEC_ACTION_KEY)) UseSecondaryAbility();
            if (Input.IsActionJustPressed(InputConstants.USE_KEY))
            {
                GD.Print(_entity.PickBehavior.Pickupables.Count);
                if(_entity.PickBehavior.Pickupables == null || _entity.PickBehavior.Pickupables.Count == 0) return;
                _entity.PickBehavior.Pickupables.Dequeue().Pickup(_entity);
            }
        }
    }
}
