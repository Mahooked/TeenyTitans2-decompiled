using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RiddleEggReveal : MonoBehaviour, IPoolable
{
	public Animator mainAnimator;

	public Animator eggAnimator;

	public AnimatorEventListener eggAnimatorEventListener;

	public tk2dUIItem eggUIItem;

	public TextMeshPro amountText;

	public CurrencyGUI currencyGUI;

	public tk2dUIItem continueButton;

	public RiddleEggRevealQMark[] questionMarks;

	public float spacing = 50f;

	public Vector3[] questionMarkPositions;

	[Space]
	public Color greenEggFaceColor;

	public Color goldenEggFaceColor;

	public Renderer[] eggRenderers;

	[Header("Sound")]
	public AudioClip showEggClip;

	[Range(0f, 1f)]
	public float showEggVolume = 1f;

	public AudioClip holdToBreakClip;

	[Range(0f, 1f)]
	public float holdToBreakVolume = 1f;

	public AudioClip duplicateFigClip;

	[Range(0f, 1f)]
	public float duplicateFigVolume = 1f;

	public AudioClip newFigClip;

	[Range(0f, 1f)]
	public float newFigVolume = 1f;

	private List<PlayerReward> rewardQueue = new List<PlayerReward>();

	private int curEggCount;

	private int totalEggCount;

	private List<RiddleEggRevealQMark> spawningQMarks = new List<RiddleEggRevealQMark>();

	private List<Material> eggMaterials = new List<Material>();

	private static RiddleEggReveal instance;

	private Texture _greenEggTex;

	private Texture _goldenEggTex;

	public static RiddleEggReveal Instance => instance;

	private Texture greenEggTex
	{
		get
		{
			if (_greenEggTex == null)
			{
				_greenEggTex = Resources.Load<Texture>("Textures/iap_eggGreen-" + tk2dSystem.CurrentPlatform);
			}
			return _greenEggTex;
		}
	}

	private Texture goldenEggTex
	{
		get
		{
			if (_goldenEggTex == null)
			{
				_goldenEggTex = Resources.Load<Texture>("Textures/iap_eggGold-" + tk2dSystem.CurrentPlatform);
			}
			return _goldenEggTex;
		}
	}

	private void Awake()
	{
		eggAnimator.gameObject.SetActive(value: false);
		continueButton.gameObject.SetActive(value: false);
		for (int i = 0; i < questionMarks.Length; i++)
		{
			questionMarks[i].gameObject.SetActive(value: false);
		}
		for (int j = 0; j < eggRenderers.Length; j++)
		{
			if (!eggMaterials.Contains(eggRenderers[j].sharedMaterial))
			{
				eggMaterials.Add(eggRenderers[j].sharedMaterial);
			}
		}
	}

	private void OnEnable()
	{
		eggAnimatorEventListener.EventTriggered += HandleEggAnimatorEvent;
	}

	private void OnDisable()
	{
		eggAnimatorEventListener.EventTriggered -= HandleEggAnimatorEvent;
	}

	private void Start()
	{
		currencyGUI.Init(Player.instance);
	}

	private void _Show(PlayerReward reward)
	{
		curEggCount = 1;
		totalEggCount = 0;
		AddToQueue(reward);
		if (rewardQueue.Count > 0)
		{
			amountText.faceColor = ((rewardQueue[0].riddleEggType != RiddleEggType.Gold) ? greenEggFaceColor : goldenEggFaceColor);
		}
		StartCoroutine(WaitForIntro());
	}

	private void _Show(List<PlayerReward> rewards)
	{
		curEggCount = 1;
		totalEggCount = 0;
		AddToQueue(rewards);
		if (rewardQueue.Count > 0)
		{
			amountText.faceColor = ((rewardQueue[0].riddleEggType != RiddleEggType.Gold) ? greenEggFaceColor : goldenEggFaceColor);
		}
		StartCoroutine(WaitForIntro());
	}

	private void AddToQueue(PlayerReward reward)
	{
		if (reward.type == PlayerRewardType.RiddleEgg)
		{
			rewardQueue.Add(reward);
			totalEggCount++;
			amountText.text = $"{curEggCount}/{totalEggCount}";
		}
	}

	private void AddToQueue(List<PlayerReward> rewards)
	{
		for (int i = 0; i < rewards.Count; i++)
		{
			AddToQueue(rewards[i]);
		}
	}

	private IEnumerator WaitForIntro()
	{
		yield return new WaitWhile(() => !mainAnimator.IsName("loop"));
		StartCoroutine(WaitForRewards());
	}

	private IEnumerator WaitForRewards()
	{
		while (rewardQueue.Count > 0)
		{
			yield return StartCoroutine(ShowReward(rewardQueue.Shift()));
			curEggCount++;
		}
		mainAnimator.Play("outro");
		mainAnimator.Update(0f);
		yield return new WaitWhile(() => mainAnimator.IsPlaying());
		ObjectPool.PoolObject(gameObject);
	}

	private IEnumerator ShowReward(PlayerReward reward)
	{
		amountText.text = $"{curEggCount}/{totalEggCount}";
		amountText.faceColor = ((reward.riddleEggType != RiddleEggType.Gold) ? greenEggFaceColor : goldenEggFaceColor);
		Texture eggTexture = ((reward.riddleEggType != RiddleEggType.Gold) ? greenEggTex : goldenEggTex);
		for (int i = 0; i < eggMaterials.Count; i++)
		{
			eggMaterials[i].mainTexture = eggTexture;
		}
		eggAnimator.gameObject.SetActive(value: true);
		eggAnimator.Update(0f);
		GFAudioManager.instance.PlayClip(showEggClip, showEggVolume, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
		AudioSource tapAndHoldSound = null;
		while (true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				eggAnimator.Play("tap hold");
				tapAndHoldSound = GFAudioManager.instance.PlayClip(holdToBreakClip, holdToBreakVolume, loop: false, 0f, 128, GFAudioManager.SFX_GROUP, ignoreLastPlayTime: true);
			}
			else if (Input.GetMouseButton(0))
			{
				if (!eggAnimator.IsPlaying())
				{
					break;
				}
			}
			else if (!eggAnimator.IsNamed("intro", "idle"))
			{
				eggAnimator.Play("idle");
				if (tapAndHoldSound != null)
				{
					AudioSource tempSource = tapAndHoldSound;
					tempSource.DOFade(0f, 0.15f).OnComplete(() =>
					{
						tempSource.Stop();
					});
					tapAndHoldSound = null;
				}
			}
			yield return null;
		}
		eggAnimator.Play("open");
		spawningQMarks.Clear();
		float startX = spacing * (float)(questionMarks.Length - 1) / -2f;
		for (int i2 = 0; i2 < questionMarks.Length; i2++)
		{
			StartCoroutine(SpawnQuestionMark(questionMarks[i2], new Vector3(startX, -50f, 587.5f), questionMarkPositions[i2], reward.eggRewards[i2]));
			spawningQMarks.Add(questionMarks[i2]);
			startX += spacing;
			yield return new WaitForSeconds(1f / 12f);
		}
		yield return new WaitWhile(() => spawningQMarks.Count > 0);
		for (int num = 0; num < questionMarks.Length; num++)
		{
			questionMarks[num].uiItem.OnDownUIItem += HandleOnQuestionMarkDown;
		}
		int len = questionMarks.Length;
		bool allRevealed;
		do
		{
			yield return null;
			allRevealed = true;
			for (int num2 = 0; num2 < len; num2++)
			{
				if (!questionMarks[num2].isRevealed)
				{
					allRevealed = false;
					break;
				}
			}
		}
		while (!allRevealed);
		continueButton.gameObject.SetActive(value: true);
		yield return new tk2dWaitForOnClick(continueButton);
		continueButton.gameObject.SetActive(value: false);
		for (int num3 = 0; num3 < len; num3++)
		{
			questionMarks[num3].animator.Play("qmark_outro_reward");
		}
		yield return new WaitForSeconds(0.2f);
		for (int num4 = 0; num4 < len; num4++)
		{
			questionMarks[num4].ResetReward();
		}
	}

	private void HandleOnQuestionMarkDown(tk2dUIItem uiItem)
	{
		uiItem.OnDownUIItem -= HandleOnQuestionMarkDown;
		RiddleEggRevealQMark component = uiItem.GetComponent<RiddleEggRevealQMark>();
		component.RevealReward();
		bool flag = true;
		if (component.reward.type == PlayerRewardType.Figure)
		{
			if (Player.instance.HasAcquiredCharacter(component.reward.figure.characterUID))
			{
				GFAudioManager.instance.PlayClip(duplicateFigClip, duplicateFigVolume, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
				CityRewardReveal.Show(component.reward);
				flag = false;
			}
			else
			{
				GFAudioManager.instance.PlayClip(newFigClip, newFigVolume, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
				NewFigureReveal.Show(component.reward);
			}
		}
		else if (component.reward.type == PlayerRewardType.Repaint)
		{
			CityRewardReveal.Show(component.reward);
		}
		if (flag)
		{
			ProcessReward(component.reward);
		}
	}

	private IEnumerator SpawnQuestionMark(RiddleEggRevealQMark questionMark, Vector3 pos1, Vector3 pos2, PlayerReward reward)
	{
		questionMark.Init(reward);
		questionMark.transform.localPosition = new Vector3(0f, -50f, 587.5f);
		questionMark.gameObject.SetActive(value: true);
		questionMark.animator.Update(0f);
		yield return questionMark.transform.DOLocalMove(pos1, 17f / 60f).SetEase(Ease.OutSine).WaitForCompletion();
		yield return new WaitForSeconds(49f / 60f);
		yield return questionMark.transform.DOLocalMove(pos2, 11f / 60f).SetEase(Ease.InSine).WaitForCompletion();
		spawningQMarks.Remove(questionMark);
	}

	private void ProcessReward(PlayerReward reward)
	{
		if (reward == null)
		{
			return;
		}
		switch (reward.type)
		{
		case PlayerRewardType.Currency:
			if (reward.currencyType == CurrencyType.Coins)
			{
				Player.instance.AddCurrency(reward.amount);
			}
			else
			{
				Player.instance.AddRainbowCurrency(reward.amount);
			}
			break;
		case PlayerRewardType.Figure:
		{
			Player.instance.AcquireCharacter(reward.figure.characterUID);
			CharacterData data = CharacterRoller.RollCharacterData(GameManager.instance.characterDatabase.GetCharacter(reward.figure.characterUID), -1, reward.figure.characterUID, reward.figure.characterLvl);
			Player.instance.AddCharacter(data);
			Player.instance.AddToParty(Player.instance.characters.GetLastItem());
			break;
		}
		case PlayerRewardType.Repaint:
			Player.instance.AcquireRepaintSlot(reward.repaintUID);
			Player.instance.AddFreeRepaintSlot(reward.repaintUID);
			break;
		}
	}

	private void HandleEggAnimatorEvent(AnimationEvent evt)
	{
		if (evt.stringParameter == "complete")
		{
			eggAnimator.gameObject.SetActive(value: false);
		}
	}

	public static void Show(PlayerReward reward)
	{
		if (instance == null)
		{
			if (reward.type == PlayerRewardType.RiddleEgg)
			{
				instance = ObjectPool.GetObjectFromResource<RiddleEggReveal>("Prefabs/GUI/iap_Egg_Reveal", new Vector3(4000f, 0f, 0f));
				instance._Show(reward);
			}
			else
			{
				Debug.Log("The reward needs to be of the Riddle Egg type for this reveal screen.");
			}
		}
		else if (reward.type == PlayerRewardType.RiddleEgg)
		{
			Debug.Log("An instance of RiddleEggReveal is already open, so adding the reward to the queue");
			instance.AddToQueue(reward);
		}
		else
		{
			Debug.Log("The reward needs to be of the Riddle Egg type for this reveal screen.");
		}
	}

	public static void Show(List<PlayerReward> rewards)
	{
		if (instance == null)
		{
			instance = ObjectPool.GetObjectFromResource<RiddleEggReveal>("Prefabs/GUI/iap_Egg_Reveal", new Vector3(4000f, 0f, 0f));
			instance._Show(rewards);
		}
		else
		{
			instance.AddToQueue(rewards);
		}
	}

	public void ResetObject()
	{
		instance = null;
	}
}
