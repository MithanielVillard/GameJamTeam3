using Godot;
using System;

public partial class PlayerAnimation : AnimatedSprite2D
{

	private PlayerMovement _playerMovement;
	
	public override void _Ready()
	{
		_playerMovement = GetParent<PlayerMovement>();
		
		_playerMovement.MovementDirection += HandleDirection;

	}

	private void HandleDirection(Vector2 dir)
	{
		switch (dir)
		{
			case (0,-1)://up
				OnUp();
				break;
			case (0,1)://down
				OnDown();
				break;
			case (1,0)://right
				OnRight();
				break;
			case (-1,0)://left
				OnLeft();
				break;
			case (0,0):
				OnIdle();
				break;
		}
	}
	
	public void OnUp()
	{
		PlayAnim("Up");
		Position = Vector2.Zero;
	}
	public void OnDown()
	{
		PlayAnim("Down");
		Position = Vector2.Zero;
	}
	public void OnRight()
	{
		PlayAnim("Right");
		Position = Vector2.Right * 60;
	}
	public void OnLeft()
	{
		PlayAnim("Left");
		Position = Vector2.Left * 60;
		
	}
	
	public void OnIdle()
	{
		string animationame = Animation.ToString();
		if (animationame.Length > 4)
		{
			if(animationame.Substring(animationame.Length - 4) == "Idle")
				return;
		}
		
		
		Play(Animation + "Idle");
		Position = Vector2.Zero;
	}
	
	private void PlayAnim(string animationName)
	{
		if (animationName == null)
			return;
		
		if(Animation.ToString() == animationName)
			return;
		
		Play(animationName);
	}
}
