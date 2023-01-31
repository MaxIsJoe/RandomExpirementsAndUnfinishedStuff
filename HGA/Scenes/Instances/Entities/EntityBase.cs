using Godot;
using HellGroundArcade.Scripts.Game;
using HellGroundArcade.Scripts.Interfaces;
using HellGroundArcade.Scripts.StateMachine;

namespace HellGroundArcade.Scenes.Instances.Entities
{
	public class EntityBase : KinematicBody2D
	{
		private IMovementInput _movement;
		[Export] private float _baseSpeed = 200;
		[Export] private NodePath _movementInterface;
		[Export] private NodePath _actionPointerPath;
		[Export] private NodePath _abilitiesPath;
		[Export] private NodePath _pickupBehaviorPath;
		[Export] private NodePath _statsPath;
		[Export] private NodePath _deathBehaviorPath;
		[Export] private NodePath _stateMachinePath;

		[Export] private bool isPlayer = false;
		
		public bool IsPlayer
		{
			get => isPlayer;
			set
			{
				if(MainGame.Instance != null) MainGame.Instance.CurrentPlayer = this;
				isPlayer = value;
			}
		}

		public ActionPointer ActionPointer;
		public EntityAbilities Abilities;
		public PickBehavior PickBehavior;
		public Stats EntityStats;
		public StateMachine EntityStateMachine;

	
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_movement = GetNodeOrNull<IMovementInput>(_movementInterface);
			ActionPointer = GetNodeOrNull<ActionPointer>(_actionPointerPath);
			Abilities = GetNodeOrNull<EntityAbilities>(_abilitiesPath);
			PickBehavior = GetNodeOrNull<PickBehavior>(_pickupBehaviorPath);
			EntityStats = GetNodeOrNull<Stats>(_statsPath);
			EntityStateMachine = GetNodeOrNull<StateMachine>(_stateMachinePath);
			if (_movement == null) GD.PushWarning("Movement method not found.");
			if (ActionPointer == null) GD.PushWarning("Action Pointer has not been found.");
			if (Abilities == null) GD.PushWarning("Abilities not been found.");
			if (PickBehavior == null) GD.PushWarning("PickBehavior not been found.");
			if (EntityStats == null) GD.PushError("Stats not been found.");
			if (EntityStateMachine == null) GD.PushError("StateMachine not found.");
		}

		private void ProcessMovement(float delta)
		{
			if (_movement == null || EntityStats == null) return;
			if (EntityStats.IsStunned) return;
			MoveAndCollide(_movement.MovementDirection() * (_baseSpeed + EntityStats.SpeedMod) * delta);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(float delta)
		{
			ProcessMovement(delta);      
		}

		public void Kill()
		{
			if (_deathBehaviorPath == null)
			{
				QueueFree();
				return;
			}
			var death = GetNodeOrNull<IDeathBehavior>(_deathBehaviorPath);
			if (death == null)
			{
				QueueFree();
				return;
			}
			death.OnDeath();
		}

		public void Damage(float dmg = 1)
		{
			EntityStats.TakeCorruption(dmg);
		}

		public void EnableState(string ID)
		{
			if (EntityStateMachine == null)
			{
				GD.PrintErr("[EntityBase/EnableState] - Attempted to enable a state in a statemachine that does not exist or assigned yet!!");
				return;
			}
			EntityStateMachine.AddState(ID);
		}
		
		public void RemoveState(string ID)
		{
			if (EntityStateMachine == null)
			{
				GD.PrintErr("[EntityBase/EnableState] - Attempted to disable a state in a statemachine that does not exist or assigned yet!!");
				return;
			}
			EntityStateMachine.RemoveState(ID);
		}
	}
}
