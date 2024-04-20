using Godot;

namespace Entitas.Godot;

[GlobalClass]
public partial class EntityListEntryDebugView : Control
{
	[Export] private Label NameLabel { get; set; }

	private IEntity _entity;
	private Pool _pool;

	public void Construct(IEntity entity, Pool entityViewsPool)
	{
		_pool = entityViewsPool;
		_entity = entity;

		_entity.OnEntityReleased += OnEntityReleased;
		UpdateEntityName();
	}

	public void Reset()
	{
		_entity.OnEntityReleased -= OnEntityReleased;
		_pool.Retain(this);
	}

	private void OnEntityReleased(IEntity entity) => Reset();

	public override void _Process(double delta) => UpdateEntityName();

	private void UpdateEntityName()
		=> NameLabel.Text = _entity.ToString();
}