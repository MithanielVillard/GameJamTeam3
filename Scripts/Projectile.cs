using Godot;
using System;

public partial class Projectile : Area2D
{
	
	
	private float _speed;
	private int _damage;
	
	[Export]private PackedScene _VfxHit;
	

	public override void _Ready()
	{
		Sprite2D shadow = GetChild<Sprite2D>(2);
		if(shadow != null)
			shadow.GlobalPosition += Vector2.Down * 15;

		BodyEntered += InstantiateHitVfx;
	}

	public override void _Process(double delta)
	{
		Position = Position + Vector2.FromAngle(GlobalRotation) * ((float)delta * _speed);
	}

	public void SetSpeed(float newspeed)
	{
		_speed = newspeed;
	}
	public void SetDamage(int damage)
	{
		_damage = damage;
	}
	
	public void InstantiateHitVfx(Node2D body)
	{
		//if enemy hit
		
		AnimatedSprite2D proj = _VfxHit.Instantiate<AnimatedSprite2D>();
		proj.GlobalPosition = GlobalPosition;
		GetTree().GetCurrentScene().AddChild(proj);

		
		QueueFree();
		
		if (body.GetParent<Enemy>() != null)
		{
			body.GetParent<Enemy>().ReceiveDamage(_damage);
		}
			
		
	}
}
