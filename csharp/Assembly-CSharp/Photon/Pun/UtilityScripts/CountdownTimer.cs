using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.UtilityScripts;

public class CountdownTimer : MonoBehaviourPunCallbacks
{
	public delegate void CountdownTimerHasExpired();

	public const string CountdownStartTime = "StartTime";

	private bool isTimerRunning;

	private float startTime;

	[Header("Reference to a Text component for visualizing the countdown")]
	public Text Text;

	[Header("Countdown time in seconds")]
	public float Countdown = 5f;

	public static event CountdownTimerHasExpired OnCountdownTimerHasExpired;

	public void Start()
	{
		if (Text == null)
		{
			Debug.LogError("Reference to 'Text' is not set. Please set a valid reference.", this);
		}
	}

	public void Update()
	{
		if (!isTimerRunning)
		{
			return;
		}
		float num = (float)PhotonNetwork.Time - startTime;
		float num2 = Countdown - num;
		Text.text = string.Format("Game starts in {0} seconds", num2.ToString("n2"));
		if (!(num2 > 0f))
		{
			isTimerRunning = false;
			Text.text = string.Empty;
			if (OnCountdownTimerHasExpired != null)
			{
				OnCountdownTimerHasExpired();
			}
		}
	}

	public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.TryGetValue("StartTime", out var value))
		{
			isTimerRunning = true;
			startTime = (float)value;
		}
	}
}
