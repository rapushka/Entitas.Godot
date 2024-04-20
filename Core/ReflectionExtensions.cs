using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas.Godot;

internal static class ReflectionExtensions
{
	internal static IEnumerable<T> GetInstancesOf<T>(this AppDomain @this)
		=> @this.GetAssemblies()
		        .SelectMany((a) => a.GetTypes())
		        .Where(t => typeof(T).IsAssignableFrom(t))
		        .Select(t => (T)Activator.CreateInstance(t));
}