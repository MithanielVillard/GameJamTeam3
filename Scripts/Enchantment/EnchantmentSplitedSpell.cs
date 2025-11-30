using GameJamTeam3.Scripts;
using GameJamTeam3.Scripts.Enums;
using Godot;
using System;

public partial class EnchantmentSplitedSpell : IEnchantmentEffect
{
    private Player _player;
    private int _damageTemp;
    public override void OnEffectAdded(Player player)
    {
        _damageTemp = (int)StatsWand.ATTACK_DAMAGE;
        _player.SetPlayerStats(StatsPlayer.DEFENSE, GameJamTeam3.Scripts.Enums.StatsOperator.PLUS, GameJamTeam3.Scripts.Enums.StatsOperationUnit.INT, _damageTemp/4);
    }
}
