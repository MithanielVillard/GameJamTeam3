using GameJamTeam3.Scripts.Enums;
using Godot;

namespace GameJamTeam3.Scripts;

[GlobalClass]
public partial class Enchantment : Resource
{
    [Export] public string Name { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    [Export] public EnchantmentCategories Categories { get; set; }
    [Export] public EnchantmentPlayerStatsEffect[] EnchantmentPlayerStatEffects { get; set; }
    [Export] public EnchantmentWandStatsEffect[] EnchantmentWandStatEffects { get; set; }
    [Export] public IEnchantmentEffect EnchantmentEffectScript { get; set; }
}