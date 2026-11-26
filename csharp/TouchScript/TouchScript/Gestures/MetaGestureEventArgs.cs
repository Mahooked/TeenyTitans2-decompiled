using System;

namespace TouchScript.Gestures;

public class MetaGestureEventArgs : EventArgs
{
	public ITouch Touch { get; private set; }

	public MetaGestureEventArgs(ITouch touch)
	{
		Touch = touch;
	}
}
