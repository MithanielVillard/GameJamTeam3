using Godot;
using System;
using System.Diagnostics;
using GameJamTeam3.Scripts.Enums;


public partial class Wand : Node2D
{
	[Export]private int _attackDamage = 40;
	[Export]private int _projectileNumber = 1;
	[Export]private int _projectileRange = 5;
	[Export]private int _projectileSpeed = 5;
	
	public void OnFireProjectile()
	{
		Debug.Print("OnFireProjectile");
	}
	
	public void SetWandStats(StatsWand stats, StatsOperator statsoperator, StatsOperationUnit operatorunit, int value)
	{
		switch (stats)
		{
			case StatsWand.ATTACK_DAMAGE:
				_attackDamage = ApplyOperator(statsoperator,operatorunit, value,_attackDamage);
				break;
			
			case StatsWand.PROJECTILE_NUMBER:
				_projectileNumber = ApplyOperator(statsoperator,operatorunit, value,_projectileNumber);
				break;
			
			case StatsWand.PROJECTILE_RANGE:
				_projectileRange = ApplyOperator(statsoperator,operatorunit, value,_projectileRange);
				break;
			
			case StatsWand.PROJECTILE_SPEED:
				_projectileSpeed = ApplyOperator(statsoperator,operatorunit, value,_projectileSpeed);
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
