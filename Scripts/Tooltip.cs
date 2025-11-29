using Godot;

namespace GameJamTeam3.Scripts;

[GlobalClass]
public partial class Tooltip : Resource
{
    [Export] public string Title { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    public TooltipScripts TooltipScene;

    public Tooltip()
    {
        TooltipScene = GD.Load<TooltipScripts>("res://Prefabs/Tooltip.tscn");
        TooltipScene.Title = Title;
        TooltipScene.Description = Description;
    }
}