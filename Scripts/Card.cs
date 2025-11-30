using Godot;
using GameJamTeam3.Scripts;

public partial class Card : Node3D
{
	public Enchantment Enchantment { get; set; }

	private TooltipScripts _ttScripts;
	private bool _hasTooltip = false;
	public override void _Ready()
	{
		
	}

	void _on_area_3d_input_event(Variant camera, InputEvent  @event, Vector3 position, Vector3 normal, long shape_idx)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			EnchantmentManager.Instance.EmitSignal(EnchantmentManager.SignalName.EnchantmentAquired);
		}
	}

	void _on_area_3d_mouse_entered()
	{
		if (_hasTooltip) return;
		Tween tween = CreateTween();
		tween.TweenProperty(this, "scale", Vector3.One * 1.2f, 0.15f).SetTrans(Tween.TransitionType.Quart);
		_ttScripts = TooltipScripts.OpenToolTip("res://Ressources/Tooltips/TestTooltip.tres");
		_hasTooltip = true;
	}

	void _on_area_3d_mouse_exited()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(this, "scale", Vector3.One, 0.15f).SetTrans(Tween.TransitionType.Quart);
		
		foreach (var tooltip in TooltipScripts.s_allToolTips)
			if (!tooltip.IsQueuedForDeletion() && tooltip._mouseInside) return;

		Tween tween3 = GetTree().CreateTween();
		tween3.TweenProperty(_ttScripts, "scale", Vector2.Zero, 0.2f).SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.InOut);
		tween3.TweenCallback(Callable.From(_ttScripts.QueueFree));
		_hasTooltip = false;
	}
}
