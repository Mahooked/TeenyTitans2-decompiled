using System.Collections;
using UnityEngine;

public class CinemaBars : MonoBehaviour
{
	public bool movingUpOrDown;

	private Vector3 idleLocalPosition;

	private Vector3 onScreenLocalPosition;

	private float currentLerp;

	private float lerpTime;

	private Coroutine animationRoutine;

	private void Start()
	{
		idleLocalPosition = transform.localPosition;
		if (movingUpOrDown)
		{
			onScreenLocalPosition = new Vector3(transform.localPosition.x, -80f, transform.localPosition.z);
		}
		else
		{
			onScreenLocalPosition = new Vector3(transform.localPosition.x, 80f, transform.localPosition.z);
		}
	}

	public void ShowCinemaBars(float t = 1f)
	{
		if (animationRoutine != null)
		{
			StopCoroutine(animationRoutine);
		}
		animationRoutine = StartCoroutine(ShowingCinemaBars(t));
	}

	private IEnumerator ShowingCinemaBars(float time)
	{
		if (transform.localPosition.Equals(onScreenLocalPosition) && GetComponent<Renderer>().enabled)
		{
			yield break;
		}
		float t = 0f;
		currentLerp = 0f;
		lerpTime = time;
		GetComponent<Renderer>().enabled = true;
		while (t < 1f && lerpTime > 0f)
		{
			currentLerp += Time.unscaledDeltaTime;
			if (currentLerp >= lerpTime)
			{
				currentLerp = lerpTime;
			}
			t = currentLerp / lerpTime;
			t = t * t * (3f - 2f * t);
			transform.localPosition = Vector3.Lerp(idleLocalPosition, onScreenLocalPosition, t);
			yield return null;
		}
		transform.localPosition = onScreenLocalPosition;
	}

	public void HideCinemaBars(float t = 1f, float delay = 0f)
	{
		if (animationRoutine != null)
		{
			StopCoroutine(animationRoutine);
		}
		animationRoutine = StartCoroutine(HidingCinemaBars(t, delay));
	}

	private IEnumerator HidingCinemaBars(float time, float delay = 0f)
	{
		yield return new WaitForSeconds(delay);
		if (transform.localPosition.Equals(idleLocalPosition) && !GetComponent<Renderer>().enabled)
		{
			yield break;
		}
		float t = 0f;
		currentLerp = 0f;
		lerpTime = time;
		while (t < 1f && lerpTime > 0f)
		{
			currentLerp += Time.unscaledDeltaTime;
			if (currentLerp >= lerpTime)
			{
				currentLerp = lerpTime;
			}
			t = currentLerp / lerpTime;
			t = t * t * (3f - 2f * t);
			transform.localPosition = Vector3.Lerp(onScreenLocalPosition, idleLocalPosition, t);
			yield return null;
		}
		transform.localPosition = idleLocalPosition;
		GetComponent<Renderer>().enabled = false;
	}
}
