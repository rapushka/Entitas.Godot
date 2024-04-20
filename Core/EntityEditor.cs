using Godot;

namespace Entitas.Godot;

public partial class EntityEditor : Control
{
	[Export] private UpdateMode UpdateMode     { get; set; }
	[Export] private Button     DestroyButton  { get; set; }
	[Export] private Pool       ComponentsPool { get; set; }

	private Entity _entity;

	public void SwitchTo(Entity entity)
	{
		// TODO
		// Disable();
		// Enable(entity);
	}

	public void Enable(Entity entity)
	{
		_entity = entity;
		DrawComponents(entity);
		// TODO: Retained

		DestroyButton.Pressed += OnDestroyButtonPressed;
	}

	public void Disable()
	{
		ClearComponents();
		DestroyButton.Pressed -= OnDestroyButtonPressed;
	}

	private void OnDestroyButtonPressed()
	{
		_entity.Destroy();
	}

	private void DrawComponents(Entity entity)
	{
		// TODO: Add Components
		// TODO: Search

		var indexes = entity.GetComponentIndices();
		var components = entity.GetComponents();
		for (var i = 0; i < components.Length; i++)
			DrawNewComponent(entity, indexes[i], components[i]);
	}

	private void DrawNewComponent(Entity entity, int index, IComponent component)
		=> ComponentsPool.Get<ComponentDrawer>((drawer) => drawer.Construct(entity, index, component));

	private void ClearComponents() => ComponentsPool.Clear<ComponentDrawer>((d) => d.Reset());
}