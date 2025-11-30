using Godot;
using System;

public partial class CharacterScript : Area2D
{
	private void _on_body_entered( Node2D node )
	{
		if (node.Name == "Enemy")
		{
			return;
		}
		Room room = node as Room;
		room.FadeIn();
	}

	private void _on_body_exited( Node2D node )
	{
		if (node.Name == "Enemy")
		{
			return;
		}
		Room room = node as Room;
		room.FadeOut();
	}
}
