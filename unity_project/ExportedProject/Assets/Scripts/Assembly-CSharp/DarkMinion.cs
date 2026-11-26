using System;
using UnityEngine;

public class DarkMinion : MonoBehaviour, IPoolable
{
	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public GameObject projectileObj;

	public Transform projectileSpawnPoint;

	private Action EnemyHit;

	private string attackAnim;

	private string outroAnim;

	private string deathOutroAnim;

	private void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleEventTriggered;
	}

	private void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleEventTriggered;
	}

	public void Init(string attackAnim, string outroAnim, string deathOutroAnim)
	{
		this.attackAnim = attackAnim;
		this.outroAnim = outroAnim;
		this.deathOutroAnim = deathOutroAnim;
	}

	public void PlayAttack(Action enemyHitCallback)
	{
		animator.Play(attackAnim);
		EnemyHit = (Action)Delegate.Combine(EnemyHit, enemyHitCallback);
	}

	public void PlayOutro()
	{
		animator.Play(outroAnim);
	}

	public void PlayDeathOutro()
	{
		animator.Play(deathOutroAnim);
	}

	public WaitForAnimationFinished WaitForAttackAnimationFnished()
	{
		return new WaitForAnimationFinished(animator, attackAnim);
	}

	private void HandleEventTriggered(AnimationEvent evt)
	{
		if (evt.stringParameter.Equals("hit"))
		{
			if (EnemyHit != null)
			{
				EnemyHit();
			}
			EnemyHit = null;
		}
		else if (evt.stringParameter.Equals("outroComplete"))
		{
			ObjectPool.PoolObject(base.gameObject);
		}
	}

	public void ResetObject()
	{
	}
}
