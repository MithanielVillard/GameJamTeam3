using Godot;
using System.Text;

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
	
	public override void _Ready()
	{
		UpdateToolTip();
	}

	private void UpdateToolTip()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(_title);
		//sb.Append("[font_size=5]"+_description+"[/font_size]");
		TextLabelRef.Text = sb.ToString();
	}
}
