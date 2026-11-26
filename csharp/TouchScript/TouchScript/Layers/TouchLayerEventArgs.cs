using System;

namespace TouchScript.Layers;

public class TouchLayerEventArgs : EventArgs
{
	public ITouch Touch { get; private set; }

	public TouchLayerEventArgs(ITouch touch)
	{
		Touch = touch;
	}
}
