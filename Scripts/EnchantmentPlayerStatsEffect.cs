using GameJamTeam3.Scripts.Enums;
using Godot;
namespace GameJamTeam3.Scripts;


[GlobalClass]
public partial class EnchantmentPlayerStatsEffect : EnchantmentStatsEffect
{
    [Export] public StatsPlayer Stat { get; set; }
}