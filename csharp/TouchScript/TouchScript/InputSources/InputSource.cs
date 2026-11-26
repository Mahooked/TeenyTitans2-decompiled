using System;
using UnityEngine;

namespace TouchScript.InputSources;

public abstract class InputSource : MonoBehaviour, IInputSource
{
	[SerializeField]
	private bool advancedProps;

	private TouchManagerInstance manager;

	public ICoordinatesRemapper CoordinatesRemapper { get; set; }

	protected virtual void OnEnable()
	{
		manager = TouchManagerInstance.Instance;
		if (manager == null)
		{
			throw new InvalidOperationException("TouchManager instance is required!");
		}
	}

	protected virtual void OnDisable()
	{
		manager = null;
	}

	protected virtual void Update()
	{
	}

	protected virtual ITouch beginTouch(Vector2 position)
	{
		return beginTouch(position, null);
	}

	protected virtual ITouch beginTouch(Vector2 position, Tags tags)
	{
		if (CoordinatesRemapper != null)
		{
			position = CoordinatesRemapper.Remap(position);
		}
		return manager.BeginTouch(position, tags);
	}

	protected virtual void updateTouch(int id)
	{
		manager.UpdateTouch(id);
	}

	protected virtual void moveTouch(int id, Vector2 position)
	{
		if (CoordinatesRemapper != null)
		{
			position = CoordinatesRemapper.Remap(position);
		}
		manager.MoveTouch(id, position);
	}

	protected virtual void endTouch(int id)
	{
		manager.EndTouch(id);
	}

	protected virtual void cancelTouch(int id)
	{
		manager.CancelTouch(id);
	}
}
