using Godot;

namespace GameJamTeam3.Scripts;

public partial class PlayerMovement : CharacterBody2D
{
	public float ActualSpeed = 60.0f;
	
	private PlayerAnimation _animationPlayer;
	private Player _player;
	
	[Signal] public delegate void MovementDirectionEventHandler(Vector2 dir);

	public override void _Ready()
	{
		_player = GetParent<Player>();
	}

	public void SetMovementSpeed(float speed)
	{
		ActualSpeed = speed;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("Left","Right","Up","Down");

		if (direction != Vector2.Zero)
		{
			Velocity = direction.Normalized() * ActualSpeed;
			_player.EmitSignal(Player.SignalName.Move);
		}
		else
		{
			Velocity = Vector2.Zero;
			_player.EmitSignal(Player.SignalName.Idle);
		}
		
		EmitSignal(SignalName.MovementDirection,  direction);
		MoveAndSlide();
	}
}