using DG.Tweening;
using UnityEngine;

public class EmoteUIEnemy : MonoBehaviour
{
	public EmoteDatabase emoteDatabase;

	public Animator emoteBubbleAnimator;

	public tk2dSprite emoteBubbleSprite;

	private Sequence bubbleSequence;

	private bool isActive = true;

	private void Start()
	{
		emoteBubbleAnimator.transform.localScale = Vector3.zero;
		BattleController.instance.WaitingStarted += HandleWaitingStarted;
	}

	private void OnDestroy()
	{
		if (BattleController.instance != null)
		{
			BattleController.instance.WaitingStarted -= HandleWaitingStarted;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			Emote(EmoteType.Cat);
		}
	}

	public void SetActive(bool active)
	{
		isActive = active;
		base.gameObject.SetActive(isActive);
	}

	public void Emote(EmoteType emoteType)
	{
		if (!isActive)
		{
			return;
		}
		EmoteDefinition emoteDefinition = emoteDatabase.emoteDefinitions.Find((EmoteDefinition x) => x.emote == emoteType);
		if (emoteDefinition != null)
		{
			emoteBubbleSprite.SetSprite(emoteDefinition.spriteName);
			GFAudioManager.instance.PlayClipFromResources(emoteDefinition.sfxPath, GFAudioManager.SFX_GROUP, string.Empty);
			if (!string.IsNullOrEmpty(emoteDefinition.bubbleAnimation))
			{
				emoteBubbleAnimator.Play(emoteDefinition.bubbleAnimation);
			}
		}
		if (bubbleSequence != null)
		{
			bubbleSequence.Kill();
		}
		emoteBubbleAnimator.transform.localScale = Vector3.zero;
		bubbleSequence = DOTween.Sequence();
		Tweener tweener = emoteBubbleAnimator.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
		tweener.easeOvershootOrAmplitude = 3f;
		Tweener tweener2 = emoteBubbleAnimator.transform.DOScale(0f, 0.25f).SetEase(Ease.InBack);
		tweener2.easeOvershootOrAmplitude = 3f;
		bubbleSequence.Append(tweener);
		bubbleSequence.AppendInterval(1.5f);
		bubbleSequence.Append(tweener2);
	}

	private void HandleWaitingStarted()
	{
		if (isActive)
		{
			bubbleSequence.Kill();
			Tweener tweener = emoteBubbleAnimator.transform.DOScale(0f, 0.25f).SetEase(Ease.InBack);
			tweener.easeOvershootOrAmplitude = 3f;
			tweener.ForceInit();
		}
	}
}
