using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	public float ActualSpeed = 60.0f;


	
	
	public void SetMovementSpeed(float speed)
	{
		ActualSpeed = speed;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("Left","Right","Up","Down");

		if (direction != Vector2.Zero)
		{
			this.Velocity = direction.Normalized() * ActualSpeed;
		}
		else
		{
			this.Velocity = Vector2.Zero;
		}
		
		//GetParent<Player>().test();
		
		MoveAndSlide();
	}
}
