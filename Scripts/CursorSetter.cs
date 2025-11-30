using Godot;

namespace GameJamTeam3.Scripts;

public partial class CursorSetter : Node
{
	public override void _Ready()
	{
		var cursor = ResourceLoader.Load("res://Assets/Sprites/cursor.png");
		Input.SetCustomMouseCursor(cursor);
		Input.SetCustomMouseCursor(cursor, Input.CursorShape.PointingHand);
	}
}