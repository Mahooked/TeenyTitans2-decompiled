using UnityEngine;

public class ExceptionLogger : MonoBehaviour
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		Application.logMessageReceived += HandleMessageReceived;
	}

	private void HandleMessageReceived(string condition, string stackTrack, LogType logType)
	{
		Debug.Log("CONDITION: " + condition);
		Debug.Log("STACKTRACE: " + stackTrack);
	}
}
