using System;
using System.Collections.Generic;
using Godot;

namespace Entitas.Godot;

public partial class ContextsDrawer : Control
{
	[Export] private UpdateMode UpdateMode       { get; set; }
	[Export] private Pool       ContextViewsPool { get; set; }

	private static readonly List<IContext> ObservedContexts = [];

	public event Action<Entity> EntitySelected;

	public static void Observe(IContext context)
	{
		ObservedContexts.Add(context);
	}

	public void Initialize()
	{
		foreach (var context in ObservedContexts)
			ContextViewsPool.Get<ContextDebugView>((v) => v.Construct(context));
	}

	public void Reset()
	{
		ContextViewsPool.Clear<ContextDebugView>((v) => v.Reset());
	}

	public override void _Process(double delta)
	{
		if (UpdateMode is UpdateMode.OnProcess)
			UpdateState();
	}

	public void UpdateState() { }
}