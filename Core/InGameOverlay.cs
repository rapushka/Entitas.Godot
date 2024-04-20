using Godot;

namespace Entitas.Godot;

[GlobalClass]
public partial class InGameOverlay : Control
{
	[Export] private bool           AutoInitialize { get; set; }
	[Export] private ContextsDrawer ContextsDrawer { get; set; }
	// TODO:
	// [Export] private EntityEditor   EntityEditor   { get; set; }

	public override void _Ready()
	{
		Visible = false;

		if (AutoInitialize)
			Initialize();
	}

	public override void _ExitTree()
	{
		ContextsDrawer.Reset();
	}

	public void Initialize()
	{
		ContextsDrawer.Initialize();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("debug_toggle_overlay"))
			ToggleOverlay();

		if (Visible)
			UpdateOverlay();
	}

	private void ToggleOverlay()
	{
		Visible = !Visible;
	}

	private new void Show()
	{
		// EntityDrawer.Enable(ContextsDrawer.SelectedEntity);
		// ContextsDrawer.EntitySelected += EntityEditor.SwitchTo;

		base.Show();
	}

	private new void Hide()
	{
		base.Hide();

		// EntityEditor.Disable();
		// ContextsDrawer.EntitySelected -= EntityEditor.SwitchTo;
	}

	private void UpdateOverlay()
	{
		// ContextsDrawer.UpdateState();
	}
}