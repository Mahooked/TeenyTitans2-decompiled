using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EmoteUI : MonoBehaviour
{
	public EmoteDatabase emoteDatabase;

	public tk2dUIItem tabBtn;

	public tk2dSprite tabSprite;

	public tk2dClippedSprite tabCooldownSprite;

	public Transform emoteShelf;

	public tk2dSprite[] shelfSprites;

	public float tabBtnOnscreenLocalXPos;

	public float shelfOpenLocalXPos;

	public Animator emoteBubbleAnimator;

	public tk2dSprite emoteBubbleSprite;

	public Transform buttonParent;

	public EmoteButton prototypeButton;

	public float buttonSpacing = 30f;

	private EmoteButton[] emoteButtons;

	private Sequence bubbleSequence;

	private Coroutine followRoutine;

	private Vector3 tabBtnOffscreenLocalPos;

	private Vector3 shelfOffscreenLocalPos;

	private bool isShelfPanelOpen;

	private bool inShelfMode;

	private float spamTimer;

	private float consecutivesUses;

	private bool isActive = true;

	private IEnumerator Start()
	{
		emoteButtons = new EmoteButton[emoteDatabase.emoteDefinitions.Count];
		int len = emoteButtons.Length;
		for (int i = 0; i < len; i++)
		{
			EmoteButton emoteButton;
			if (i > 0)
			{
				emoteButton = Object.Instantiate(prototypeButton, buttonParent);
				emoteButton.transform.position = prototypeButton.transform.position + new Vector3(0f, (0f - buttonSpacing) * (float)i);
			}
			else
			{
				emoteButton = prototypeButton;
			}
			emoteButton.Init(emoteDatabase.emoteDefinitions[len - 1 - i].emote, emoteDatabase.emoteDefinitions[len - 1 - i].spriteName);
			emoteButton.uiItem.OnClickUIItem += HandleEmoteButtonClicked;
			emoteButton.uiItem.enabled = false;
			emoteButtons[i] = emoteButton;
		}
		Vector3 iPhoneXOffset = new Vector3(0f, 20f);
		emoteShelf.localPosition = ((Screen.height != 2436 && Screen.height != 609) ? emoteShelf.localPosition : (emoteShelf.localPosition + iPhoneXOffset));
		tabBtn.transform.localPosition = ((Screen.height != 2436 && Screen.height != 609) ? tabBtn.transform.localPosition : (tabBtn.transform.localPosition + iPhoneXOffset));
		shelfOffscreenLocalPos = emoteShelf.localPosition;
		tabBtnOffscreenLocalPos = tabBtn.transform.localPosition;
		tabBtn.OnClick += HandleTabBtnClicked;
		if (BattleController.instance != null)
		{
			BattleController.instance.BattleStarted += HandleBattleStarted;
			BattleController.instance.BattleEnded += HandleBattleEnded;
			BattleController.instance.WaitingStarted += HandleWaitingStarted;
			BattleController.instance.WaitingEnded += HandleWaitingEnded;
			if (BattleController.instance.useFlashbackBattleEnding)
			{
				SetActive(active: false);
			}
		}
		tabCooldownSprite.color = new Color(tabCooldownSprite.color.r, tabCooldownSprite.color.g, tabCooldownSprite.color.b, 0f);
		emoteBubbleAnimator.transform.localScale = Vector3.zero;
		yield return new WaitForEndOfFrame();
		if (TutorialManager.instance != null)
		{
			TutorialManager.instance.ActiveTutorialChanged += HandleActiveTutorialChanged;
			HandleActiveTutorialChanged(TutorialManager.instance.activeTutorial);
		}
	}

	private void OnDestroy()
	{
		if (emoteButtons != null)
		{
			int num = emoteButtons.Length;
			for (int i = 0; i < num; i++)
			{
				emoteButtons[i].uiItem.OnClickUIItem -= HandleEmoteButtonClicked;
			}
		}
		tabBtn.OnClick -= HandleTabBtnClicked;
		if (BattleController.instance != null)
		{
			BattleController.instance.BattleStarted -= HandleBattleStarted;
			BattleController.instance.BattleEnded -= HandleBattleEnded;
			BattleController.instance.WaitingStarted -= HandleWaitingStarted;
			BattleController.instance.WaitingEnded -= HandleWaitingEnded;
		}
		if (TutorialManager.instance != null)
		{
			TutorialManager.instance.ActiveTutorialChanged -= HandleActiveTutorialChanged;
		}
	}

	private void OnEnable()
	{
		emoteBubbleAnimator.gameObject.SetActive(value: true);
		if (BattleController.instance != null && BattleController.instance.battleStarted)
		{
			HandleBattleStarted();
		}
	}

	private void OnDisable()
	{
		emoteBubbleAnimator.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			Emote(EmoteType.Angry);
		}
		if (!(consecutivesUses > 0f))
		{
			return;
		}
		spamTimer -= Time.deltaTime;
		if (spamTimer <= 0f)
		{
			consecutivesUses--;
			if (consecutivesUses > 0f)
			{
				spamTimer = emoteDatabase.spamDelay;
			}
			if (consecutivesUses < (float)emoteDatabase.maxSpamUses)
			{
				SetTabBtnEnabled(enable: true);
				tabCooldownSprite.color = new Color(tabCooldownSprite.color.r, tabCooldownSprite.color.g, tabCooldownSprite.color.b, 0f);
			}
		}
		if (consecutivesUses >= (float)emoteDatabase.maxSpamUses)
		{
			tabCooldownSprite.ClipRect = new Rect(0f, 0f, tabCooldownSprite.ClipRect.width, Mathf.Clamp01(spamTimer / emoteDatabase.spamDelay));
		}
	}

	private void LateUpdate()
	{
		if (isShelfPanelOpen && Input.GetMouseButtonUp(0) && (BattleController.instance == null || !BattleController.instance.waiting))
		{
			CloseEmoteShelf();
		}
	}

	public void SetActive(bool active)
	{
		isActive = active;
		gameObject.SetActive(isActive);
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
			TT2NetManager.SendEmoteSent(emoteType);
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
		bubbleSequence.AppendCallback(() =>
		{
		});
	}

	private void HandleBattleStarted()
	{
		if (isActive)
		{
			tabBtn.transform.DOLocalMoveX(tabBtnOnscreenLocalXPos, 0.25f);
		}
	}

	private void HandleBattleEnded()
	{
		if (isActive)
		{
			tabBtn.transform.DOLocalMove(tabBtnOffscreenLocalPos, 0.25f);
		}
	}

	private void HandleWaitingStarted()
	{
		if (isActive)
		{
			Color gray = Color.gray;
			SetTabBtnEnabled(enable: false);
			int num = shelfSprites.Length;
			for (int i = 0; i < num; i++)
			{
				shelfSprites[i].color = gray;
			}
			num = emoteButtons.Length;
			for (int j = 0; j < num; j++)
			{
				EmoteButton emoteButton = emoteButtons[j];
				emoteButton.uiItem.enabled = false;
				emoteButton.emoteSprite.color = gray;
			}
			bubbleSequence.Kill();
			Tweener tweener = emoteBubbleAnimator.transform.DOScale(0f, 0.25f).SetEase(Ease.InBack);
			tweener.easeOvershootOrAmplitude = 3f;
			tweener.ForceInit();
		}
	}

	private void HandleWaitingEnded()
	{
		if (isActive)
		{
			Color white = Color.white;
			SetTabBtnEnabled(enable: true);
			int num = shelfSprites.Length;
			for (int i = 0; i < num; i++)
			{
				shelfSprites[i].color = white;
			}
			num = emoteButtons.Length;
			for (int j = 0; j < num; j++)
			{
				EmoteButton emoteButton = emoteButtons[j];
				emoteButton.uiItem.enabled = true;
				emoteButton.emoteSprite.color = white;
			}
		}
	}

	private void HandleEmoteButtonClicked(tk2dUIItem tk2dUiItem)
	{
		if (!isActive || !tk2dUiItem.enabled)
		{
			return;
		}
		EmoteButton component = tk2dUiItem.GetComponent<EmoteButton>();
		if (component != null)
		{
			consecutivesUses++;
			spamTimer = emoteDatabase.spamDelay;
			if (consecutivesUses >= (float)emoteDatabase.maxSpamUses)
			{
				SetTabBtnEnabled(enable: false);
				tabCooldownSprite.color = new Color(tabCooldownSprite.color.r, tabCooldownSprite.color.g, tabCooldownSprite.color.b, 1f);
			}
			Emote(component.currentEmoteType);
		}
	}

	private void HandleTabBtnClicked()
	{
		OpenEmoteShelf();
	}

	private void OpenEmoteShelf()
	{
		inShelfMode = true;
		tabBtn.transform.DOLocalMove(tabBtnOffscreenLocalPos, 0.25f);
		SetTabBtnEnabled(enable: false);
		emoteShelf.DOLocalMoveX(shelfOpenLocalXPos, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
		{
			isShelfPanelOpen = true;
			if (BattleController.instance == null || !BattleController.instance.waiting)
			{
				for (int i = 0; i < emoteButtons.Length; i++)
				{
					emoteButtons[i].uiItem.enabled = true;
				}
			}
		});
	}

	private void CloseEmoteShelf()
	{
		inShelfMode = false;
		isShelfPanelOpen = false;
		for (int i = 0; i < emoteButtons.Length; i++)
		{
			emoteButtons[i].uiItem.enabled = false;
		}
		tabBtn.transform.DOLocalMoveX(tabBtnOnscreenLocalXPos, 0.25f).OnComplete(() =>
		{
			SetTabBtnEnabled(enable: true);
		});
		emoteShelf.DOLocalMove(shelfOffscreenLocalPos, 0.25f).SetEase(Ease.InBack);
	}

	private void SetTabBtnEnabled(bool enable, bool force = false)
	{
		if (enable && ((!inShelfMode && (BattleController.instance == null || !BattleController.instance.waiting) && consecutivesUses < (float)emoteDatabase.maxSpamUses) || force))
		{
			tabBtn.enabled = true;
			tabSprite.color = Color.white;
		}
		else if (!enable && (inShelfMode || BattleController.instance == null || BattleController.instance.waiting || consecutivesUses >= (float)emoteDatabase.maxSpamUses || force))
		{
			tabBtn.enabled = false;
			tabSprite.color = ((!(BattleController.instance != null) || !BattleController.instance.waiting) ? Color.white : Color.gray);
		}
	}

	private void HandleActiveTutorialChanged(Tutorial tutorial)
	{
		if (isActive)
		{
			gameObject.SetActive(tutorial == null && !BattleController.instance.useFlashbackBattleEnding);
		}
	}
}
