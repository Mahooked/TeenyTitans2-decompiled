using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
	public TrailRenderer trail;

	private bool allowedToMove = true;

	private int targetTouchId = -1;

	public void StartTrail(SwipeResult swipeResult)
	{
		if (GameManager.instance.gameSettings.useSwipeFXOnlyOnSwipe)
		{
			Vector3 position = GUICamera.instance.ScreenToWorldPoint(swipeResult.start);
			position.z = 0f;
			Vector3 endValue = GUICamera.instance.ScreenToWorldPoint(swipeResult.end);
			endValue.z = 0f;
			transform.position = position;
			transform.DOMove(endValue, trail.time).SetEase(Ease.OutQuad);
		}
	}

	public void SetTargetTouch(int touchID)
	{
		targetTouchId = touchID;
	}

	private void Update()
	{
		if (GameManager.instance.gameSettings.useSwipeFXOnlyOnSwipe)
		{
			return;
		}
		Touch touch = default;
		if (targetTouchId != -1)
		{
			Touch[] touches = Input.touches;
			for (int i = 0; i < touches.Length; i++)
			{
				Touch touch2 = touches[i];
				if (touch2.fingerId == targetTouchId)
				{
					touch = touch2;
				}
			}
		}
		bool flag = false;
		flag = targetTouchId != -1 && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary);
		bool flag2 = false;
		flag2 = targetTouchId != -1 && (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended);
		if (flag && allowedToMove)
		{
			Vector3 screenPosition = Vector3.zero;
			if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
			{
				screenPosition = touch.position;
			}
			Vector3 position = GUICamera.instance.ScreenToWorldPoint(screenPosition);
			position.z = 0f;
			transform.position = position;
		}
		else if (flag2)
		{
			allowedToMove = false;
			targetTouchId = -1;
			StartCoroutine(DelayedDisable());
		}
	}

	private IEnumerator DelayedDisable()
	{
		yield return new WaitForSeconds(trail.time);
		allowedToMove = true;
		ObjectPool.PoolObject(gameObject);
	}
}
