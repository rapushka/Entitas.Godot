using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Entitas.Godot;

public partial class Pool : Node // TODO: try Pool<T>
{
	[Export] private PackedScene PackedScene { get; set; }
	[Export] private Node        Root        { get; set; }

	private readonly List<Node> _pool = [];

	public TNode Get<TNode>(Action<TNode> prepare = null)
		where TNode : Node
	{
		var node = _pool.OfType<TNode>().FirstOrDefault()
		           ?? PackedScene.Instantiate<TNode>();

		_pool.Remove(node);
		prepare?.Invoke(node);
		Root?.AddChild(node);

		return node;
	}

	public void Clear<TNode>(Action<TNode> reset = null)
		where TNode : Node
	{
		foreach (var child in Root.GetChildren().OfType<TNode>())
			Retain(child, reset);
	}

	public void Retain<TNode>(TNode node, Action<TNode> reset = null)
		where TNode : Node
	{
		reset?.Invoke(node);
		Root?.RemoveChild(node);

		_pool.Add(node);
	}
}