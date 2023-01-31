using Godot;
using HellGroundArcade.Scenes.Instances.Entities;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scenes.Instances.Abilities
{
    public abstract class AbilityBase : Area2D, IPickupable
    {
        [Export] public float BaseDamage = 5f;
        [Export] public string AbilityName = "???";
        [Export] public bool CanBePickedUp = true;
        [Export] public AbilityType Type;
        public bool IsOnCooldown = false;
        public RichTextLabel NameText;
        public Area2D HurtBox;
    
        [Export(PropertyHint.Range, "How much cooldown does this ability have? 0 for none.")]
        protected float CooldownTime = 0f;
        [Export] protected NodePath nameTextPath;
        [Export] protected NodePath hurtBoxPath;
        protected bool IsEquipped = false;
        protected EntityBase EquppiedOn;
        protected ActionPointer Pointer;

        public enum AbilityType
        {
            MainAction,
            PassiveAction,
            SecondaryAction
        }

        public override void _Ready()
        {
            base._Ready();
            NameText = GetNodeOrNull<RichTextLabel>(nameTextPath);
            HurtBox = GetNodeOrNull<Area2D>(hurtBoxPath);
            if (NameText != null)
            {
                NameText.BbcodeText = $"[center]{AbilityName}[/center]";
                NameText.Visible = false;
            }
        }

        public virtual void DoAbility(){}
        public virtual void OnAddAbility(EntityBase equppier)
        {
            EquppiedOn = equppier;
            Pointer = equppier.ActionPointer;
            IsEquipped = true;
            Monitorable = false;
        }

        public virtual void OnRemoveAbility()
        {
            EquppiedOn = null;
            Pointer = null;
            IsEquipped = false;
            Monitorable = true;
        }
        
        public virtual void ManageCooldown(){}
        
        public void Pickup(EntityBase entity)
        {
            if (entity.Abilities.HasAbilityType(Type))
            {
                entity.Abilities.RemoveAbility(Type);
            }
            entity.Abilities.AddAbility(this);
            GD.Print("added ability");
        }
    }
}
