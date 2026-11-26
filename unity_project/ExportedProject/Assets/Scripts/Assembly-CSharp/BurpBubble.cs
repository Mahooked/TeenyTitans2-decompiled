using UnityEngine;

public class BurpBubble : MonoBehaviour, IPoolable
{
	public Animator animator;

	public Transform scaleGroup;

	public void Pop()
	{
		animator.Play("spawn", 0, 0f);
		ObjectPool.PoolObject(base.gameObject);
	}

	public void ResetObject()
	{
		scaleGroup.localScale = Vector3.one;
	}
}
