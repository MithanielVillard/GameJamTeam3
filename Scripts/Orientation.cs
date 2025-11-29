using Godot;
using System;
using System.Diagnostics;

public partial class Orientation : Node2D
{

	private Sprite2D arrow;

	public override void _Ready()
	{
		arrow = this.GetChild<Sprite2D>(0);
	}
	
	public override void _Process(double delta)
	{
		arrow.LookAt(GetGlobalMousePosition());
	}

}
