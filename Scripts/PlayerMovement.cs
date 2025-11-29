using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	public const float Speed = 60.0f;

	
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("Left","Right","Up","Down");

		if (direction != Vector2.Zero)
		{
			this.Velocity = direction * Speed;
		}
		else
		{
			this.Velocity = Vector2.Zero;
		}
		
		//GetParent<Player>().test();
		
		MoveAndSlide();
	}
}
