using UnityEngine;

namespace TouchScript.InputSources;

internal struct TouchState(int anId, TouchPhase aPhase, Vector2 aPosition)
{
	public int Id = anId;

	public TouchPhase Phase = aPhase;

	public Vector2 Position = aPosition;
}
