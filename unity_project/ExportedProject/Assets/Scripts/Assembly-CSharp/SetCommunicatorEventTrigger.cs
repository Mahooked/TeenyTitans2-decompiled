using UnityEngine;

public class SetCommunicatorEventTrigger : MonoBehaviour
{
	[SerializeField]
	private string _listenForEventName;

	private void Start()
	{
		CommunicatorManager.instance.AnimationEvent += HandleAnimationEvent;
	}

	private void OnDestroy()
	{
		if (CommunicatorManager.instance != null)
		{
			CommunicatorManager.instance.AnimationEvent -= HandleAnimationEvent;
		}
	}

	private void HandleAnimationEvent(string eventName)
	{
		if (eventName.Equals(_listenForEventName))
		{
			Player.instance.SetTrigger(eventName);
		}
	}
}
