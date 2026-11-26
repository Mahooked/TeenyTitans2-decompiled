using System;
using System.Collections;
using UnityEngine;

public class TunnelEnterCard : MonoBehaviour
{
	public static TunnelEnterCard instance;

	public Animator animator;

	public tk2dBaseSprite card;

	public event Action OnScreenObscured;

	public event Action OnAnimationFinish;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void ShowTunnelCard()
	{
		StartCoroutine(PlayAnimation());
	}

	private IEnumerator PlayAnimation()
	{
		animator.Play("DayNight_Titlecard_AnimTemp", 0, 0f);
		card.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(5f / 12f);
		if (OnScreenObscured != null)
		{
			OnScreenObscured();
		}
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return null;
		}
		if (OnAnimationFinish != null)
		{
			OnAnimationFinish();
		}
		card.gameObject.SetActive(value: false);
	}
}
