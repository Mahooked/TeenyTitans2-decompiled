using System;
using System.Collections;
using UnityEngine;

public class Tutorial_Battle_Tofu : MonoBehaviour
{
	public BattleController battleController;

	private BattleObjectManager battleObjectManager;

	private TutorialHands tapFinger;

	private void Start()
	{
		if (Player.instance.HasTriggered("tutorialTofu") || Time.frameCount == 1)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		if (battleController == null)
		{
			battleController = BattleController.instance;
		}
		battleObjectManager = BattleObjectManager.instance;
		battleObjectManager.ObjectAdded += HandleObjectAdded;
	}

	private void OnDestroy()
	{
		if (battleObjectManager != null)
		{
			battleObjectManager.ObjectAdded -= HandleObjectAdded;
		}
	}

	private void HandleObjectAdded(GameObject obj)
	{
		TofuBubble component = obj.GetComponent<TofuBubble>();
		if (component != null)
		{
			battleObjectManager.ObjectAdded -= HandleObjectAdded;
			StartCoroutine(RunTutorial(component));
		}
	}

	private IEnumerator RunTutorial(TofuBubble bubble)
	{
		bubble.ForceTofuType(TofuType.Spicy);
		yield return new WaitWhile(() => bubble.Spinning);
		tapFinger = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tapFinger.transform.parent = bubble.tofuSprite.transform.parent;
		tapFinger.transform.localPosition = Vector3.zero;
		tapFinger.transform.localScale = Vector3.one;
		tapFinger.transform.localRotation = Quaternion.identity;
		tapFinger.gameObject.SetLayerRecursively(bubble.gameObject.layer);
		tapFinger.ShowTap();
		bubble.Minimized += HandleBubbleMinimized;
		bubble.Maximized += HandleBubbleMaximized;
		if (bubble.Paused)
		{
			HandleBubbleMinimized();
		}
		bool tofuUsed = false;
		Action tofuUsedDelegate = null;
		tofuUsedDelegate = () =>
		{
			tofuUsed = true;
			bubble.TofuUsed -= tofuUsedDelegate;
		};
		bubble.TofuUsed += tofuUsedDelegate;
		yield return new WaitWhile(() => !tofuUsed);
		tapFinger.transform.parent = null;
		tapFinger.transform.localScale = Vector3.one;
		tapFinger.transform.localRotation = Quaternion.identity;
		ObjectPool.PoolObject(tapFinger.gameObject);
		bubble.Minimized -= HandleBubbleMinimized;
		bubble.Maximized -= HandleBubbleMaximized;
		Player.instance.SetTrigger("tutorialTofu");
	}

	private void HandleBubbleMinimized()
	{
		tapFinger.gameObject.SetActive(value: false);
	}

	private void HandleBubbleMaximized()
	{
		tapFinger.gameObject.SetActive(value: true);
		tapFinger.ShowTap();
	}
}
