using UnityEngine;

public class TapWaypointIndicator : MonoBehaviour
{
	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	private CityCharacter cityCharacter;

	private PathNode targetNode;

	private bool exiting;

	public void Init(CityCharacter newCityCharacter, PathNode newTargetNode)
	{
		cityCharacter = newCityCharacter;
		targetNode = newTargetNode;
		exiting = false;
		base.transform.position = targetNode.transform.position;
		if (CityController.instance != null)
		{
			CityController.instance.viewDeobstructer.AddTarget(base.transform);
		}
	}

	private void LateUpdate()
	{
		if (!(cityCharacter == null) && (!cityCharacter.IsNodeOnPath(targetNode) || (Vector3.Distance(cityCharacter.transform.position, base.transform.position) < 0.4f && cityCharacter.CurrentNode == targetNode)) && !exiting)
		{
			exiting = true;
			animator.CrossFade("tap_circle_outro", 0.1f);
			animatorEventListener.EventTriggered -= HandleAnimationEvent;
			animatorEventListener.EventTriggered += HandleAnimationEvent;
		}
	}

	private void HandleAnimationEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "outroComplete")
		{
			animatorEventListener.EventTriggered -= HandleAnimationEvent;
			if (CityController.instance != null)
			{
				CityController.instance.viewDeobstructer.RemoveTarget(base.transform);
			}
			ObjectPool.PoolObject(base.gameObject);
		}
	}
}
