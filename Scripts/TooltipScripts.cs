using System.Collections.Generic;
using System.Text;
using Godot;

namespace GameJamTeam3.Scripts;

public partial class TooltipScripts : PanelContainer
{
	private string _title;
	[Export] public string Title
	{
		get => _title;
		set
		{
			UpdateToolTip();
			_title = value;
		}
	}
	
	private string _description;
	[Export(PropertyHint.MultilineText)] public string Description
	{
		get => _description;
		set
		{
			UpdateToolTip();
			_description = value;
		}
	}

	[Export] public RichTextLabel TextLabelRef;

	private bool _hasChild = false;
	private bool _mouseInside = false;
	private TooltipScripts _parentTooltip;
	private static List<TooltipScripts> s_allToolTips = new();

	private static Node _currentScene = null;
	
	public override void _Ready()
	{ 
		_currentScene ??= GetTree().GetCurrentScene();
		s_allToolTips.Add(this);
		UpdateToolTip();

		PivotOffset = GetGlobalRect().GetCenter();
		
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "scale", Vector2.One, 0.2f).SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.InOut);
	}
	
	public override void _Process(double delta)
	{
		_mouseInside = GetGlobalRect().HasPoint(GetGlobalMousePosition());
	}

	private void UpdateToolTip()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("[center]" + _title + "[/center]\n");
		sb.Append("[hr height=1 width=95/]\n");
		sb.Append("[font_size=5]"+_description+"[/font_size]");
		TextLabelRef.Text = sb.ToString();
	}

	private void _on_rich_text_label_meta_hover_started(Variant meta)
	{
		if (_hasChild) return;
		var node = OpenToolTip("res://Ressources/Tooltips/" + meta.AsString());
		
		_hasChild = true;
		node._parentTooltip = this;
		ComputeNewToolTipPosition(node);
	}

	public static TooltipScripts OpenToolTip(string path)
	{
		Tooltip newTip = GD.Load<Tooltip>(path);
		TooltipScripts node = newTip.CreateInstance();
		_currentScene.AddChild(node);
		return node;
	}

	private void OnHoverLeave()
	{
		if (!_hasChild)
		{
			if (_parentTooltip != null)
				_parentTooltip._hasChild = false;
			
			Tween tween = GetTree().CreateTween();
			tween.TweenProperty(this, "scale", Vector2.Zero, 0.2f).SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.InOut);
			tween.TweenCallback(Callable.From(QueueFree));
			s_allToolTips.Remove(this);
		}
		
		foreach (var tooltip in s_allToolTips)
			if (!tooltip.IsQueuedForDeletion() && tooltip._mouseInside) return;

		foreach (var toolTip in s_allToolTips)
		{
			Tween tween = GetTree().CreateTween();
			tween.TweenProperty(toolTip, "scale", Vector2.Zero, 0.2f).SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.InOut);
			tween.TweenCallback(Callable.From(QueueFree));
		}
	}

	private void ComputeNewToolTipPosition(TooltipScripts newToolTip)
	{
		//Always try to spawn right first
		newToolTip.GlobalPosition = GetViewport().GetMousePosition() - new Vector2(3, 3);
	}
}