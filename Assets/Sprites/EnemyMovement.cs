using Godot;
using System;

public partial class EnemyMovement : CharacterBody2D
{
	public float ActualSpeed = 60.0f;
	
	private Enemy _enemy;
	
	[Signal] public delegate void MovementDirectionEnemyEventHandler(Vector2 dir);

	public override void _Ready()
	{
		_enemy = GetParent<Enemy>();
	}

	public void SetMovementSpeed(float speed)
	{
		ActualSpeed = speed;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = _enemy.GetPlayerpos() - GlobalPosition;
		
		if (direction != Vector2.Zero)
		{
			Velocity = direction.Normalized() * ActualSpeed;
			//_enemy.EmitSignal(Player.SignalName.Move);
		}
		else
		{
			Velocity = Vector2.Zero;
			//_enemy.EmitSignal(Player.SignalName.Idle);
		}
		
		EmitSignal(SignalName.MovementDirectionEnemy,  direction);
		MoveAndSlide();
	}
}
