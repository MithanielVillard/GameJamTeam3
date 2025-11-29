using Godot;
using System;
using GameJamTeam3.Scripts.Enums;

public partial class Player : Node2D
{
	[Export]private int _healthRegen = 0;
	[Export]private int _maxHealth = 100;
	[Export]private int _health = 100;
	[Export]private int _luck = 15;
	[Export]private int _defense = 50;
	[Export]private int _attackSpeed = 2;
	[Export]private int _movementSpeed = 10;
	
	private PlayerMovement _movementScript;
	
	public override void _Ready()
	{
		
		_movementScript = this.GetChild<PlayerMovement>(0);
		_movementScript.SetMovementSpeed(_movementSpeed);
	}

	public void SetPlayerStats(Stats stats, StatsOperator statsoperator, StatsOperationUnit operatorunit, int value)
	{
		
		
		switch (stats)
		{
			case Stats.HEALTH_REGEN:
				_healthRegen = ApplyOperator(statsoperator,operatorunit, value,_healthRegen);
				break;
			
			case Stats.MAX_HEALTH:
				_maxHealth = ApplyOperator(statsoperator,operatorunit, value,_maxHealth);
				break;
			
			case Stats.HEALTH:
				_health = ApplyOperator(statsoperator,operatorunit, value,_health);
				break;
			
			case Stats.LUCK:
				_luck = ApplyOperator(statsoperator,operatorunit, value,_luck);
				break;
			
			case Stats.DEFENSE:
				_defense = ApplyOperator(statsoperator,operatorunit, value,_defense);
				break;
			
			case Stats.ATTACK_SPEED:
				_attackSpeed = ApplyOperator(statsoperator,operatorunit, value,_attackSpeed);
				break;
			
			case  Stats.MOVEMENT_SPEED:
				_movementSpeed = ApplyOperator(statsoperator,operatorunit, value,_movementSpeed);
				_movementScript.SetMovementSpeed(_movementSpeed);
				break;
		}
	}

	public int ApplyOperator(StatsOperator statsoperator,StatsOperationUnit operatorunit, int value, int baseValue)
	{
		int result = 0;
		int valuetooperate = value;
		
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
