using Godot;
using System;

using ResourceRooms = Godot.Collections.Array<ResourceRoom>;
using SpawnedRoomsType = System.Collections.Generic.Dictionary<Godot.Vector2I, bool>;
using RoomSpawnersType = System.Collections.Generic.List<RoomSpawner>;

public partial class Dungeon : Node
{
	public static Dungeon Node;

	[Export] public ResourceRooms ROOMS;
	
	[Export] public int MaxRooms = 25;
	[Export] public int MaxDistance = 6;

	public SpawnedRoomsType Rooms = new SpawnedRoomsType();
	public RoomSpawnersType RoomSpawners = new RoomSpawnersType();

	public int TotalWeight;

	
	public void GenerateDungeon()
	{
		int i = 0;
		while ( true )
		{
			if ( i < MaxRooms ) RoomSpawners[i].SpawnRoom( i == RoomSpawners.Count-1 );
			else
			{
				Rooms.Remove( RoomSpawners[i].dungeonIndex );
				RoomSpawners[i].QueueFree();
			}
			i++;
			if ( i == RoomSpawners.Count ) break;
		}
	}

	public override void _Ready()
	{
		Node = this;
		foreach ( ResourceRoom resourceRoom in ROOMS )
			TotalWeight += resourceRoom.weight;
	}
}
