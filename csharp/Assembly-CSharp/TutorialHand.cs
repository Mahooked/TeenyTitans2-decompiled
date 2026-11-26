using UnityEngine;

public class TutorialHand : MonoBehaviour
{
	public Animator tapHandAnimator;

	public Animator swipeHandAnimator;

	public Animator arrowAnimator;

	public void PlaySwipeHandDown()
	{
		HideAllAnimators();
		swipeHandAnimator.gameObject.SetActive(value: true);
		swipeHandAnimator.Play("Swipe Down");
	}

	public void PlaySwipeHandSide()
	{
		HideAllAnimators();
		swipeHandAnimator.gameObject.SetActive(value: true);
		swipeHandAnimator.Play("Swipe Side");
	}

	public void PlaySwipeHandFigureShop()
	{
		HideAllAnimators();
		swipeHandAnimator.gameObject.SetActive(value: true);
		swipeHandAnimator.Play("Flick", 0, 0f);
	}

	public void TapShopSetup()
	{
		HideAllAnimators();
		tapHandAnimator.gameObject.SetActive(value: true);
	}

	public void PlayTapHand()
	{
		HideAllAnimatorsExcept(tapHandAnimator.gameObject);
		if (!tapHandAnimator.gameObject.activeSelf)
		{
			tapHandAnimator.gameObject.SetActive(value: true);
		}
		if (!tapHandAnimator.IsName("Tap Loop"))
		{
			tapHandAnimator.Play("Tap Loop", 0, 0f);
		}
	}

	public void PlayArrowBounce()
	{
		HideAllAnimators();
		arrowAnimator.gameObject.SetActive(value: true);
	}

	public void HideAllAnimators()
	{
		tapHandAnimator.gameObject.SetActive(value: false);
		swipeHandAnimator.gameObject.SetActive(value: false);
		arrowAnimator.gameObject.SetActive(value: false);
	}

	public void HideAllAnimatorsExcept(GameObject exception)
	{
		if (exception != tapHandAnimator.gameObject)
		{
			tapHandAnimator.gameObject.SetActive(value: false);
		}
		if (exception != swipeHandAnimator.gameObject)
		{
			swipeHandAnimator.gameObject.SetActive(value: false);
		}
		if (exception != arrowAnimator.gameObject)
		{
			arrowAnimator.gameObject.SetActive(value: false);
		}
	}
}
