using UnityEngine;

public class AEAnimatorDisabler : MonoBehaviour
{
	[SerializeField]
	private AEAnimator _aeAnimator;

	private int updateFrame;

	private Transform aeAnimatorTransform;

	private Camera cam;

	private Rect viewportRect;

	private static int _staggerNum;

	private static readonly int updateFrameDelay = 10;

	private static int staggerNum
	{
		get
		{
			int result = _staggerNum;
			_staggerNum++;
			if (_staggerNum == updateFrameDelay)
			{
				_staggerNum = 0;
			}
			return result;
		}
	}

	private void Awake()
	{
		updateFrame = staggerNum;
	}

	private void Start()
	{
		aeAnimatorTransform = _aeAnimator.transform;
		cam = Camera.main;
		viewportRect = cam.rect;
	}

	private void Update()
	{
		if (Time.frameCount % updateFrameDelay != updateFrame)
		{
			return;
		}
		Vector3 localPosition = _aeAnimator.spritesHolder.localPosition;
		Vector3 position = localPosition + new Vector3(_aeAnimator.animationAsset.composition.width / (float)_aeAnimator.pixelsPerMeter, 0f);
		Vector3 position2 = localPosition + new Vector3(0f, (0f - _aeAnimator.animationAsset.composition.height) / (float)_aeAnimator.pixelsPerMeter);
		Vector3 position3 = new Vector3(position.x, position2.y, localPosition.z);
		Vector2 point = cam.WorldToViewportPoint(aeAnimatorTransform.TransformPoint(localPosition));
		Vector2 point2 = cam.WorldToViewportPoint(aeAnimatorTransform.TransformPoint(position));
		Vector2 point3 = cam.WorldToViewportPoint(aeAnimatorTransform.TransformPoint(position2));
		Vector2 point4 = cam.WorldToViewportPoint(aeAnimatorTransform.TransformPoint(position3));
		if (viewportRect.Contains(point) || viewportRect.Contains(point2) || viewportRect.Contains(point3) || viewportRect.Contains(point4))
		{
			if (!_aeAnimator.enabled)
			{
				_aeAnimator.enabled = true;
			}
		}
		else if (_aeAnimator.enabled)
		{
			_aeAnimator.enabled = false;
		}
	}
}
