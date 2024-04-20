using Godot;

namespace Entitas.Godot;

public partial class ContextDebugView : Control
{
	[Export] private Label NameLabel       { get; set; }
	[Export] private Pool  EntityViewsPool { get; set; }

	private IContext _context;

	public void Construct(IContext context)
	{
		_context = context;

		NameLabel.Name = _context.contextInfo.name;

		_context.OnEntityCreated += OnEntityCreated;
		_context.OnGroupCreated += OnGroupCreated;
	}

	public void Reset()
	{
		EntityViewsPool.Clear<EntityListEntryDebugView>((v) => v.Reset());

		_context.OnEntityCreated -= OnEntityCreated;
		_context.OnGroupCreated -= OnGroupCreated;
	}

	private void OnEntityCreated(IContext context, IEntity entity)
	{
		EntityViewsPool.Get<EntityListEntryDebugView>((v) => v.Construct(entity, EntityViewsPool));
		// AddChild(entityView, @internal: InternalMode.Back);
	}

	private void OnGroupCreated(IContext context, IGroup group)
	{
		// TODO
	}
}