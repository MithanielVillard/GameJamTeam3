using Godot;
using System;

public partial class CharacterScript : Area2D
{
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

	private void _on_area_2d_body_entered( Node2D node )
	{
		Room room = node as Room;
		room.FadeIn();
	}

	private void _on_area_2d_body_exited( Node2D node )
	{
		Room room = node as Room;
		room.FadeOut();
	}
}
