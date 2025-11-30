// using Godot;
// using System;
// using System.Collections.Generic;
// using Godot.Collections;
//
// public partial class OldRoom : TileMap
// {
// 	private static Array<PackedScene> Rooms = new Array<PackedScene>()
// 	{
// 		GD.Load<PackedScene>( "res://Prefabs/room-single.tscn" )
// 	};
// 	private static System.Collections.Generic.Dictionary<Vector2I, bool> Dungeon = new System.Collections.Generic.Dictionary<Vector2I, bool>();
//
// 	[Export] public ResourceRoomExit positions;
// 	private Vector2I dungeonIndex = new Vector2I();
// 	
// 	private List<Vector2I> topPositions = new List<Vector2I>();
// 	private List<Vector2I> leftPositions = new List<Vector2I>();
// 	private List<Vector2I> bottomPositions = new List<Vector2I>();
// 	private List<Vector2I> rightPositions = new List<Vector2I>();
// 	private List<OldRoom> topRooms = new List<OldRoom>();
// 	private List<OldRoom> leftRooms = new List<OldRoom>();
// 	private List<OldRoom> bottomRooms = new List<OldRoom>();
// 	private List<OldRoom> rightRooms = new List<OldRoom>();
//
// 	public void PlaceExit( Vector2I position )
// 	{
// 		SetCell( 0, position, 1, new Vector2I( 1, 0 ) );
// 		EraseCell( 1, new Vector2I( position.X-1, position.Y-1 ) );
// 		EraseCell( 2, new Vector2I( position.X-2, position.Y-2 ) );
// 	}
//
// 	public void GenerateNexRooms()
// 	{
// 		RandomNumberGenerator generator = new RandomNumberGenerator();
// 		for ( int i = 0; i < topPositions.Count; i++ )
// 		{
// 			OldRoom room = Rooms[generator.RandiRange( 0, Rooms.Count - 1 )].Instantiate<OldRoom>();
// 			GetTree().GetCurrentScene().AddChild( room );
// 			// topRooms.Add( room );
// 			// topRooms[i].bottomPositions.Add( topRooms[i].positions.bottomPositions[0] );
// 			// topRooms[i].PlaceExit( topRooms[i].bottomPositions[0] );
// 			// topRooms[i].dungeonIndex = new Vector2I( dungeonIndex.X, dungeonIndex.Y+1 );
// 			// topRooms[i].PlaceExits();
// 		}
// 		// for ( int i = 0; i < leftPositions.Count; i++ )
// 		// {
// 		// 	leftRooms[i] = Rooms[generator.RandiRange( 0, Rooms.Count-1 )].Instantiate<OldRoom>();
// 		// 	leftRooms[i].rightPositions.Add( leftRooms[i].positions.rightPositions[0] );
// 		// 	leftRooms[i].PlaceExit( leftRooms[i].rightPositions[0] );
// 		// 	leftRooms[i].dungeonIndex = new Vector2I( dungeonIndex.X, dungeonIndex.Y+1 );
// 		// 	// leftRooms[i].PlaceExits();
// 		// }
// 		// for ( int i = 0; i < bottomPositions.Count; i++ )
// 		// {
// 		// 	bottomRooms[i] = Rooms[generator.RandiRange( 0, Rooms.Count-1 )].Instantiate<OldRoom>();
// 		// 	bottomRooms[i].topPositions.Add( bottomRooms[i].positions.topPositions[0] );
// 		// 	bottomRooms[i].PlaceExit( bottomRooms[i].topPositions[0] );
// 		// 	bottomRooms[i].dungeonIndex = new Vector2I( dungeonIndex.X, dungeonIndex.Y+1 );
// 		// 	// bottomRooms[i].PlaceExits();
// 		// }
// 		// for ( int i = 0; i < rightPositions.Count; i++ )
// 		// {
// 		// 	rightRooms[i] = Rooms[generator.RandiRange( 0, Rooms.Count-1 )].Instantiate<OldRoom>();
// 		// 	rightRooms[i].leftPositions.Add( rightRooms[i].positions.leftPositions[0] );
// 		// 	rightRooms[i].PlaceExit( rightRooms[i].leftPositions[0] );
// 		// 	rightRooms[i].dungeonIndex = new Vector2I( dungeonIndex.X, dungeonIndex.Y+1 );
// 		// 	// rightRooms[i].PlaceExits();
// 		// }
// 	}
//
// 	public void PlaceExits()
// 	{
// 		RandomNumberGenerator generator = new RandomNumberGenerator();
// 		dungeonIndex.Y--;
// 		foreach ( Vector2I position in positions.topPositions )
// 		{
// 			if ( Dungeon.ContainsKey(dungeonIndex) && Dungeon[dungeonIndex] ) continue;
// 			// if ( generator.RandiRange( 0, 1 ) == 0 ) continue;
// 			topPositions.Add( position );
// 			PlaceExit( position );
// 			Dungeon[dungeonIndex] = true;
// 		}
// 		dungeonIndex.Y++;
// 		dungeonIndex.X--;
// 		foreach ( Vector2I position in positions.leftPositions )
// 		{
// 			if ( Dungeon.ContainsKey(dungeonIndex) && Dungeon[dungeonIndex] ) continue;
// 			// if ( generator.RandiRange( 0, 1 ) == 0 ) continue;
// 			leftPositions.Add( position );
// 			PlaceExit( position );
// 			Dungeon[dungeonIndex] = true;
// 		}
// 		dungeonIndex.X++;
// 		dungeonIndex.Y++;
// 		foreach ( Vector2I position in positions.bottomPositions )
// 		{
// 			if ( Dungeon.ContainsKey(dungeonIndex) && Dungeon[dungeonIndex] ) continue;
// 			// if ( generator.RandiRange( 0, 1 ) == 0 ) continue;
// 			bottomPositions.Add( position );
// 			PlaceExit( position );
// 			Dungeon[dungeonIndex] = true;
// 		}
// 		dungeonIndex.Y--;
// 		dungeonIndex.X++;
// 		foreach ( Vector2I position in positions.rightPositions )
// 		{
// 			if ( Dungeon.ContainsKey(dungeonIndex) && Dungeon[dungeonIndex] ) continue;
// 			// if ( generator.RandiRange( 0, 1 ) == 0 ) continue;
// 			rightPositions.Add( position );
// 			PlaceExit( position );
// 			Dungeon[dungeonIndex] = true;
// 		}
// 		dungeonIndex.X--;
// 	}
//
// 	public override void _Ready() {}
// 	public override void _Process( double delta ) {}
// }
