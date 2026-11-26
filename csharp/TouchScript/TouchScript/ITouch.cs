using System.Collections.Generic;
using TouchScript.Hit;
using TouchScript.Layers;
using UnityEngine;

namespace TouchScript;

public interface ITouch
{
	int Id { get; }

	Transform Target { get; }

	Vector2 Position { get; }

	Vector2 PreviousPosition { get; }

	ITouchHit Hit { get; }

	TouchLayer Layer { get; }

	Tags Tags { get; }

	IDictionary<string, object> Properties { get; }
}
