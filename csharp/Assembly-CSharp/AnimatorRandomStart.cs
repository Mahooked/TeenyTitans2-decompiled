using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorRandomStart : MonoBehaviour
{
	public Animator animator;

	public string stateName;

	private void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
		if (animator == null)
		{
			Object.Destroy(this);
		}
	}

	private void Reset()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		animator.Play(stateName, 0, Random.value);
	}
}
