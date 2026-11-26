using UnityEngine;

public static class AnimatorExtensions
{
	public static bool IsPlaying(this Animator animator)
	{
		return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
	}

	public static float NormalizedTime(this Animator animator)
	{
		return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	public static bool IsName(this Animator animator, string name)
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
	}

	public static bool IsNamed(this Animator animator, params string[] names)
	{
		for (int i = 0; i < names.Length; i++)
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName(names[i]))
			{
				return true;
			}
		}
		return false;
	}

	public static bool HasState(this Animator animator, int layerIndex, string stateName)
	{
		return animator.HasState(layerIndex, Animator.StringToHash("Base Layer." + stateName));
	}
}
