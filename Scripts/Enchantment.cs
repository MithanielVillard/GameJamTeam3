using GameJamTeam3.Scripts.Enums;
using Godot;

namespace GameJamTeam3.Scripts;

[GlobalClass]
public partial class Enchantment : Resource
{
    [Export] public string Name { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    [Export] public EnchantmentCategories Categories { get; set; }
    [Export] public EnchantmentStatsEffect[] CardStatEffects { get; set; }
    [Export] public IEnchantmentEffect EnchantmentEffectScript { get; set; }
}