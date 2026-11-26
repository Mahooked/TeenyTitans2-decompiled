using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialPopup : MonoBehaviour, IPoolable
{
	public enum TailPosition
	{
		Bottom,
		Top
	}

	public Animator animator;

	public tk2dBaseSprite backingSprite;

	public TextMeshPro messageText;

	public tk2dSprite bottomTail;

	public tk2dSprite sideTail;

	public KeepOnScreenSpriteTk2d box;

	private TailPosition _tailPosition;

	public TailPosition tailPosition
	{
		get
		{
			return _tailPosition;
		}
		set
		{
			SetTailPosition(value);
		}
	}

	public void SetMessage(string message)
	{
		messageText.text = message;
	}

	public void SetTailPosition(TailPosition position)
	{
		if (position != _tailPosition)
		{
			_tailPosition = position;
			switch (position)
			{
			case TailPosition.Bottom:
				box.SetOriginalPosition(new Vector3(-30f, 60f, 0f));
				bottomTail.gameObject.SetActive(value: true);
				bottomTail.scale = Vector3.one;
				bottomTail.transform.localPosition = new Vector3(13f, 33f, -2f);
				sideTail.gameObject.SetActive(value: false);
				break;
			case TailPosition.Top:
				box.SetOriginalPosition(new Vector3(-30f, -224f, 0f));
				bottomTail.gameObject.SetActive(value: true);
				bottomTail.scale = new Vector3(1f, -1f, 1f);
				bottomTail.transform.localPosition = new Vector3(13f, -33f, -2f);
				sideTail.gameObject.SetActive(value: false);
				break;
			}
		}
	}

	public void IntroPopup()
	{
		animator.Play("Tutorial_PopUp_Intro");
	}

	public void OutroPopup()
	{
		animator.Play("Tutorial_PopUp_Outro");
	}

	public void ResetObject()
	{
		gameObject.SetLayerRecursively(LayerMask.NameToLayer("Tutorial_FitHeight"));
	}
}
public class TutorialPopUp : MonoBehaviour
{
	public enum TailType
	{
		BottomLeft,
		BottomRight,
		TopRight
	}

	public Animator animator;

	public TextMeshPro text;

	public GameObject tailBottomLeft;

	public GameObject tailBottomRight;

	public GameObject tailTopRight;

	private Transform targetFollowObj;

	private Vector3 targetFollowOffset;

	private Camera targetFollowObjCamera;

	private TailType targetTailType;

	private bool followTargetObj;

	private Coroutine animFinshedCoroutine;

	private Coroutine delayedOutroCoroutine;

	public bool IsActive { get; private set; }

	public void PlayIntro()
	{
		if (animFinshedCoroutine != null)
		{
			StopCoroutine(animFinshedCoroutine);
		}
		if (delayedOutroCoroutine != null)
		{
			StopCoroutine(delayedOutroCoroutine);
		}
		animFinshedCoroutine = null;
		delayedOutroCoroutine = null;
		animator.gameObject.SetActive(value: true);
		animator.Play("Tutorial_PopUp_Intro", 0, 0f);
		IsActive = true;
	}

	public void PlayOutro()
	{
		animator.Play("Tutorial_PopUp_Outro", 0, 0f);
		animFinshedCoroutine = StartCoroutine(CheckAnimationFinished());
	}

	public void ShowBottomLeftTail()
	{
		tailBottomLeft.SetActive(value: true);
		tailBottomRight.SetActive(value: false);
		tailTopRight.SetActive(value: false);
	}

	public void ShowTopRightTail()
	{
		tailBottomLeft.SetActive(value: false);
		tailBottomRight.SetActive(value: false);
		tailTopRight.SetActive(value: true);
	}

	public void ShowBottomRightTail()
	{
		tailBottomLeft.SetActive(value: false);
		tailBottomRight.SetActive(value: true);
		tailTopRight.SetActive(value: false);
	}

	public void SetUpPopUp(string info, TailType tail, Transform targetObj, Camera targetObjCamera, bool disableAfterDelay = false, float delay = -1f, Vector3 targetObjOffset = default(Vector3))
	{
		text.text = info;
		targetFollowObj = targetObj;
		targetFollowObjCamera = targetObjCamera;
		targetFollowOffset = targetObjOffset;
		targetTailType = tail;
		followTargetObj = true;
		PlayIntro();
		if (disableAfterDelay)
		{
			Debug.Log("Delayed outro: " + delay);
			delayedOutroCoroutine = StartCoroutine(DelayedOutro(delay));
		}
	}

	private void LateUpdate()
	{
		if (followTargetObj && targetFollowObj != null && targetFollowObjCamera != null)
		{
			Vector3 screenPosition = targetFollowObjCamera.WorldToScreenPoint(targetFollowObj.position + targetFollowOffset);
			Vector3 vector = GUICamera.instance.ScreenToWorldPoint(screenPosition);
			vector.z = 0f;
			switch (targetTailType)
			{
			case TailType.BottomLeft:
				transform.position = vector;
				ShowBottomLeftTail();
				break;
			case TailType.BottomRight:
				transform.position = vector + Vector3.Scale(new Vector3(-148.802f, 0f), transform.localScale);
				ShowBottomRightTail();
				break;
			case TailType.TopRight:
				transform.position = vector + Vector3.Scale(new Vector3(-148.802f, -291.2424f), transform.localScale);
				ShowTopRightTail();
				break;
			}
		}
	}

	private IEnumerator DelayedOutro(float delay)
	{
		float timer = 0f;
		while (timer < delay)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		PlayOutro();
	}

	private IEnumerator CheckAnimationFinished()
	{
		yield return new WaitForEndOfFrame();
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return new WaitForEndOfFrame();
		}
		followTargetObj = false;
		IsActive = false;
		animator.gameObject.SetActive(value: false);
	}
}
