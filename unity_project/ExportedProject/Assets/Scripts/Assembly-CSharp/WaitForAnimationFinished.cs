using UnityEngine;

public class WaitForAnimationFinished : CustomYieldInstruction
{
	private Animator animator;

	private string animationName;

	public override bool keepWaiting
	{
		get
		{
			return animator.IsName(animationName) && animator.IsPlaying();
		}
	}

	public WaitForAnimationFinished(Animator animator, string animationName)
	{
		this.animator = animator;
		this.animationName = animationName;
	}
}
