using System;
using System.Collections.Generic;

namespace TouchScript;

public class TouchEventArgs : EventArgs
{
	public IList<ITouch> Touches { get; private set; }

	public TouchEventArgs(IList<ITouch> touches)
	{
		Touches = touches;
	}
}
