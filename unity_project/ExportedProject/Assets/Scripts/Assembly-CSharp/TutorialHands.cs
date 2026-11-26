using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TutorialHands : MonoBehaviour, IPoolable
{
	public GameObject tapHand;

	public GameObject swipeHand;

	public GameObject arrow;

	public Animator tapHandAnimator;

	public Animator swipeHandAnimator;

	public Animator arrowAnimator;

	private IEnumerator dragCoroutine;

	private void Awake()
	{
		ResetActive();
	}

	private void OnDisable()
	{
		ResetActive();
	}

	public void ShowTap()
	{
		ResetActive();
		tapHand.SetActive(true);
		tapHandAnimator.Play("Tap Loop");
	}

	public void ShowTap(Vector3 position)
	{
		ShowTap();
		base.transform.position = position;
	}

	public void ShowDrag(Vector3 startPosition, Vector3 endPosition, float speed, float holdDelay = 0f)
	{
		ResetActive();
		tapHand.SetActive(true);
		dragCoroutine = PlayDrag(startPosition, endPosition, speed, holdDelay);
		StartCoroutine(dragCoroutine);
	}

	public void ShowArrow(Vector3 position, TutorialArrowDirection direction = TutorialArrowDirection.Down)
	{
		ResetActive();
		arrow.SetActive(true);
		base.transform.position = position;
		switch (direction)
		{
		case TutorialArrowDirection.Up:
			arrowAnimator.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
			break;
		case TutorialArrowDirection.Right:
			arrowAnimator.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
			break;
		case TutorialArrowDirection.Left:
			arrowAnimator.transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
			break;
		default:
			arrowAnimator.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			break;
		}
	}

	public void ShowSideSwipe(Vector3 position)
	{
		ResetActive();
		swipeHand.SetActive(true);
		swipeHandAnimator.Play("Swipe Side");
		base.transform.position = position;
	}

	public void ShowDownSwipe(Vector3 position)
	{
		ResetActive();
		swipeHand.SetActive(true);
		swipeHandAnimator.Play("Swipe Down");
		base.transform.position = position;
	}

	public void ShowFlick(Vector3 position)
	{
		ResetActive();
		swipeHand.SetActive(true);
		swipeHandAnimator.Play("Flick");
		base.transform.position = position;
	}

	private void ResetActive()
	{
		if (dragCoroutine != null)
		{
			StopCoroutine(dragCoroutine);
			dragCoroutine = null;
			DOTween.Kill(base.transform);
		}
		tapHand.SetActive(false);
		swipeHand.SetActive(false);
		arrow.SetActive(false);
	}

	private IEnumerator PlayDrag(Vector3 startPosition, Vector3 endPosition, float speed, float holdDelay = 0f)
	{
		base.transform.position = startPosition;
		while (true)
		{
			tapHandAnimator.Play("Drag Intro");
			while (!tapHandAnimator.GetCurrentAnimatorStateInfo(0).IsName("Drag Hold"))
			{
				yield return null;
			}
			Tweener tween = base.transform.DOMove(endPosition, speed).SetDelay(holdDelay).SetSpeedBased()
				.SetEase(Ease.InOutSine)
				.SetUpdate(true);
			yield return tween.WaitForCompletion();
			tapHandAnimator.Play("Drag Release");
			yield return null;
			while (tapHandAnimator.IsPlaying())
			{
				yield return null;
			}
			tween = base.transform.DOMove(startPosition, speed * 2f).SetSpeedBased().SetEase(Ease.InOutSine)
				.SetUpdate(true);
			yield return tween.WaitForCompletion();
		}
	}

	public void ResetObject()
	{
		base.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Tutorial_FitHeight"));
	}
}
