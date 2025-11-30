using Godot;
using System;

public partial class CharacterScript : Area2D
{
	private void _on_body_entered( Node2D node )
	{
		Room room = node as Room;
		room.FadeIn();
	}

	private void _on_body_exited( Node2D node )
	{
		Room room = node as Room;
		room.FadeOut();
	}
}
