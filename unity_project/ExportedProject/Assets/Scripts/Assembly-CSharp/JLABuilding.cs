using System.Collections;
using UnityEngine;

public class JLABuilding : MonoBehaviour
{
	private CityEvent cityEvent;

	public CityShopSign shopSign;

	public Animator animator;

	public Animator lightsAnimator;

	public GameObject[] disabledObjects;

	private AnimatorDisabler _animatorDisabler;

	private AnimatorDisabler _lightsAnimatorDisabler;

	private AnimatorDisabler animatorDisabler
	{
		get
		{
			if (_animatorDisabler == null)
			{
				_animatorDisabler = animator.gameObject.GetComponent<AnimatorDisabler>();
			}
			return _animatorDisabler;
		}
	}

	private AnimatorDisabler lightsAnimatorDisabler
	{
		get
		{
			if (_lightsAnimatorDisabler == null)
			{
				_lightsAnimatorDisabler = lightsAnimator.gameObject.GetComponent<AnimatorDisabler>();
			}
			return _lightsAnimatorDisabler;
		}
	}

	private IEnumerator Start()
	{
		cityEvent = GetComponent<CityEvent>();
		cityEvent.EventEnabled += HandleCityEventEnabled;
		cityEvent.animator = animator;
		yield return new WaitForEndOfFrame();
	}

	private void OnDestroy()
	{
		if (cityEvent != null)
		{
			cityEvent.EventEnabled -= HandleCityEventEnabled;
		}
	}

	private void HandleCityEventEnabled(bool isEnabled, CityEventData cityEventData)
	{
		if (isEnabled && cityEventData != null)
		{
			EnableBuilding();
		}
		else
		{
			DisableBuilding();
		}
	}

	private void EnableBuilding()
	{
		shopSign.gameObject.SetActive(false);
		PlayBuildingAnimation();
		for (int i = 0; i < disabledObjects.Length; i++)
		{
			disabledObjects[i].SetActive(true);
		}
		cityEvent.CheckBuildingPad();
	}

	private void DisableBuilding()
	{
		shopSign.gameObject.SetActive(true);
		shopSign.SetState(CityShopSign.State.ComingSoon);
		StopBuildingAnimation();
		for (int i = 0; i < disabledObjects.Length; i++)
		{
			disabledObjects[i].SetActive(false);
		}
		cityEvent.CheckBuildingPad();
	}

	private void PlayBuildingAnimation()
	{
		if (animator != null)
		{
			if (animatorDisabler != null)
			{
				animatorDisabler.enabled = true;
			}
			animator.Play("ShopBuilding_AnimLoop", 0, 0f);
		}
		if (lightsAnimator != null)
		{
			if (lightsAnimatorDisabler != null)
			{
				lightsAnimatorDisabler.enabled = true;
			}
			lightsAnimator.Play("JLA_building_loop", 0, 0f);
		}
	}

	private void StopBuildingAnimation()
	{
		if (animator != null)
		{
			animator.Play("ShopBuilding_AnimLoop", 0, 0f);
			animator.StopPlayback();
			animator.enabled = false;
			if (animatorDisabler != null)
			{
				animatorDisabler.enabled = false;
			}
			animator.transform.localScale = Vector3.one;
		}
		if (lightsAnimator != null)
		{
			animator.StopPlayback();
			lightsAnimator.enabled = false;
			if (lightsAnimatorDisabler != null)
			{
				lightsAnimatorDisabler.enabled = false;
			}
		}
	}
}
