using UnityEngine;

namespace TouchScript.Devices.Display;

public class DisplayDevice : ScriptableObject, IDisplayDevice
{
	[SerializeField]
	protected new string name = "Unknown Device";

	[SerializeField]
	protected float dpi = 96f;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
			base.name = value;
		}
	}

	public virtual float DPI
	{
		get
		{
			return dpi;
		}
		set
		{
			dpi = value;
		}
	}

	protected virtual void OnEnable()
	{
		base.name = name;
	}
}
