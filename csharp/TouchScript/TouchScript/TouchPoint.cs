using System.Collections.Generic;
using TouchScript.Hit;
using TouchScript.Layers;
using UnityEngine;

namespace TouchScript;

internal sealed class TouchPoint : ITouch
{
	private Vector2 position = Vector2.zero;

	private Vector2 newPosition = Vector2.zero;

	private ITouchHit hit;

	private bool isDirty;

	private Dictionary<string, object> properties;

	public int Id { get; private set; }

	public Transform Target { get; internal set; }

	public Vector2 Position => position;

	public Vector2 PreviousPosition { get; private set; }

	public ITouchHit Hit
	{
		get
		{
			if (isDirty)
			{
				TouchManager.Instance.GetHitTarget(position, out hit);
				isDirty = false;
			}
			return hit;
		}
		internal set
		{
			hit = value;
			isDirty = false;
		}
	}

	public TouchLayer Layer { get; internal set; }

	public Tags Tags { get; private set; }

	public IDictionary<string, object> Properties => properties;

	internal TouchPoint(int id, Vector2 position, Tags tags)
	{
		Id = id;
		this.position = (PreviousPosition = (newPosition = position));
		Tags = tags ?? new Tags();
		properties = new Dictionary<string, object>();
	}

	internal void ResetPosition()
	{
		PreviousPosition = position;
		position = newPosition;
		newPosition = position;
		if (PreviousPosition != position)
		{
			isDirty = true;
		}
	}

	internal void SetPosition(Vector2 value)
	{
		newPosition = value;
	}
}
