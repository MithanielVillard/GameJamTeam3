using GameJamTeam3.Scripts.Enums;
using Godot;

namespace GameJamTeam3.Scripts;

public partial class Wand : Node2D
{
	[Export]private int _attackDamage;
	[Export]private int _projectileNumber;
	[Export]private int _projectileRange;
	[Export]private int _projectileSpeed;
	
	[Export]private PackedScene _projectileScene;
	
	private Player _player;
	private PlayerMovement _playerMovement;
	private int childIndex;
	
	public override void _Ready()
	{
		_playerMovement = GetParent<PlayerMovement>();
		_player = _playerMovement.GetParent<Player>();

		_playerMovement.MovementDirection += HandleDirection;
		_player.FireProjectile += OnFireProjectile;
		
	}

	private void HandleDirection(Vector2 dir)
	{
		switch (dir)
		{
			case (0,-1)://up
				childIndex = 0;
				break;
			case (0,1)://down
				childIndex = 1;
				break;
			case (1,0)://right
				childIndex = 3;
				break;
			case (-1,0)://left
				childIndex = 5;
				break;
			case (0,0):
				if (childIndex == 3)
				{
					childIndex = 2;//far right
				}else if (childIndex == 5)
				{
					childIndex = 4;//far left
				}
				break;
		}
	}
	
	public void OnFireProjectile()
	{
		Projectile proj = _projectileScene.Instantiate<Projectile>();
		proj.GlobalPosition = GetChild<Node2D>(childIndex).GlobalPosition;
		proj.SetSpeed(_projectileSpeed);
		proj.SetDamage(_attackDamage);
		proj.LookAt(GetGlobalMousePosition());
		
		GetTree().GetCurrentScene().AddChild(proj);
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