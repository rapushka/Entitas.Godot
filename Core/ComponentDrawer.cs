using DesperateDevs.Reflection;
using Godot;
using Godot.NativeInterop;

namespace Entitas.Godot;

public partial class ComponentDrawer : Control
{
	private Entity _entity;
	private int _index;
	private IComponent _component;
	[Export] private Label  NameLabel    { get; set; }
	[Export] private Label  ValueLabel   { get; set; }
	[Export] private Button RemoveButton { get; set; }

	public void Construct(Entity entity, int index, IComponent component)
	{
		_index = index;
		_entity = entity;
		_component = component;

		RemoveButton.Pressed += OnRemoveButtonPressed;

		var componentType = _component.GetType();
		var componentName = componentType.Name; // .RemoveSuffix("Component")

		var memberInfos = componentType.GetPublicMemberInfos();

		var newComponent = _entity.CreateComponent(_index, componentType);
		_component.CopyPublicMemberValues(newComponent);
		// Variant.CreateFrom()
		foreach (var info in memberInfos)
		{
			var memberValue = info.GetValue(newComponent);
			ValueLabel.Text = memberValue?.ToString() ?? "null";
		}

		// if (changed)
		// 	entity.ReplaceComponent(index, newComponent);
		// else
		// 	entity.GetComponentPool(index).Push(newComponent);

		NameLabel.Text = componentName;
	}

	public void Reset()
	{
		_entity = null;
		_component = null;

		RemoveButton.Pressed -= OnRemoveButtonPressed;
	}

	private void OnRemoveButtonPressed()
	{
		_entity.RemoveComponent(_index);
	}
}