using Godot;
using System;

public partial class RoomSpawner : Node2D
{
	public Room existing;
	public Vector2I dungeonIndex;
	public int distance;

	
	public void SpawnRoom( bool forceSpawn )
	{
		RandomNumberGenerator generator = new RandomNumberGenerator();
		Vector2I direction = existing.dungeonIndex - dungeonIndex;
	
		// Create Room

		int index = 0;
		int weight = generator.RandiRange( 0, Dungeon.Node.TotalWeight );
		foreach ( ResourceRoom resourceRoom in Dungeon.Node.ROOMS )
		{
			if ( weight <= resourceRoom.weight ) break;
			weight -= resourceRoom.weight;
			index++;
		}
		
		Room room = Dungeon.Node.ROOMS[index].prefab.Instantiate<Room>();
		existing.GetParent().AddChild( room );
		room.SetPosition( GetPositionFromDungeonIndex( dungeonIndex ) );
		room.dungeonIndex = dungeonIndex;

		// Create Room Spawners

		if ( distance < Dungeon.Node.MaxDistance )
		{
			Godot.Collections.Array<Vector2I> directions = new Godot.Collections.Array<Vector2I> {
				new Vector2I( -direction.X, -direction.Y ),
				new Vector2I( direction.Y, -direction.X ),
				new Vector2I( -direction.Y, direction.X )
			};

			foreach ( Vector2I newDirection in directions )
			{
				Vector2I newDungeonIndex = dungeonIndex + newDirection;
				if ( Dungeon.Node.Rooms.ContainsKey( newDungeonIndex ) ) continue;
				if ( forceSpawn == false && generator.RandiRange( 0, 1 ) == 0 ) continue;
				RoomSpawner roomSpawner = new RoomSpawner();
				room.AddChild( roomSpawner );
				roomSpawner.existing = room;
				roomSpawner.dungeonIndex = newDungeonIndex;
				roomSpawner.distance = distance + 1;
				Dungeon.Node.Rooms[newDungeonIndex] = true;
				Dungeon.Node.RoomSpawners.Add( roomSpawner );
			}
		}
		
		// Place Exit and Enter Tiles
		
		room.PlaceExitTile( direction * 5 );
		room.PlaceExitTile( direction * 6 );
		existing.PlaceExitTile( direction * -5 );
		existing.PlaceExitTile( direction * -6 );
		
		// Free this Room Spawner
		
		QueueFree();
	}

	private Vector2I GetPositionFromTile( Vector2I dungeonIndex, Vector2I tile )
	{
		return new Vector2I( 32 * ( tile.X - tile.Y ), 16 * ( tile.X + tile.Y ) )
		       + GetPositionFromDungeonIndex( dungeonIndex );
	}

	private Vector2I GetPositionFromDungeonIndex( Vector2I dungeonIndex )
	{
		return new Vector2I( 480 * ( dungeonIndex.X - dungeonIndex.Y ), 240 * ( dungeonIndex.X + dungeonIndex.Y ) );
	}
}
