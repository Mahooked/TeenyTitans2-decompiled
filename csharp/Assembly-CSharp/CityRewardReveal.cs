using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityRewardReveal : MonoBehaviour, IPoolable
{
	public Animator animator;

	public TextMeshPro headerTextMesh;

	public tk2dSprite iconSprite;

	public TextMeshPro rewardName;

	public TextMeshPro rewardLvl;

	public tk2dUIItem nextBtn;

	public ModChipSelectPortrait modchipPortrait;

	public GameObject newFigureGroup;

	public GameObject newAccessoryGroup;

	public RepaintReveal repaintReveal;

	public GameObject currencyGroup;

	public TextMeshPro accessoryName;

	public Transform accessoryModelHolder;

	public TextMeshPro currencyText;

	private List<PlayerReward> rewardQueue = new List<PlayerReward>();

	private static CityRewardReveal instance;

	public static CityRewardReveal Instance => instance;

	private void Awake()
	{
		animator.gameObject.SetActive(value: false);
	}

	private void _Show(PlayerReward reward)
	{
		AddToQueue(reward);
		StartCoroutine(WaitForIntro());
	}

	private void _Show(List<PlayerReward> rewards)
	{
		AddToQueue(rewards);
		StartCoroutine(WaitForIntro());
	}

	private void _Show(int missionUid)
	{
		AddToQueue(missionUid);
		StartCoroutine(WaitForIntro());
	}

	private void AddToQueue(PlayerReward reward)
	{
		if (reward.type == PlayerRewardType.Figure || reward.type == PlayerRewardType.Currency || reward.type == PlayerRewardType.Repaint)
		{
			rewardQueue.Add(reward);
		}
	}

	private void AddToQueue(List<PlayerReward> rewards)
	{
		for (int i = 0; i < rewards.Count; i++)
		{
			AddToQueue(rewards[i]);
		}
	}

	private void AddToQueue(int missionUid)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUid);
		if (!(mission != null))
		{
			return;
		}
		List<PlayerReward> list = new List<PlayerReward>();
		if (mission.coinReward > 0)
		{
			PlayerReward playerReward = new PlayerReward();
			playerReward.type = PlayerRewardType.Currency;
			playerReward.amount = mission.coinReward;
			list.Add(playerReward);
		}
		if (mission.rainbowCoinReward > 0)
		{
			PlayerReward playerReward2 = new PlayerReward();
			playerReward2.type = PlayerRewardType.Currency;
			playerReward2.amount = mission.rainbowCoinReward;
			list.Add(playerReward2);
		}
		if (mission.characterRewards.Count > 0)
		{
			for (int i = 0; i < mission.characterRewards.Count; i++)
			{
				PlayerReward playerReward3 = new PlayerReward();
				playerReward3.type = PlayerRewardType.Figure;
				playerReward3.figure = mission.characterRewards[i].Copy();
				list.Add(playerReward3);
			}
		}
		if (mission.repaintReward != null && (mission.repaintReward.isRepaintRandom || (mission.repaintReward.repaintUID > -1 && mission.repaintReward.characterUID > -1)))
		{
			PlayerReward item = ((!mission.repaintReward.isRepaintRandom) ? mission.repaintReward : PlayerRewardFactory.GetRandomRepaintReward(mission.repaintReward.isRepaintRare, 0.15f));
			list.Add(item);
		}
		AddToQueue(list);
	}

	private IEnumerator WaitForIntro()
	{
		StartCoroutine(WaitForRewards());
		yield break;
	}

	private IEnumerator WaitForRewards()
	{
		while (rewardQueue.Count > 0)
		{
			yield return StartCoroutine(ShowReward(rewardQueue.Shift()));
		}
		ObjectPool.PoolObject(gameObject);
	}

	private IEnumerator ShowReward(PlayerReward reward)
	{
		switch (reward.type)
		{
		case PlayerRewardType.Currency:
			yield return StartCoroutine(ShowCurrencyReward(reward));
			break;
		case PlayerRewardType.Figure:
			yield return StartCoroutine(ShowFigureReward(reward));
			break;
		case PlayerRewardType.Repaint:
			yield return StartCoroutine(ShowRepaintReward(reward));
			break;
		}
	}

	private IEnumerator ShowCurrencyReward(PlayerReward reward)
	{
		animator.gameObject.SetActive(value: true);
		newFigureGroup.SetActive(value: false);
		newAccessoryGroup.SetActive(value: false);
		modchipPortrait.gameObject.SetActive(value: false);
		iconSprite.gameObject.SetActive(value: false);
		repaintReveal.gameObject.SetActive(value: false);
		currencyGroup.SetActive(value: true);
		headerTextMesh.text = Language.Get("YOUGOT", "MENUS,GUI");
		switch (reward.currencyType)
		{
		case CurrencyType.Coins:
			currencyText.text = "<sprite name=\"icon_currency_coin\">" + reward.amount + "\n" + Language.Get("LOOSECHANGE", "MENUS,GUI");
			Player.instance.AddCurrency(reward.amount);
			break;
		case CurrencyType.RainbowCoins:
			currencyText.text = "<sprite name=\"icon_currency_rainbowCoin\">" + reward.amount + "\n" + Language.Get((reward.amount != 1) ? "REPAINT_TOKENS" : "REPAINT_TOKENS_SINGULAR", "MENUS,GUI");
			Player.instance.AddRainbowCurrency(reward.amount);
			break;
		}
		animator.Play("CityReward_Figure1_Intro", 0, 0f);
		yield return new tk2dWaitForOnClick(nextBtn);
		animator.Play("CityReward_Figure1_Outro");
		animator.Update(0f);
		yield return new WaitWhile(() => animator.IsPlaying());
		animator.gameObject.SetActive(value: false);
	}

	private IEnumerator ShowFigureReward(PlayerReward reward)
	{
		if (!Player.instance.HasAcquiredCharacter(reward.figure.characterUID) || reward.figure.blindBox)
		{
			if (!reward.figure.blindBox)
			{
				CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(reward.figure.characterUID);
				Player.instance.AcquireCharacter(character.uid);
				Player.instance.AddCharacter(CharacterRoller.RollCharacterData(character, -1, character.uid, reward.figure.characterLvl));
				Player.instance.AddToParty(Player.instance.characters.GetLastItem());
			}
			NewFigureReveal.Show(reward);
			yield return new WaitUntil(() => NewFigureReveal.Instance == null);
			yield break;
		}
		animator.gameObject.SetActive(value: true);
		newFigureGroup.gameObject.SetActive(value: true);
		newAccessoryGroup.gameObject.SetActive(value: false);
		modchipPortrait.gameObject.SetActive(value: false);
		iconSprite.gameObject.SetActive(value: true);
		repaintReveal.gameObject.SetActive(value: false);
		currencyGroup.SetActive(value: false);
		headerTextMesh.text = Language.Get("YOUGOT", "MENUS,GUI");
		CharacterDefinition character2 = GameManager.instance.characterDatabase.GetCharacter(reward.figure.characterUID);
		iconSprite.SetSprite(character2.portraitSprite);
		rewardName.text = ((!string.IsNullOrEmpty(character2.characterNameKey)) ? Language.Get(character2.characterNameKey, "NPCS,FIGS") : character2.characterName);
		rewardLvl.text = Language.Get("LVL", "MENUS,GUI") + " " + reward.figure.characterLvl;
		Player.instance.AcquireCharacter(character2.uid);
		Player.instance.AddCharacter(CharacterRoller.RollCharacterData(character2, -1, character2.uid, reward.figure.characterLvl));
		Player.instance.AddToParty(Player.instance.characters.GetLastItem());
		animator.Play("CityReward_Figure1_Intro", 0, 0f);
		yield return new tk2dWaitForOnClick(nextBtn);
		animator.Play("CityReward_Figure1_Outro");
		animator.Update(0f);
		yield return new WaitWhile(() => animator.IsPlaying());
		animator.gameObject.SetActive(value: false);
	}

	private IEnumerator ShowItemReward(PlayerReward reward)
	{
		yield break;
	}

	private IEnumerator ShowRepaintReward(PlayerReward reward)
	{
		CharacterDefinition charDef = GameManager.instance.characterDatabase.GetCharacter(reward.characterUID);
		if (!(charDef == null))
		{
			RepaintColorSlot colorSlot = charDef.GetRepaintColorSlotForID(reward.repaintUID);
			if (colorSlot != null)
			{
				animator.gameObject.SetActive(value: false);
				newFigureGroup.gameObject.SetActive(value: false);
				newAccessoryGroup.gameObject.SetActive(value: false);
				modchipPortrait.gameObject.SetActive(value: false);
				iconSprite.gameObject.SetActive(value: false);
				currencyGroup.SetActive(value: false);
				repaintReveal.gameObject.SetActive(value: true);
				repaintReveal.Init(charDef, colorSlot);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_repaintshop_purchase", GFAudioManager.SFX_GROUP, string.Empty);
				yield return new WaitForTap();
				repaintReveal.Init(null, null);
				repaintReveal.gameObject.SetActive(value: false);
			}
		}
	}

	public static void Show(PlayerReward reward)
	{
		if (instance == null)
		{
			if (reward.type == PlayerRewardType.Currency || reward.type == PlayerRewardType.Figure || reward.type == PlayerRewardType.Repaint)
			{
				instance = ObjectPool.GetObjectFromResource<CityRewardReveal>("Prefabs/GUI/CityRewardReveal", new Vector3(5000f, 0f, 0f));
				instance._Show(reward);
			}
			else
			{
				Debug.Log("The reward needs to be of the Currency or Figure type for this reveal screen.");
			}
		}
		else if (reward.type == PlayerRewardType.Currency || reward.type == PlayerRewardType.Figure || reward.type == PlayerRewardType.Repaint)
		{
			Debug.Log("An instance of CityRewardReveal is already open, so adding the reward to the queue");
			instance.AddToQueue(reward);
		}
		else
		{
			Debug.Log("The reward needs to be of the Currency or Figure type for this reveal screen.");
		}
	}

	public static void Show(List<PlayerReward> rewards)
	{
		if (instance == null)
		{
			instance = ObjectPool.GetObjectFromResource<CityRewardReveal>("Prefabs/GUI/CityRewardReveal", new Vector3(5000f, 0f, 0f));
			instance._Show(rewards);
		}
		else
		{
			instance.AddToQueue(rewards);
		}
	}

	public static void Show(int missionUid)
	{
		if (instance == null)
		{
			instance = ObjectPool.GetObjectFromResource<CityRewardReveal>("Prefabs/GUI/CityRewardReveal", new Vector3(5000f, 0f, 0f));
			instance._Show(missionUid);
		}
		else
		{
			instance.AddToQueue(missionUid);
		}
	}

	public void ResetObject()
	{
		instance = null;
	}
}
