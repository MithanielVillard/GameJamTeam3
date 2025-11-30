using GameJamTeam3.Scripts.Enums;
using Godot;
namespace GameJamTeam3.Scripts;

[GlobalClass]
public partial class EnchantmentWandStatsEffect : EnchantmentStatsEffect
{
    [Export] public StatsWand Stat { get; set; }
}