using Godot;
using System;

[GlobalClass]
public partial class ResourceRoom : Resource
{
    [Export] public PackedScene prefab;
    [Export] public int weight;
    [Export] public Vector2I topPosition;
    [Export] public Vector2I leftPosition;
    [Export] public Vector2I bottomPosition;
    [Export] public Vector2I rightPosition;
}