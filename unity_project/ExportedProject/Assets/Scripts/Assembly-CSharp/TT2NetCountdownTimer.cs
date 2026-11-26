using System;
using System.Diagnostics;
using System.Threading;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class TT2NetCountdownTimer : MonoBehaviourPunCallbacks
{
	public const string CountdownStartTime = "PreBattleStartTime";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action CountdownTimerStopped__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action CountdownTimerExpired__BackingField;

	public float countdownTime = 30f;

	private float startTime;

	public static TT2NetCountdownTimer Instance { get; private set; }

	public bool isTimerRunning { get; private set; }

	public float countdown { get; private set; }

	public static event Action CountdownTimerStopped
	{
		add
		{
			Action action = CountdownTimerStopped__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CountdownTimerStopped__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = CountdownTimerStopped__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CountdownTimerStopped__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action CountdownTimerExpired
	{
		add
		{
			Action action = CountdownTimerExpired__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CountdownTimerExpired__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = CountdownTimerExpired__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CountdownTimerExpired__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		Instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Update()
	{
		if (!isTimerRunning)
		{
			return;
		}
		countdown = countdownTime - ((float)PhotonNetwork.Time - startTime);
		if (countdown <= 0f)
		{
			isTimerRunning = false;
			if (CountdownTimerExpired__BackingField != null)
			{
				CountdownTimerExpired__BackingField();
			}
		}
	}

	public void ForceStop()
	{
		if (isTimerRunning)
		{
			isTimerRunning = false;
			if (CountdownTimerStopped__BackingField != null)
			{
				CountdownTimerStopped__BackingField();
			}
		}
	}

	public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
	{
		object value;
		if (propertiesThatChanged.TryGetValue("PreBattleStartTime", out value))
		{
			isTimerRunning = true;
			startTime = (float)value;
		}
	}
}
