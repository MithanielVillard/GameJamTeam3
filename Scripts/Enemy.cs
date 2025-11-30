using Godot;
using System;
using GameJamTeam3.Scripts;

public partial class Enemy : Node2D
{
	[Export]private int _maxHealth = 100;
	[Export]private int _health = 100;
	[Export]private float _attackSpeed = 2;
	[Export]private int _movementSpeed;
	
	private EnemyMovement _movementScript;
	private Player _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_movementScript = GetChild<EnemyMovement>(0);
		_movementScript.SetMovementSpeed(_movementSpeed);

		_player = GetParent().GetChild<Player>(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Vector2 GetPlayerpos()
	{
		return _player.GetChild<PlayerMovement>(0).GlobalPosition;
	}

	public void ReceiveDamage(int damage)
	{
		_health -= damage;
		
		if(_health <= 0)
			QueueFree();
	}
}
