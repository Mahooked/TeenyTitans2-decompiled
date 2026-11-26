using UnityEngine;

public class RoboBuddy : Drone
{
	public Animator lightningAnimator;

	public AnimatorEventListener lightningAnimatorEventListener;

	protected override void OnEnable()
	{
		base.OnEnable();
		lightningAnimatorEventListener.EventTriggered += HandleLightningEventTriggered;
		lightningAnimator.gameObject.SetActive(value: false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		lightningAnimatorEventListener.EventTriggered -= HandleLightningEventTriggered;
	}

	public override void PlayAttack()
	{
		base.PlayAttack();
		lightningAnimator.gameObject.SetActive(value: true);
	}

	private void HandleLightningEventTriggered(AnimationEvent evt)
	{
		if (evt.stringParameter == "complete")
		{
			lightningAnimator.gameObject.SetActive(value: false);
		}
	}
}
