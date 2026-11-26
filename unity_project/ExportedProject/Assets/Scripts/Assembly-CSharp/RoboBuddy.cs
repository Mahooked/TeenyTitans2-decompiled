using UnityEngine;

public class RoboBuddy : Drone
{
	public Animator lightningAnimator;

	public AnimatorEventListener lightningAnimatorEventListener;

	protected override void OnEnable()
	{
		base.OnEnable();
		lightningAnimatorEventListener.EventTriggered += HandleLightningEventTriggered;
		lightningAnimator.gameObject.SetActive(false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		lightningAnimatorEventListener.EventTriggered -= HandleLightningEventTriggered;
	}

	public override void PlayAttack()
	{
		base.PlayAttack();
		lightningAnimator.gameObject.SetActive(true);
	}

	private void HandleLightningEventTriggered(AnimationEvent evt)
	{
		if (evt.stringParameter == "complete")
		{
			lightningAnimator.gameObject.SetActive(false);
		}
	}
}
