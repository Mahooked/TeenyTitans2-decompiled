using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpBubble : MonoBehaviour
{
	public enum BubbleType
	{
		None = 0,
		Normal = 1,
		UpsideDown = 2
	}

	public Animator animator;

	public Transform bubbleGroupObj;

	public Transform tailGroupObj;

	public TextMeshPro bubbleTextMesh;

	public tk2dSlicedSprite bubbleBackgroundSprite;

	public Transform bubbleTailAttachPoint;

	[Space]
	public float minViewportXPosition = 0.1f;

	public float maxViewportXPosition = 0.9f;

	[Space]
	public float minViewportYPosition = 0.1f;

	public float maxViewportYPosition = 0.9f;

	[Space]
	public float bubbleGroupObjMinLocalX;

	public float bubbleGroupObjMaxLocalX;

	public float bubbleGroupObjMinLocalXTailFlipped;

	public float bubbleGroupObjMaxLocalXTailFlipped;

	private Transform followTarget;

	private Vector3 followTargetOffset;

	private Camera targetCamera;

	private bool useTail = true;

	private bool isBubbleTypeForced;

	private bool initialized;

	private bool isOutroing;

	private bool tailXFlipped;

	private bool debug;

	private const string INTRO_ANIMATION = "Tutorial_PopUp_Intro";

	private const string OUTRO_ANIMATION = "Tutorial_PopUp_Outro";

	private const string HOLD_ANIMATION = "Tutorial_PopUp_Hold";

	private BubbleType _bubbleType;

	public bool reintroingBubble { get; private set; }

	public BubbleType bubbleType
	{
		get
		{
			return _bubbleType;
		}
		set
		{
			_bubbleType = value;
		}
	}

	public bool IsOutroing
	{
		get
		{
			return isOutroing;
		}
		set
		{
			isOutroing = value;
		}
	}

	public bool IsShowing
	{
		get
		{
			return base.gameObject.activeInHierarchy && !isOutroing;
		}
	}

	public void Init(string message, Vector3 targetWorldPosition, Camera targetCamera, BubbleType forcedBubbleType = BubbleType.None, bool flipTailX = false, bool debug = false)
	{
		if (animator.IsName("Tutorial_PopUp_Hold"))
		{
			StartCoroutine(ReintroBubbleSequence(message, targetWorldPosition, targetCamera, forcedBubbleType, true, flipTailX, debug));
			return;
		}
		animator.Play("Tutorial_PopUp_Intro");
		bubbleTextMesh.text = message;
		base.transform.position = targetWorldPosition;
		isBubbleTypeForced = forcedBubbleType != BubbleType.None;
		_bubbleType = ((!isBubbleTypeForced) ? BubbleType.Normal : forcedBubbleType);
		this.targetCamera = targetCamera;
		useTail = true;
		tailXFlipped = flipTailX;
		tailGroupObj.gameObject.SetActive(useTail);
		tailGroupObj.transform.localScale = new Vector3(Mathf.Abs(tailGroupObj.transform.localScale.x) * (float)((!tailXFlipped) ? 1 : (-1)), tailGroupObj.transform.localScale.y, tailGroupObj.transform.localScale.z);
		initialized = true;
		this.debug = debug;
		StartCoroutine(DelayedUpdateBubblePosition());
	}

	public void Init(string message, Transform followTarget, Vector3 followTargetOffset, Camera targetCamera, BubbleType forcedBubbleType = BubbleType.None, bool flipTailX = false)
	{
		if (animator.IsName("Tutorial_PopUp_Hold"))
		{
			StartCoroutine(ReintroBubbleSequence(message, followTarget, followTargetOffset, targetCamera, forcedBubbleType, true, flipTailX));
			return;
		}
		animator.Play("Tutorial_PopUp_Intro");
		bubbleTextMesh.text = message;
		this.followTarget = followTarget;
		this.followTargetOffset = followTargetOffset;
		isBubbleTypeForced = forcedBubbleType != BubbleType.None;
		_bubbleType = ((!isBubbleTypeForced) ? BubbleType.Normal : forcedBubbleType);
		this.targetCamera = targetCamera;
		useTail = true;
		tailXFlipped = flipTailX;
		tailGroupObj.gameObject.SetActive(useTail);
		tailGroupObj.transform.localScale = new Vector3(Mathf.Abs(tailGroupObj.transform.localScale.x) * (float)((!tailXFlipped) ? 1 : (-1)), tailGroupObj.transform.localScale.y, tailGroupObj.transform.localScale.z);
		initialized = true;
		base.transform.position = followTarget.position + followTargetOffset;
		StartCoroutine(DelayedUpdateBubblePosition());
	}

	public void InitWithoutTail(string message, Vector3 targetWorldPosition, Camera targetCamera, BubbleType forcedBubbleType = BubbleType.None, bool flipTailX = false)
	{
		if (animator.IsName("Tutorial_PopUp_Hold"))
		{
			StartCoroutine(ReintroBubbleSequence(message, targetWorldPosition, targetCamera, forcedBubbleType, false, flipTailX));
			return;
		}
		animator.Play("Tutorial_PopUp_Intro");
		bubbleTextMesh.text = message;
		base.transform.position = targetWorldPosition;
		isBubbleTypeForced = forcedBubbleType != BubbleType.None;
		_bubbleType = ((!isBubbleTypeForced) ? BubbleType.Normal : forcedBubbleType);
		this.targetCamera = targetCamera;
		useTail = false;
		tailXFlipped = flipTailX;
		tailGroupObj.gameObject.SetActive(useTail);
		tailGroupObj.transform.localScale = new Vector3(Mathf.Abs(tailGroupObj.transform.localScale.x) * (float)((!tailXFlipped) ? 1 : (-1)), tailGroupObj.transform.localScale.y, tailGroupObj.transform.localScale.z);
		initialized = true;
		StartCoroutine(DelayedUpdateBubblePosition());
	}

	public void InitWithoutTail(string message, Transform followTarget, Vector3 followTargetOffset, Camera targetCamera, BubbleType forcedBubbleType = BubbleType.None, bool flipTailX = false)
	{
		if (animator.IsName("Tutorial_PopUp_Hold"))
		{
			StartCoroutine(ReintroBubbleSequence(message, followTarget, followTargetOffset, targetCamera, forcedBubbleType, false, flipTailX));
			return;
		}
		animator.Play("Tutorial_PopUp_Intro");
		bubbleTextMesh.text = message;
		this.followTarget = followTarget;
		this.followTargetOffset = followTargetOffset;
		isBubbleTypeForced = forcedBubbleType != BubbleType.None;
		_bubbleType = ((!isBubbleTypeForced) ? BubbleType.Normal : forcedBubbleType);
		this.targetCamera = targetCamera;
		useTail = false;
		tailXFlipped = flipTailX;
		tailGroupObj.gameObject.SetActive(useTail);
		tailGroupObj.transform.localScale = new Vector3(Mathf.Abs(tailGroupObj.transform.localScale.x) * (float)((!tailXFlipped) ? 1 : (-1)), tailGroupObj.transform.localScale.y, tailGroupObj.transform.localScale.z);
		initialized = true;
		base.transform.position = followTarget.position + followTargetOffset;
		StartCoroutine(DelayedUpdateBubblePosition());
	}

	public void OutroBubble(float delay = 0f, bool poolWhenFinished = false)
	{
		if (!isOutroing)
		{
			StartCoroutine(OutroSequence(delay, poolWhenFinished));
		}
	}

	private IEnumerator OutroSequence(float delay, bool poolWhenFinished)
	{
		isOutroing = true;
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		animator.Play("Tutorial_PopUp_Outro");
		animator.Update(0f);
		yield return null;
		while (animator.IsPlaying())
		{
			yield return null;
		}
		isOutroing = false;
		if (poolWhenFinished)
		{
			ObjectPool.PoolObject(base.gameObject);
		}
	}

	private IEnumerator ReintroBubbleSequence(string message, Vector3 targetWorldPosition, Camera targetCamera, BubbleType forcedBubbleType, bool activeTail, bool flipTailX, bool debug = false)
	{
		reintroingBubble = true;
		yield return StartCoroutine(OutroSequence(0f, false));
		animator.Play("Tutorial_PopUp_Intro");
		animator.Update(0f);
		reintroingBubble = false;
		if (activeTail)
		{
			Init(message, targetWorldPosition, targetCamera, forcedBubbleType, false, debug);
		}
		else
		{
			InitWithoutTail(message, targetWorldPosition, targetCamera, forcedBubbleType);
		}
	}

	private IEnumerator ReintroBubbleSequence(string message, Transform followTarget, Vector3 followTargetOffset, Camera targetCamera, BubbleType forcedBubbleType, bool activeTail, bool flipTailX)
	{
		reintroingBubble = true;
		yield return StartCoroutine(OutroSequence(0f, false));
		animator.Play("Tutorial_PopUp_Intro");
		reintroingBubble = false;
		if (activeTail)
		{
			Init(message, followTarget, followTargetOffset, targetCamera, forcedBubbleType);
		}
		else
		{
			InitWithoutTail(message, followTarget, followTargetOffset, targetCamera, forcedBubbleType);
		}
	}

	private IEnumerator DelayedUpdateBubblePosition()
	{
		yield return null;
		UpdateBubblePosition();
	}

	private void UpdateBubblePosition()
	{
		if (!initialized)
		{
			return;
		}
		Vector3 b = new Vector3(1f, -1f, 1f);
		if (tailGroupObj.localScale.y < 0f)
		{
			tailGroupObj.localScale = Vector3.Scale(tailGroupObj.localScale, b);
		}
		if (bubbleGroupObj.localScale.y < 0f)
		{
			bubbleGroupObj.localScale = Vector3.Scale(bubbleGroupObj.localScale, b);
		}
		if (targetCamera == null)
		{
			targetCamera = GUICamera.instance.camera;
		}
		Vector3 position = bubbleBackgroundSprite.transform.position;
		Vector3 vector = bubbleBackgroundSprite.transform.position + Vector3.Scale(bubbleBackgroundSprite.dimensions, bubbleBackgroundSprite.transform.lossyScale);
		Vector3 position2 = targetCamera.WorldToViewportPoint(position);
		Vector3 position3 = targetCamera.WorldToViewportPoint(vector);
		if (position2.x < minViewportXPosition)
		{
			position2.x = Mathf.Max(minViewportXPosition, position2.x);
			Vector3 vector2 = targetCamera.ViewportToWorldPoint(position2);
			Vector3 vector3 = vector2 - position;
			bubbleGroupObj.localPosition += vector3;
		}
		else if (position3.x > maxViewportXPosition)
		{
			position3.x = Mathf.Min(maxViewportXPosition, position3.x);
			Vector3 vector4 = targetCamera.ViewportToWorldPoint(position3);
			Vector3 vector5 = vector4 - vector;
			bubbleGroupObj.localPosition += vector5;
		}
		Vector2 vector6 = ((!tailXFlipped) ? new Vector2(bubbleGroupObjMinLocalX, bubbleGroupObjMaxLocalX) : new Vector2(bubbleGroupObjMinLocalXTailFlipped, bubbleGroupObjMaxLocalXTailFlipped));
		bubbleGroupObj.localPosition = new Vector3(Mathf.Clamp(bubbleGroupObj.localPosition.x, vector6.x, vector6.y), bubbleGroupObj.localPosition.y, bubbleGroupObj.localPosition.z);
		if (!isBubbleTypeForced)
		{
			bubbleType = ((!(position3.y > maxViewportYPosition)) ? BubbleType.Normal : BubbleType.UpsideDown);
		}
		if (bubbleType == BubbleType.UpsideDown)
		{
			if (tailGroupObj.localScale.y > 0f)
			{
				tailGroupObj.localScale = Vector3.Scale(tailGroupObj.localScale, b);
			}
			if (bubbleGroupObj.localScale.y > 0f)
			{
				bubbleGroupObj.localScale = Vector3.Scale(bubbleGroupObj.localScale, b);
			}
		}
		Vector3 vector7 = bubbleBackgroundSprite.dimensions / 2f;
		Vector3 vector8 = bubbleGroupObj.localPosition + bubbleBackgroundSprite.transform.localPosition + vector7;
		if (bubbleType == BubbleType.UpsideDown)
		{
			vector8 = Vector3.Scale(vector8, b);
		}
		bubbleTextMesh.transform.localPosition = vector8;
	}

	public void SetLayer(int layer)
	{
		base.gameObject.SetLayerRecursively(layer);
	}

	private void LateUpdate()
	{
		if (followTarget != null)
		{
			base.transform.position = followTarget.position + followTargetOffset;
		}
		UpdateBubblePosition();
	}

	public YieldInstruction WaitUntilIntroComplete()
	{
		return StartCoroutine(WaitUntilIntroCompleteSequence());
	}

	private IEnumerator WaitUntilIntroCompleteSequence()
	{
		yield return null;
		while (reintroingBubble || (animator.IsPlaying() && animator.IsName("Tutorial_PopUp_Intro")))
		{
			yield return null;
		}
	}

	public YieldInstruction WaitUntilOutroComplete()
	{
		return StartCoroutine(WaitUntilOutroCompleteSequence());
	}

	private IEnumerator WaitUntilOutroCompleteSequence()
	{
		Debug.Log("Waiting Until Outro Complete Sequence...");
		yield return null;
		while (animator.IsPlaying() && animator.IsName("Tutorial_PopUp_Outro"))
		{
			yield return null;
		}
	}
}
