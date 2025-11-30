using GameJamTeam3.Scripts;
using GameJamTeam3.Scripts.Enums;
using Godot;
using System;

public partial class EnchantmentBlackMage : IEnchantmentEffect
{
    private Player _player;
    public override void OnEffectAdded(Player player)
    {
        _player = player;
        player.StatChanged -= DoEffect;
    }



    private void DoEffect(StatsPlayer healthChange)
    {
            _player.SetPlayerStats(StatsPlayer.LUCK, GameJamTeam3.Scripts.Enums.StatsOperator.PLUS, GameJamTeam3.Scripts.Enums.StatsOperationUnit.INT, 1);
    }
}