using Godot;
using System;

public partial class Card : Node3D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	void _on_area_3d_input_event(Variant camera, InputEvent  @event, Vector3 position, Vector3 normal, long shape_idx)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			GD.Print("Mouse Motion at: ", eventMouseMotion.Position);
			float rotationY = eventMouseMotion.Position.Y - Position.Y + 180.0f;
			float rotationX = eventMouseMotion.Position.X - (Position.X - 40);
			rotationX *= Single.Pi / 180.0f;
			rotationY *= Single.Pi / 180.0f;
			GlobalRotation = new Vector3(rotationY, rotationX, 0.0f);
		}
	}
}
