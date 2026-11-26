using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class TunnelEnterCard : MonoBehaviour
{
	public static TunnelEnterCard instance;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnScreenObscured__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnAnimationFinish__BackingField;

	public Animator animator;

	public tk2dBaseSprite card;

	public event Action OnScreenObscured
	{
		add
		{
			Action action = OnScreenObscured__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScreenObscured__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnScreenObscured__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnScreenObscured__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnAnimationFinish
	{
		add
		{
			Action action = OnAnimationFinish__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAnimationFinish__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnAnimationFinish__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnAnimationFinish__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

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
		card.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f / 12f);
		if (OnScreenObscured__BackingField != null)
		{
			OnScreenObscured__BackingField();
		}
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return null;
		}
		if (OnAnimationFinish__BackingField != null)
		{
			OnAnimationFinish__BackingField();
		}
		card.gameObject.SetActive(false);
	}
}
