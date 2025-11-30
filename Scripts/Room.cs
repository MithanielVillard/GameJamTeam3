using Godot;
using System;
// using Positions = Godot.Collections.Array<Godot.Vector2I>;

public partial class Room : TileMapLayer
{
	[Export] private bool firstRoom = false;
	public Vector2I dungeonIndex;
	
	public override void _Ready()
	{
		// Called when the node enters the scene tree for the first time.
		if ( firstRoom ) CallDeferred( "FirstRoomSetup" );
		else SetModulate( new Color( Modulate.R, Modulate.G, Modulate.B, 0 ) );
	}

	public void PlaceExitTile( Vector2I position, Vector2I direction, int distance )
	{
		position.X -= 1;
		Vector2I altasCoord = GetCellAtlasCoords( position );
		for ( int i = 0; i < distance; i++ )
		{
			position += direction;
			SetCell( position, 0, altasCoord );
		}
	}

	public void FadeIn()
	{
		Tween tween = CreateTween();
		tween.TweenProperty( this, "modulate:a", 1, 0.5 );
	}

	public void FadeOut()
	{
		Tween tween = CreateTween();
		tween.TweenProperty( this, "modulate:a", 0, 0.5 );
	}

	public void FirstRoomSetup()
	{
		Dungeon.Node.Rooms[dungeonIndex] = true;
		
		// Create Room Spawner
		// TODO Random Multiple
		
		RoomSpawner roomSpawner = new RoomSpawner();
		AddChild( roomSpawner );
		roomSpawner.existing = this;
		roomSpawner.dungeonIndex = dungeonIndex;
		roomSpawner.dungeonIndex.Y -= 1;
		roomSpawner.distance = 1;
		Dungeon.Node.RoomSpawners.Add( roomSpawner );
		
		Dungeon.Node.GenerateDungeon();
	}
}
