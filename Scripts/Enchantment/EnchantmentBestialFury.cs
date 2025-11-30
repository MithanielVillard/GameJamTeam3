using GameJamTeam3.Scripts;
using Godot;
using System;

public partial class EnchantmentBestialFury : IEnchantmentEffect
{
    Player _player;

    private float _elapsedTime = 0;
    private double _lastTime;

    public override void OnEffectAdded(Player player)
    {
        _lastTime = Time.GetTicksMsec();
        _player = player;

        _player.FireProjectile += DoEffect;

    }

    public void DoEffect()
    {
        double newTime = Time.GetTicksMsec();
        int i = 0;
        while (newTime - _lastTime > 1.0f / _player._attackSpeed)
        {
            _player.PlayerWand.SetWandStats(GameJamTeam3.Scripts.Enums.StatsWand.ATTACK_DAMAGE, GameJamTeam3.Scripts.Enums.StatsOperator.PLUS, GameJamTeam3.Scripts.Enums.StatsOperationUnit.INT, 2 * i);
        }
    }
}
