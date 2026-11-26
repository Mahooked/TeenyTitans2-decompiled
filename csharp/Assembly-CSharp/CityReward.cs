using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CityReward : MonoBehaviour
{
	public static CityReward instance;

	public TextMeshPro headerTextMesh;

	public tk2dSprite iconSprite;

	public TextMeshPro rewardName;

	public TextMeshPro rewardLvl;

	public Animator animator;

	public GameObject nextBtn;

	public GameObject titanTowerBtn;

	public Transform newFigureRevealScreenParent;

	private GameObject _newFigureRevealScreen;

	public ModChipDescriptionBox modchipReveal;

	public AccessoryReveal accessoryReveal;

	public MissionObjectGUI missionObjectGui;

	public GameObject newFigureRevealNxtBtn;

	public ModChipSelectPortrait modchipPortrait;

	public GameObject newFigureGroup;

	public GameObject newAccessoryGroup;

	public RepaintReveal repaintReveal;

	public GameObject currencyGroup;

	public TextMeshPro accessoryName;

	public Transform accessoryModelHolder;

	public TextMeshPro currencyText;

	public bool showingRewards;

	public bool isChallengeReward;

	private bool waitForInput;

	private bool waitForNxtBtnInput;

	private Mission mission;

	private List<CityRewardData> rewardsQueue = new List<CityRewardData>();

	private List<ShopFigure> newFiguresToReveal = new List<ShopFigure>();

	private GameObject accessoryModelObj;

	private bool playedOutro;

	private bool startedExit;

	private GameObject newFigureRevealScreen
	{
		get
		{
			if (_newFigureRevealScreen == null)
			{
				_newFigureRevealScreen = Object.Instantiate(Resources.Load("Prefabs/Shop/NewFigureRevealScreen") as GameObject);
				_newFigureRevealScreen.transform.parent = newFigureRevealScreenParent;
				_newFigureRevealScreen.transform.localPosition = Vector3.zero;
				_newFigureRevealScreen.transform.localScale = Vector3.one;
				_newFigureRevealScreen.transform.localEulerAngles = Vector3.zero;
			}
			return _newFigureRevealScreen;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		animator.gameObject.SetActive(value: false);
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus && newFigureRevealScreen.activeSelf)
		{
			PauseManager.instance.Resume();
		}
	}

	public IEnumerator ShowPlayerReward(PlayerReward reward)
	{
		switch (reward.type)
		{
		case PlayerRewardType.Currency:
			animator.gameObject.SetActive(value: true);
			newFigureGroup.gameObject.SetActive(value: false);
			newAccessoryGroup.gameObject.SetActive(value: false);
			headerTextMesh.text = Language.Get("YOUGOT", "MENUS,GUI");
			modchipPortrait.gameObject.SetActive(value: false);
			iconSprite.gameObject.SetActive(value: false);
			currencyGroup.SetActive(value: true);
			if (reward.currencyType == CurrencyType.Coins)
			{
				currencyText.text = "<sprite name=\"icon_currency_coin\">" + reward.amount + "\n" + Language.Get("LOOSECHANGE", "MENUS,GUI");
				Player.instance.AddCurrency(reward.amount);
			}
			else
			{
				currencyText.text = "<sprite name=\"icon_currency_rainbowCoin\">" + reward.amount + "\n" + Language.Get((reward.amount != 1) ? "REPAINT_TOKENS" : "REPAINT_TOKENS_SINGULAR", "MENUS,GUI");
				Player.instance.AddRainbowCurrency(reward.amount);
			}
			animator.Play("CityReward_Figure1_Intro", 0, 0f);
			break;
		case PlayerRewardType.Item:
		{
			ItemDefinition itemDef = GameManager.instance.itemDatabase.GetItem(reward.itemUID);
			if (itemDef is ModChipDefinition)
			{
				ShowModChipReward(itemDef.uid);
				yield return new WaitWhile(() => rewardsQueue.Count > 0);
			}
			else if (itemDef is AccessoryItemDefinition)
			{
				ShowAccessoryReward(itemDef.uid);
				yield return new WaitWhile(() => rewardsQueue.Count > 0);
			}
			else if (itemDef is QuestItemDefinition)
			{
				yield return StartCoroutine(missionObjectGui.Show(itemDef, reward.amount));
			}
			break;
		}
		case PlayerRewardType.Figure:
		{
			CharacterRewardData rewardData = new CharacterRewardData
			{
				characterUID = reward.figure.characterUID,
				characterLvl = reward.figure.characterLvl,
				blindBox = reward.figure.blindBox
			};
			ShowFigureReward(new List<CharacterRewardData> { rewardData });
			yield return new WaitWhile(() => rewardsQueue.Count > 0);
			break;
		}
		}
	}

	public void ShowRewardForMission(int missionUID)
	{
		mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		rewardsQueue.Clear();
		playedOutro = false;
		if (!(mission != null))
		{
			return;
		}
		if (mission.coinReward > 0)
		{
			rewardsQueue.Add(new CityRewardData(mission.coinReward, -1, -1, -1));
		}
		if (mission.rainbowCoinReward > 0)
		{
			rewardsQueue.Add(new CityRewardData(0, -1, -1, -1, isBlindBox: false, isGoldBox: false, mission.rainbowCoinReward));
		}
		if (mission.characterRewards.Count > 0)
		{
			for (int i = 0; i < mission.characterRewards.Count; i++)
			{
				rewardsQueue.Add(new CityRewardData(0, mission.characterRewards[i].characterUID, Mathf.Max(1, mission.characterRewards[i].characterLvl), -1));
			}
		}
		if (mission.modChipReward > -1)
		{
			rewardsQueue.Add(new CityRewardData(0, -1, 0, mission.modChipReward));
		}
		if (mission.accessoryReward > -1)
		{
			rewardsQueue.Add(new CityRewardData(mission.accessoryReward));
		}
		if (mission.questItemReward > -1)
		{
			rewardsQueue.Add(new CityRewardData(mission.questItemReward, isQuestItem: true));
		}
		if (mission.repaintReward != null && (mission.repaintReward.isRepaintRandom || (mission.repaintReward.repaintUID > -1 && mission.repaintReward.characterUID > -1)))
		{
			PlayerReward playerReward = ((!mission.repaintReward.isRepaintRandom) ? mission.repaintReward : PlayerRewardFactory.GetRandomRepaintReward(mission.repaintReward.isRepaintRare, 0.15f));
			rewardsQueue.Add(new CityRewardData(0, playerReward.characterUID, 0, -1, isBlindBox: false, isGoldBox: false, 0, playerReward.repaintUID));
		}
		if (mission.riddleEggReward != null && mission.riddleEggReward.amount > 0)
		{
			List<PlayerReward> list = new List<PlayerReward>();
			for (int j = 0; j < mission.riddleEggReward.amount; j++)
			{
				list.Add(GameManager.instance.iapConfig.GenerateRiddleEggReward(mission.riddleEggReward.riddleEggType));
			}
			rewardsQueue.Add(new CityRewardData(list));
		}
		if (rewardsQueue.Count > 0)
		{
			showingRewards = true;
			StartCoroutine("StartRewardsQueue");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_achievement", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public void ShowRewardWithRewards(List<CityRewardData> rewards)
	{
		rewardsQueue.Clear();
		playedOutro = false;
		rewardsQueue = rewards;
		if (rewardsQueue.Count > 0)
		{
			showingRewards = true;
			StartCoroutine("StartRewardsQueue");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_achievement", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public void ShowFigureReward(List<CharacterRewardData> characterRewards)
	{
		rewardsQueue.Clear();
		playedOutro = false;
		if (characterRewards.Count > 0)
		{
			for (int i = 0; i < characterRewards.Count; i++)
			{
				int characterUID = characterRewards[i].characterUID;
				int b = characterRewards[i].characterLvl;
				bool blindBox = characterRewards[i].blindBox;
				bool goldBox = characterRewards[i].goldBox;
				if (blindBox && characterRewards[i].blindBoxShopFigures.Count > 0)
				{
					int index = Random.Range(0, characterRewards[i].blindBoxShopFigures.Count);
					characterUID = characterRewards[i].blindBoxShopFigures[index].characterUID;
					b = characterRewards[i].blindBoxShopFigures[index].level;
				}
				rewardsQueue.Add(new CityRewardData(0, characterUID, Mathf.Max(1, b), -1, blindBox, goldBox));
			}
		}
		if (rewardsQueue.Count > 0)
		{
			showingRewards = true;
			StartCoroutine("StartRewardsQueue");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_achievement", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public void ShowModChipReward(int modchipUID)
	{
		playedOutro = false;
		rewardsQueue.Clear();
		rewardsQueue.Add(new CityRewardData(0, -1, 0, modchipUID));
		if (rewardsQueue.Count > 0)
		{
			showingRewards = true;
			StartCoroutine("StartRewardsQueue");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_achievement", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public void ShowAccessoryReward(int accessoryUID)
	{
		playedOutro = false;
		rewardsQueue.Clear();
		rewardsQueue.Add(new CityRewardData(accessoryUID));
		showingRewards = true;
		StartCoroutine("StartRewardsQueue");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_achievement", GFAudioManager.SFX_GROUP, string.Empty);
	}

	public void ShowRepaintReward(int characterUID, int repaintUID)
	{
		playedOutro = false;
		rewardsQueue.Clear();
		rewardsQueue.Add(new CityRewardData(0, characterUID, 0, -1, isBlindBox: false, isGoldBox: false, 0, repaintUID));
		if (rewardsQueue.Count > 0)
		{
			showingRewards = true;
			StartCoroutine("StartRewardsQueue");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_achievement", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private IEnumerator StartRewardsQueue()
	{
		yield return new WaitForEndOfFrame();
		if (!isChallengeReward)
		{
			GUICamera.instance.gameObject.SetActive(value: false);
			CityController.instance.communicator.gameObject.SetActive(value: false);
			GameUI.instance.HideMySquad();
		}
		animator.gameObject.SetActive(value: true);
		newFigureGroup.gameObject.SetActive(value: true);
		newAccessoryGroup.gameObject.SetActive(value: false);
		currencyGroup.SetActive(value: false);
		headerTextMesh.text = Language.Get("YOUGOT", "MENUS,GUI");
		if (accessoryModelObj != null)
		{
			ObjectPool.PoolObject(accessoryModelObj);
			accessoryModelObj = null;
		}
		if (modchipReveal != null)
		{
			modchipReveal.gameObject.SetActive(value: false);
		}
		if (accessoryReveal != null)
		{
			accessoryReveal.gameObject.SetActive(value: false);
		}
		if (repaintReveal != null)
		{
			repaintReveal.gameObject.SetActive(value: false);
		}
		bool wasShowingModchip = false;
		if (rewardsQueue.Count > 0)
		{
			CityRewardData rewardData = rewardsQueue[0];
			if (rewardData.coinAmount > 0)
			{
				Debug.Log("Coin reward");
				newFigureGroup.SetActive(value: false);
				modchipPortrait.gameObject.SetActive(value: false);
				currencyGroup.SetActive(value: true);
				currencyText.text = "<sprite name=\"icon_currency_coin\">" + rewardData.coinAmount + "\n" + Language.Get("LOOSECHANGE", "MENUS,GUI");
				Player.instance.AddCurrency(rewardData.coinAmount);
				animator.Play("CityReward_Figure1_Intro", 0, 0f);
			}
			else if (rewardData.rainbowCoinAmount > 0)
			{
				Debug.Log("Rainbow Coin reward");
				newFigureGroup.SetActive(value: false);
				modchipPortrait.gameObject.SetActive(value: false);
				currencyGroup.SetActive(value: true);
				currencyText.text = "<sprite name=\"icon_currency_rainbowCoin\">" + rewardData.rainbowCoinAmount + "\n" + Language.Get((rewardData.rainbowCoinAmount != 1) ? "REPAINT_TOKENS" : "REPAINT_TOKENS_SINGULAR", "MENUS,GUI");
				Player.instance.AddRainbowCurrency(rewardData.rainbowCoinAmount);
				animator.Play("CityReward_Figure1_Intro", 0, 0f);
			}
			else if (rewardData.repaintUID > -1)
			{
				Debug.Log("Repaint Reward");
				animator.gameObject.SetActive(value: false);
				newFigureGroup.gameObject.SetActive(value: false);
				currencyGroup.SetActive(value: false);
				CharacterDefinition charDef = GameManager.instance.characterDatabase.GetCharacter(rewardData.characterUID);
				if (charDef != null)
				{
					RepaintColorSlot colorSlot = charDef.GetRepaintColorSlotForID(rewardData.repaintUID);
					if (colorSlot != null)
					{
						wasShowingModchip = true;
						Player.instance.AddFreeRepaintSlot(rewardData.repaintUID);
						repaintReveal.gameObject.SetActive(value: true);
						repaintReveal.Init(charDef, colorSlot);
						GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_repaintshop_purchase", GFAudioManager.SFX_GROUP, string.Empty);
						yield return new WaitForTap();
						repaintReveal.Init(null, null);
						repaintReveal.gameObject.SetActive(value: false);
					}
				}
			}
			else if (rewardData.characterUID > -1)
			{
				Debug.Log("Character reward");
				CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(rewardData.characterUID);
				modchipPortrait.gameObject.SetActive(value: false);
				iconSprite.gameObject.SetActive(value: true);
				iconSprite.SetSprite(character.portraitSprite);
				rewardName.text = ((!string.IsNullOrEmpty(character.characterNameKey)) ? Language.Get(character.characterNameKey, "NPCS,FIGS") : character.characterName);
				rewardLvl.text = Language.Get("LVL", "MENUS,GUI") + " " + rewardData.characterLvl;
				bool isNewFigure = !Player.instance.HasAcquiredCharacter(rewardData.characterUID);
				Player.instance.AddCharacter(CharacterRoller.RollCharacterData(character, -1, character.uid, rewardData.characterLvl));
				Player.instance.AddToParty(Player.instance.characters.GetLastItem());
				if (isNewFigure || rewardData.blindBox)
				{
					ShopFigure figureData = new ShopFigure
					{
						level = rewardData.characterLvl,
						characterUID = rewardData.characterUID
					};
					newFiguresToReveal.Add(figureData);
					animator.gameObject.SetActive(value: false);
					nextBtn.SetActive(value: false);
					titanTowerBtn.SetActive(value: false);
					newFigureRevealNxtBtn.SetActive(value: false);
					yield return StartCoroutine(RevealNextFigure(rewardData.blindBox, rewardData.goldBox));
					newFigureRevealNxtBtn.SetActive(value: true);
					waitForNxtBtnInput = true;
					while (waitForNxtBtnInput)
					{
						yield return null;
					}
					animator.gameObject.SetActive(value: true);
					animator.Play("CityReward_Figure1_Intro", 0, 0f);
					nextBtn.SetActive(value: true);
				}
				else
				{
					animator.Play("CityReward_Figure1_Intro", 0, 0f);
				}
			}
			else if (rewardData.chipUID > -1)
			{
				Debug.Log("Chip reward");
				animator.gameObject.SetActive(value: false);
				ItemDefinition modchipDef = GameManager.instance.itemDatabase.GetItem(rewardData.chipUID);
				if (modchipDef != null)
				{
					wasShowingModchip = true;
					modchipReveal.gameObject.SetActive(value: true);
					modchipReveal.Init((ModChipDefinition)modchipDef);
					modchipReveal.animator.Play("ModChip_Reveal_Intro", 0, 0f);
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_modchip_reveal", GFAudioManager.SFX_GROUP, string.Empty);
					yield return new WaitForSeconds(1f);
					yield return new WaitForTap();
					modchipReveal.animator.Play("ModChip_Reveal_Outro", 0, 0f);
					yield return new WaitForEndOfFrame();
					while (modchipReveal.animator.NormalizedTime() < 0.9f)
					{
						yield return new WaitForEndOfFrame();
					}
					yield return new WaitForEndOfFrame();
					Player.instance.AddItem(modchipDef);
					modchipReveal.gameObject.SetActive(value: false);
				}
				else
				{
					Debug.Log("null chip def");
				}
			}
			else if (rewardData.accessoryUID > -1)
			{
				animator.gameObject.SetActive(value: false);
				AccessoryItemDefinition accessoryItemDefinition = GameManager.instance.itemDatabase.GetItem(rewardData.accessoryUID) as AccessoryItemDefinition;
				if (accessoryItemDefinition != null)
				{
					wasShowingModchip = true;
					accessoryReveal.gameObject.SetActive(value: true);
					accessoryReveal.Init(accessoryItemDefinition);
					accessoryReveal.animator.Play("ModChip_Reveal_Intro", 0, 0f);
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_modchip_reveal", GFAudioManager.SFX_GROUP, string.Empty);
					yield return new WaitForSeconds(1f);
					yield return new WaitForTap();
					accessoryReveal.animator.Play("ModChip_Reveal_Outro", 0, 0f);
					yield return new WaitForEndOfFrame();
					while (accessoryReveal.animator.NormalizedTime() < 0.9f)
					{
						yield return new WaitForEndOfFrame();
					}
					yield return new WaitForEndOfFrame();
					Player.instance.AddAccessory(accessoryItemDefinition.uid);
					Player.instance.AddToEquippedAccessories(accessoryItemDefinition.uid);
					accessoryReveal.Init(null);
				}
			}
			else if (rewardData.questItemUID > -1)
			{
				if (animator.gameObject.activeSelf)
				{
					animator.Play("CityReward_Figure1_Outro");
				}
				yield return StartCoroutine(missionObjectGui.Show(GameManager.instance.itemDatabase.GetItem(rewardData.questItemUID)));
				wasShowingModchip = true;
			}
			else if (rewardData.eggRewards != null && rewardData.eggRewards.Count > 0)
			{
				if (animator.gameObject.activeSelf)
				{
					animator.Play("CityReward_Figure1_Outro");
				}
				RiddleEggReveal.Show(rewardData.eggRewards);
				yield return new WaitWhile(() => RiddleEggReveal.Instance != null);
				wasShowingModchip = true;
			}
			if (rewardsQueue.Count > 0)
			{
				rewardsQueue.RemoveAt(0);
				rewardsQueue.TrimExcess();
			}
			Debug.Log("rewardsQueue.Count " + rewardsQueue.Count + " wasShowingModchip: " + wasShowingModchip);
			if (wasShowingModchip)
			{
				OnClickPlay();
			}
		}
		yield return new WaitForEndOfFrame();
	}

	private ShopFigure NextFigureToReveal()
	{
		if (newFiguresToReveal.Count > 0)
		{
			return newFiguresToReveal[0];
		}
		return null;
	}

	private IEnumerator RevealNextFigure(bool isBlindBox, bool isGoldBox = false)
	{
		ShopFigureReveal figureReveal = newFigureRevealScreen.GetComponent<ShopFigureReveal>();
		bool firstReveal = !figureReveal.gameObject.activeSelf;
		if (!firstReveal)
		{
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
			yield return new WaitForEndOfFrame();
			while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		figureReveal.figureRevealBoxGroupAnimator.Play("empty");
		if (isBlindBox)
		{
			figureReveal.newFigureBacking.SetActive(value: false);
			figureReveal.isGoldBox = isGoldBox;
			figureReveal.MakeBlindBox();
		}
		else
		{
			figureReveal.newFigureBacking.SetActive(value: true);
			figureReveal.RemoveBlindBox();
		}
		ShopFigure figureData = NextFigureToReveal();
		if (figureData == null)
		{
			yield break;
		}
		if (newFiguresToReveal.Count > 0)
		{
			newFiguresToReveal.RemoveAt(0);
		}
		CharacterDefinition currentRevealCharacterDef = GameManager.instance.characterDatabase.GetCharacter(figureData.characterUID);
		bool isNewFigure = !Player.instance.HasAcquiredCharacter(currentRevealCharacterDef);
		figureReveal.figureRevealToyBox.figureData = figureData;
		figureReveal.figureRevealToyBox.UpdateBoxForCharacter(currentRevealCharacterDef, newFigureReveal: true);
		Player.instance.AcquireCharacter(figureData.characterUID);
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.SetActive(value: false);
		}
		yield return new WaitForSeconds(0.2f);
		if (firstReveal)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_firstfigappear", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_subsequentfigappear", GFAudioManager.SFX_GROUP, string.Empty);
		}
		figureReveal.gameObject.SetActive(value: true);
		figureReveal.figuresDiscoveredAmt.transform.parent.gameObject.SetActive(isNewFigure);
		figureReveal.figuresDiscoveredAmt.text = $"<color=\"orange\">{Mathf.Max(0, Player.instance.charactersAcquired.Count - 1)}<color=\"white\">/{GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance)}";
		figureReveal.figuredRevealName.text = ((!string.IsNullOrEmpty(currentRevealCharacterDef.characterNameKey)) ? Language.Get(currentRevealCharacterDef.characterNameKey, "NPCS,FIGS") : currentRevealCharacterDef.characterName);
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.transform.DOKill();
			figureReveal.figureRevealToyBox.figure.transform.localEulerAngles = new Vector3(currentRevealCharacterDef.shopFigureRotationOffset.x, 180f + currentRevealCharacterDef.shopFigureRotationOffset.y, currentRevealCharacterDef.shopFigureRotationOffset.z);
		}
		if (!firstReveal)
		{
			while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_Outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
			{
				yield return new WaitForEndOfFrame();
			}
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Intro");
		}
		else
		{
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Intro");
			figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Intro");
		}
		yield return new WaitForSeconds(0.1f);
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.SetActive(value: true);
		}
		figureReveal.waitToOpenBox = true;
		float lastTimeWigglePlayed = Time.time;
		while (figureReveal.waitToOpenBox)
		{
			yield return new WaitForEndOfFrame();
		}
		figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Tap");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_open", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(1.6f);
		if (!string.IsNullOrEmpty(currentRevealCharacterDef.shopRevealNameSoundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(currentRevealCharacterDef.shopRevealNameSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		}
		yield return new WaitForSeconds(0.6f);
		figureReveal.figuresDiscoveredAmt.text = $"<color=\"orange\">{Player.instance.charactersAcquired.Count}<color=\"white\">/{GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance)}";
		figureReveal.figureRevealToyBox.figure.transform.DOLocalRotate(new Vector3(0f, 900f, 0f), 25f, RotateMode.FastBeyond360);
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			waitForInput = false;
		}
	}

	private void OnClickPlay()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		Debug.Log("click play");
		if (rewardsQueue.Count > 0)
		{
			StartCoroutine("StartRewardsQueue");
		}
		else
		{
			StartCoroutine("PlayOutro");
		}
	}

	private IEnumerator PlayOutro()
	{
		if (playedOutro)
		{
			yield break;
		}
		playedOutro = true;
		if (mission != null)
		{
			Player.instance.CollectMission(mission.uid);
		}
		bool savingPreviouslyEnabled = Player.instance.isSavingEnabled;
		Player.instance.isSavingEnabled = true;
		Player.instance.Save();
		Player.instance.isSavingEnabled = savingPreviouslyEnabled;
		if (animator.gameObject.activeSelf)
		{
			animator.Play("CityReward_Figure1_Outro");
			yield return new WaitForEndOfFrame();
			while (animator.NormalizedTime() < 0.8f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		animator.gameObject.SetActive(value: false);
		if (accessoryModelObj != null)
		{
			ObjectPool.PoolObject(accessoryModelObj);
			accessoryModelObj = null;
		}
		if (!isChallengeReward)
		{
			GUICamera.instance.gameObject.SetActive(value: true);
			GameUI.instance.ShowMySquad();
			CityController.instance.communicator.gameObject.SetActive(value: true);
			CityController.instance.communicator.ShowCurrentTicker();
			CityController.instance.communicator.UpdateMissions();
			CityController.instance.communicator.CheckForNewMissions();
			CityController.instance.communicator.CheckCollectedMissionNewsFlashes();
		}
		showingRewards = false;
	}

	private void OnClickNewFigureNextBtn()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		waitForNxtBtnInput = false;
		waitForInput = false;
		newFigureRevealNxtBtn.SetActive(value: false);
		if (newFiguresToReveal.Count <= 0)
		{
			StartCoroutine("StartOutro");
		}
	}

	private IEnumerator StartOutro()
	{
		if (mission != null)
		{
			Player.instance.CollectMission(mission.uid);
		}
		bool savingPreviouslyEnabled = Player.instance.isSavingEnabled;
		Player.instance.isSavingEnabled = true;
		Player.instance.Save();
		Player.instance.isSavingEnabled = savingPreviouslyEnabled;
		newFigureRevealNxtBtn.SetActive(value: false);
		ShopFigureReveal figureReveal = newFigureRevealScreen.GetComponent<ShopFigureReveal>();
		figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro", 0, 0f);
		figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Outro", 0, 0f);
		yield return new WaitForEndOfFrame();
		while (figureReveal.figureRevealScreenAnimator.IsPlaying())
		{
			yield return new WaitForEndOfFrame();
		}
		newFigureRevealScreen.SetActive(value: false);
	}

	private void OnClickTower()
	{
		if (newFiguresToReveal.Count <= 0 && !startedExit)
		{
			if (mission != null)
			{
				Player.instance.CollectMission(mission.uid);
			}
			startedExit = true;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
			if (!isChallengeReward)
			{
				CityController.prevPosition = CityController.instance.cityCharacter.transform.position;
				CityController.prevCameraAngleIndex = CityController.instance.curCameraAngleIndex;
				CityController.usePrevPosition = true;
				CityController.instance.communicator.SaveDayAndTime();
			}
			else
			{
				TitanTowerController.fromChallengeScene = true;
			}
			LoadingScreenCover.LoadScene("TitanTower");
			showingRewards = false;
		}
	}
}
