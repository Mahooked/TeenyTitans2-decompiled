using UnityEngine;

public class DayNightTintAEControllerTk2d : MonoBehaviour
{
	[SerializeField]
	private AEControllerTk2d controller;

	private void OnEnable()
	{
		if (controller != null && DayNightCard.instance != null)
		{
			DayNightCard.instance.AddAnimationControllerToTint(controller);
		}
	}

	private void OnDisable()
	{
		if (controller != null && DayNightCard.instance != null)
		{
			DayNightCard.instance.RemoveAnimationControllerToTint(controller);
		}
	}

	private void Start()
	{
		if (controller != null && DayNightCard.instance != null)
		{
			DayNightCard.instance.AddAnimationControllerToTint(controller);
		}
	}
}
