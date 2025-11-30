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


	public void CreateFirstRoom()
	{
		RandomNumberGenerator generator = new RandomNumberGenerator();
	
		// Create Room

		int index = 0;
		int weight = generator.RandiRange( 0, TotalWeight );
		foreach ( ResourceRoom resourceRoom in ROOMS )
		{
			if ( weight <= resourceRoom.weight ) break;
			weight -= resourceRoom.weight;
			index++;
		}
		
		Room room = ROOMS[index].prefab.Instantiate<Room>();
		AddChild( room );
		room.SetPosition( Vector2.Zero );
		room.FirstRoomSetup();
	}
	
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
		CallDeferred( "CreateFirstRoom" );
	}
}
