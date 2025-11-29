using Godot;
using System;
using Godot.Collections;

public partial class Room : TileMap
{
	private static Array<bool> Dungeon { get; set; }
	
	[Export]
	private Array<Vector2> positions { get; set; }
	private Array<Room> rooms { get; set; }

	public void GenerateNexRooms()
	{
		for (int i = 0; i < rooms.Count; i++)
		{
			rooms[i];
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process( double delta )
	{
	}
}
