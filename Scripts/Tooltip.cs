using Godot;

namespace GameJamTeam3.Scripts;

[GlobalClass]
public partial class Tooltip : Resource
{
    [Export] public string Title { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    
    private PackedScene _tooltipScene;

    public Tooltip()
    {
        _tooltipScene = GD.Load<PackedScene>("res://Prefabs/Tooltip.tscn");
    }

    public TooltipScripts CreateInstance()
    {
        TooltipScripts tooltip = _tooltipScene.Instantiate<TooltipScripts>();
        tooltip.Title = Title;
        tooltip.Description = Description;
        return tooltip;
    }
}