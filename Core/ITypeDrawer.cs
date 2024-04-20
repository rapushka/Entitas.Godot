using System;

namespace Entitas.Godot;

public interface ITypeDrawer
{
	bool HandlesType(Type type);

	object Draw(Type memberType, string memberName, object value, object target);
}