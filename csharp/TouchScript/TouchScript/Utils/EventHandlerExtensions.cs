using System;
using UnityEngine;

namespace TouchScript.Utils;

internal static class EventHandlerExtensions
{
	public static Exception InvokeHandleExceptions<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
	{
		if (handler != null)
		{
			try
			{
				handler(sender, args);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				return ex;
			}
		}
		return null;
	}

	public static Exception InvokeHandleExceptions(this EventHandler handler, object sender, EventArgs args)
	{
		if (handler != null)
		{
			try
			{
				handler(sender, args);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				return ex;
			}
		}
		return null;
	}
}
