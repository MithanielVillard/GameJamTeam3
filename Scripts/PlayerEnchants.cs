using System.Collections.Generic;
using Godot;

namespace GameJamTeam3.Scripts;

public partial class PlayerEnchants : Node2D
{
    public List<Enchantment> EnchantmentsHistory { get; set; }
    
    private Player _player;
    
    public override void _Ready()
    {
        EnchantmentsHistory = new List<Enchantment>();
        _player = GetParent<Player>();
    }

    public void AddEnchantments(Enchantment enchantment)
    {
        foreach (var statEffect in enchantment.EnchantmentPlayerStatEffects)
            _player.SetPlayerStats(statEffect.Stat, statEffect.Operator, statEffect.OperationUnit, statEffect.StatValue);

        foreach (var statEffect in enchantment.EnchantmentWandStatEffects)
            _player.PlayerWand.SetWandStats(statEffect.Stat, statEffect.Operator, statEffect.OperationUnit, statEffect.StatValue);

         enchantment.EnchantmentEffectScript?.OnEffectAdded(_player);
        EnchantmentsHistory.Add(enchantment);
    }
}