using UnityEngine;

public class AEQueueAnimation : MonoBehaviour
{
	public string queueAnimation;

	private AEControllerTk2d animationController;

	private void Start()
	{
		animationController = GetComponent<AEControllerTk2d>();
		animationController.AddToQueue(queueAnimation);
	}
}
