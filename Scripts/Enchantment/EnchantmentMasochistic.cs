using GameJamTeam3.Scripts;
using GameJamTeam3.Scripts.Enums;
using Godot;
using System;

public partial class EnchantmentMasochistic : IEnchantmentEffect
{
    private Player _player;
    private int _healthTemp;
    public override void OnEffectAdded(Player player)
    {
        _healthTemp = (int)StatsPlayer.HEALTH;
        _player = player;
        player.StatChanged -= DoEffect;
    }



    private void DoEffect(StatsPlayer healthChange)
    {
        if (_healthTemp > (int)StatsPlayer.HEALTH )
            _player.SetPlayerStats(StatsPlayer.DEFENSE, GameJamTeam3.Scripts.Enums.StatsOperator.PLUS, GameJamTeam3.Scripts.Enums.StatsOperationUnit.INT, 1);
    }
}
