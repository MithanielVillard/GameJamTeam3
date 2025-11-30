using GameJamTeam3.Scripts.Enums;
using Godot;
namespace GameJamTeam3.Scripts;

public partial class EnchantmentStatsEffect : Resource
{
    [Export] public StatsOperator Operator { get; set; }
    [Export] public int StatValue { get; set; }
    [Export] public StatsOperationUnit OperationUnit { get; set; }
}
