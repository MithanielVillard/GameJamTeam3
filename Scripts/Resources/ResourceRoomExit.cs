using Godot;
using System;

using Positions = Godot.Collections.Array<Godot.Vector2I>;

[GlobalClass]
public partial class ResourceRoomExit : Resource
{
    [Export] public Positions top    { get; private set; }
    [Export] public Positions left   { get; private set; }
    [Export] public Positions bottom { get; private set; }
    [Export] public Positions right  { get; private set; }
}
