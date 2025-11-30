using Godot;
using System;
using GameJamTeam3.Scripts;
using GameJamTeam3.Scripts.Enums;

public partial class Player : Node2D
{
	[Export]public int _healthRegen = 0;
	[Export]public int _maxHealth = 100;
	[Export]public int _health = 100;
	[Export]public int _luck = 15;
	[Export]public int _defense = 50;
	[Export]public float _attackSpeed = 2;
	[Export]public int _movementSpeed = 10;

	private float lastTimeFired = 0;
	private float processedAttackSpeed = 0;
	private bool _isTryingFire;
	
	private PlayerMovement _movementScript;
	public Wand PlayerWand;

	
	// -=========- SIGNALS FOR ENCHANTMENTS -==========-
	[Signal] public delegate void StatChangedEventHandler(StatsPlayer statChanged);
	[Signal] public delegate void FireProjectileEventHandler();
	[Signal] public delegate void MoveEventHandler();
	[Signal] public delegate void IdleEventHandler();
	[Signal] public delegate void EnchantmentReceivedEventHandler(Enchantment enchantmentReceived);
	
	// -=========- SIGNALS FOR ENCHANTMENTS -==========-
	
	public override void _Ready()
	{
		_movementScript = GetChild<GameJamTeam3.Scripts.PlayerMovement>(0);
		_movementScript.SetMovementSpeed(_movementSpeed);
		
		PlayerWand = _movementScript.GetChild<GameJamTeam3.Scripts.Wand>(0);

		ProccesAttackSpeed();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Attack"))
		{
			_isTryingFire =  true;
		}
		else if (@event.IsActionReleased("Attack"))
		{
			_isTryingFire =  false;
		}
	}

	public void TryFireProjectile()
	{
		if( (float)Time.GetTicksMsec() >= lastTimeFired + processedAttackSpeed)
		{
			EmitSignal(SignalName.FireProjectile);
			lastTimeFired = (float)Time.GetTicksMsec();
		}
	}
	
	public override void _Process(double delta)
	{
		if (_isTryingFire)
		{
			TryFireProjectile();
		}
	}

	public void ProccesAttackSpeed()
	{
		if (_attackSpeed == 0)
		{
			_attackSpeed = 0.0001f;
		}
		
		processedAttackSpeed = 1000 / _attackSpeed;
	}
	
	public void SetPlayerStats(StatsPlayer stats, StatsOperator statsoperator, StatsOperationUnit operatorunit, float value)
	{
		switch (stats)
		{
			case StatsPlayer.HEALTH_REGEN:
				_healthRegen = (int)ApplyOperator(statsoperator,operatorunit, value,(float)_healthRegen);
				break;
			
			case StatsPlayer.MAX_HEALTH:
				_maxHealth = (int)ApplyOperator(statsoperator,operatorunit, value,(float)_maxHealth);
				break;
			
			case StatsPlayer.HEALTH:
				_health = (int)ApplyOperator(statsoperator,operatorunit, value,(float)_health);
				break;
			
			case StatsPlayer.LUCK:
				_luck = (int)ApplyOperator(statsoperator,operatorunit, value,(float)_luck);
				break;
			
			case StatsPlayer.DEFENSE:
				_defense = (int)ApplyOperator(statsoperator,operatorunit, value,(float)_defense);
				break;
			
			case StatsPlayer.ATTACK_SPEED:
				_attackSpeed = ApplyOperator(statsoperator,operatorunit, value,_attackSpeed);
				ProccesAttackSpeed();
				break;
			
			case StatsPlayer.MOVEMENT_SPEED:
				_movementSpeed = (int)ApplyOperator(statsoperator,operatorunit, value,(float)_movementSpeed);
				_movementScript.SetMovementSpeed(_movementSpeed);
				break;
		}
	}

	public float ApplyOperator(StatsOperator statsoperator,StatsOperationUnit operatorunit, float value, float baseValue)
	{
		float result = 0;
		float valuetooperate = value;
		
		if (operatorunit == StatsOperationUnit.PERCENTAGE)
		{
			valuetooperate = baseValue * value;
		}

		switch (statsoperator)
		{
			case StatsOperator.PLUS:
				result = baseValue += valuetooperate;
				break;
			case StatsOperator.MINUS:
				result = baseValue /= valuetooperate;
				break;
			case StatsOperator.MULTIPLIED:
				result = baseValue *= valuetooperate;
				break;
			case StatsOperator.DIVIDED:
				result = baseValue /= valuetooperate;
				break;
			
		}
		return result;
	}
}
