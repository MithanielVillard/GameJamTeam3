using Godot;
using System;

using RoomArray = Godot.Collections.Array<Godot.PackedScene>;
using Dungeon = System.Collections.Generic.Dictionary<Godot.Vector2I, bool>;
using ActivePositions = System.Collections.Generic.List<bool>;
// using Positions = Godot.Collections.Array<Godot.Vector2I>;

public partial class Room : TileMapLayer
{
	private static RoomArray roomArray = new RoomArray()
	{
		GD.Load<PackedScene>( "res://Prefabs/room-single.tscn" )
	};
	private static Dungeon dungeon = new Dungeon();
	private Vector2I dungeonIndex = new Vector2I();

	[Export] private bool firstRoom = false;
	[Export] private ResourceRoomExit positions;
	
	public ActivePositions topExits    { get; private set; } = new ActivePositions();
	public ActivePositions leftExits   { get; private set; } = new ActivePositions();
	public ActivePositions bottomExits { get; private set; } = new ActivePositions();
	public ActivePositions rightExits  { get; private set; } = new ActivePositions();
	
	public override void _Ready()
	{
		// Called when the node enters the scene tree for the first time.
		for ( int i = 0; i < positions.top.Count; i++ ) topExits.Add( false );
		for ( int i = 0; i < positions.left.Count; i++ ) leftExits.Add( false );
		for ( int i = 0; i < positions.bottom.Count; i++ ) bottomExits.Add( false );
		for ( int i = 0; i < positions.right.Count; i++ ) rightExits.Add( false );
		if ( firstRoom ) CallDeferred( "FirstRoomSetup" );
	}

	public void PlaceExitTile( Vector2I exitCoord )
	{
		SetCell( exitCoord, 0, new Vector2I( 1, 0 ) );
	}
	
	public void PlaceExits()
	{
		RandomNumberGenerator generator = new RandomNumberGenerator();
		for ( int i = 0; i < positions.top.Count; i++ )
		{
			dungeonIndex.Y--;
			if ( dungeon.ContainsKey( dungeonIndex ) && dungeon[dungeonIndex] ) { dungeonIndex.Y++; continue; }
			topExits[i] = generator.RandiRange(0,1) == 1;
			dungeon[dungeonIndex] = topExits[i];
			dungeonIndex.Y++;
			if ( topExits[i] )
			{
				PlaceExitTile( positions.top[i] );
				PlaceExitTile( new Vector2I( positions.top[i].X, positions.top[i].Y-1 ) );
			}
		}

		for ( int i = 0; i < positions.left.Count; i++ )
		{
			dungeonIndex.X--;
			if ( dungeon.ContainsKey( dungeonIndex ) && dungeon[dungeonIndex] ) { dungeonIndex.X++; continue; }
			leftExits[i] = generator.RandiRange(0,1) == 1;
			dungeon[dungeonIndex] = leftExits[i];
			dungeonIndex.X++;
			if ( leftExits[i] )
			{
				PlaceExitTile( positions.left[i] );
				PlaceExitTile( new Vector2I( positions.left[i].X-1, positions.left[i].Y ) );
			}
		}

		for ( int i = 0; i < positions.bottom.Count; i++ )
		{
			dungeonIndex.Y++;
			if ( dungeon.ContainsKey( dungeonIndex ) && dungeon[dungeonIndex] ) { dungeonIndex.Y--; continue; }
			bottomExits[i] = generator.RandiRange(0,1) == 1;
			dungeon[dungeonIndex] = bottomExits[i];
			dungeonIndex.Y--;
			if ( bottomExits[i] )
			{
				PlaceExitTile( positions.bottom[i] );
				PlaceExitTile( new Vector2I( positions.bottom[i].X, positions.bottom[i].Y+1 ) );
			}
		}

		for ( int i = 0; i < positions.right.Count; i++ )
		{
			dungeonIndex.X++;
			if ( dungeon.ContainsKey( dungeonIndex ) && dungeon[dungeonIndex] ) { dungeonIndex.X--; continue; }
			rightExits[i] = generator.RandiRange(0,1) == 1;
			dungeon[dungeonIndex] = rightExits[i];
			dungeonIndex.X--;
			if ( rightExits[i] )
			{
				PlaceExitTile( positions.right[i] );
				PlaceExitTile( new Vector2I( positions.right[i].X+1, positions.right[i].Y ) );
			}
		}
	}

	public Room GenerateRoom()
	{
		Room room = roomArray[0].Instantiate<Room>();
		GetParent().AddChild( room );
		return room;
	}

	public void GenerateRooms( int distance )
	{
		for ( int i = 0; i < positions.top.Count; i++ )
		{
			if ( topExits[i] == false ) continue;
			Room room = GenerateRoom();
			room.dungeonIndex = dungeonIndex;
			room.dungeonIndex.Y -= 1;
			Vector2 position = positions.top[i];
			position.X += 1;
			position.Y -= 10;
			room.SetPosition( new Vector2(
				Position.X + 32 * ( position.X - position.Y ),
				Position.Y + 16 * ( position.X + position.Y )
			) );
			if ( distance < 5 )
			{
				room.PlaceExits();
				room.GenerateRooms( distance+1 );
			}
			room.bottomExits[0] = true;
			room.PlaceExitTile( positions.bottom[0] );
			room.PlaceExitTile( new Vector2I( positions.bottom[0].X, positions.bottom[0].Y+1 ) );
		}
		
		for ( int i = 0; i < positions.left.Count; i++ )
		{
			if ( leftExits[i] == false ) continue;
			Room room = GenerateRoom();
			room.dungeonIndex = dungeonIndex;
			room.dungeonIndex.X -= 1;
			Vector2 position = positions.left[i];
			position.X += 1-10;
			room.SetPosition( new Vector2(
				Position.X + 32 * ( position.X - position.Y ),
				Position.Y + 16 * ( position.X + position.Y )
			) );
			if ( distance < 5 )
			{
				room.PlaceExits();
				room.GenerateRooms( distance+1 );
			}
			room.rightExits[0] = true;
			room.PlaceExitTile( positions.right[0] );
			room.PlaceExitTile( new Vector2I( positions.right[0].X+1, positions.right[0].Y ) );
		}
		
		for ( int i = 0; i < positions.bottom.Count; i++ )
		{
			if ( bottomExits[i] == false ) continue;
			Room room = GenerateRoom();
			room.dungeonIndex = dungeonIndex;
			room.dungeonIndex.Y += 1;
			Vector2 position = positions.bottom[i];
			position.X += 1;
			position.Y += 10;
			room.SetPosition( new Vector2(
				Position.X + 32 * ( position.X - position.Y ),
				Position.Y + 16 * ( position.X + position.Y )
			) );
			if ( distance < 5 )
			{
				room.PlaceExits();
				room.GenerateRooms( distance+1 );
			}
			room.topExits[0] = true;
			room.PlaceExitTile( positions.top[0] );
			room.PlaceExitTile( new Vector2I( positions.top[0].X, positions.top[0].Y-1 ) );
		}
		
		for ( int i = 0; i < positions.right.Count; i++ )
		{
			if ( rightExits[i] == false ) continue;
			Room room = GenerateRoom();
			room.dungeonIndex = dungeonIndex;
			room.dungeonIndex.X += 1;
			Vector2 position = positions.right[i];
			position.X += 1+10;
			room.SetPosition( new Vector2(
				Position.X + 32 * ( position.X - position.Y ),
				Position.Y + 16 * ( position.X + position.Y )
			) );
			if ( distance < 5 )
			{
				room.PlaceExits();
				room.GenerateRooms( distance+1 );
			}
			room.leftExits[0] = true;
			room.PlaceExitTile( positions.left[0] );
			room.PlaceExitTile( new Vector2I( positions.left[0].X-1, positions.left[0].Y ) );
		}
	}

	public void FirstRoomSetup()
	{
		dungeon[dungeonIndex] = true;
		PlaceExits();
		GenerateRooms( 0 );
	}

	public override void _Process(double delta)
	{
		// Called every frame. 'delta' is the elapsed time since the previous frame.
	}
}
