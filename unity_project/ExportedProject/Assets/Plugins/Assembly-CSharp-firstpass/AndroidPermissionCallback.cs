using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class AndroidPermissionCallback : AndroidJavaProxy
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> OnPermissionGrantedAction__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> OnPermissionDeniedAction__BackingField;

	private event Action<string> OnPermissionGrantedAction
	{
		add
		{
			Action<string> action = OnPermissionGrantedAction__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPermissionGrantedAction__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = OnPermissionGrantedAction__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPermissionGrantedAction__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private event Action<string> OnPermissionDeniedAction
	{
		add
		{
			Action<string> action = OnPermissionDeniedAction__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPermissionDeniedAction__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = OnPermissionDeniedAction__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPermissionDeniedAction__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public AndroidPermissionCallback(Action<string> onGrantedCallback, Action<string> onDeniedCallback)
		: base("com.unity3d.plugin.UnityAndroidPermissions$IPermissionRequestResult")
	{
		if (onGrantedCallback != null)
		{
			OnPermissionGrantedAction += onGrantedCallback;
		}
		if (onDeniedCallback != null)
		{
			OnPermissionDeniedAction += onDeniedCallback;
		}
	}

	public virtual void OnPermissionGranted(string permissionName)
	{
		if (OnPermissionGrantedAction__BackingField != null)
		{
			OnPermissionGrantedAction__BackingField(permissionName);
		}
	}

	public virtual void OnPermissionDenied(string permissionName)
	{
		if (OnPermissionDeniedAction__BackingField != null)
		{
			OnPermissionDeniedAction__BackingField(permissionName);
		}
	}
}
