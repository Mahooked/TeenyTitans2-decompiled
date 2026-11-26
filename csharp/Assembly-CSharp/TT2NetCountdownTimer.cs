using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class TT2NetCountdownTimer : MonoBehaviourPunCallbacks
{
	public const string CountdownStartTime = "PreBattleStartTime";

	public float countdownTime = 30f;

	private float startTime;

	public static TT2NetCountdownTimer Instance { get; private set; }

	public bool isTimerRunning { get; private set; }

	public float countdown { get; private set; }

	public static event Action CountdownTimerStopped;

	public static event Action CountdownTimerExpired;

	private void Awake()
	{
		Instance = this;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
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
			if (CountdownTimerExpired != null)
			{
				CountdownTimerExpired();
			}
		}
	}

	public void ForceStop()
	{
		if (isTimerRunning)
		{
			isTimerRunning = false;
			if (CountdownTimerStopped != null)
			{
				CountdownTimerStopped();
			}
		}
	}

	public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.TryGetValue("PreBattleStartTime", out var value))
		{
			isTimerRunning = true;
			startTime = (float)value;
		}
	}
}
