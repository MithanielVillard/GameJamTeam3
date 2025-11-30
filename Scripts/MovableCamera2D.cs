using Godot;
using System;

public partial class MovableCamera2D : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process( double delta )
	{
		Vector2 movement = new Vector2( 0, 0 );
		if ( Input.IsActionPressed( "ui_up" ) )    movement.Y -= 200;
		if ( Input.IsActionPressed( "ui_left" ) )  movement.X -= 200;
		if ( Input.IsActionPressed( "ui_down" ) )  movement.Y += 200;
		if ( Input.IsActionPressed( "ui_right" ) ) movement.X += 200;
		movement.X *= (float)delta;
		movement.Y *= (float)delta;
		Translate( movement );
	}
}
