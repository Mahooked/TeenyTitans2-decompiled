using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CommunicatorMission : MonoBehaviour
{
	public Animator animator;

	public bool storyMission;

	public TextMeshPro description;

	public tk2dSprite rewardCitySprite;

	public tk2dSprite rewardCityPlusSprite;

	public TextMeshPro rewardMoneyAmnt;

	public TextMeshPro rewardMoneyPlusAmnt;

	public TextMeshPro rewardCityAll;

	public TextMeshPro rewardCityPlusAll;

	public tk2dSprite icon;

	public tk2dSprite pinbox;

	public tk2dSprite portrait;

	public tk2dSprite rewardSprite;

	public GameObject completeCheckMark;

	public GameObject newBanner;

	public GameObject rewardMoney;

	public GameObject rewardMoneyPlus;

	public GameObject rewardModChip;

	public GameObject collectButton;

	public GameObject hintButton;

	public GameObject collect1;

	public GameObject collect2;

	public GameObject collect3;

	public GameObject collect4;

	public GameObject collect5;

	public GameObject versus;

	public TextMeshPro numberNeeded;

	public TextMeshPro numberCompleted;

	public TextMeshPro versusText;

	public TextMeshPro versusText2;

	public TextMeshPro timerText;

	private tk2dUIItem _hintButtonUIItem;

	private tk2dSprite _hintButtonSprite;

	private Collider2D _hintButtonCollider;

	private bool preventClick;

	private int uid = -1;

	private tk2dUIItem hintButtonUIItem
	{
		get
		{
			if (_hintButtonUIItem == null)
			{
				_hintButtonUIItem = hintButton.GetComponent<tk2dUIItem>();
			}
			return _hintButtonUIItem;
		}
	}

	private tk2dSprite hintButtonSprite
	{
		get
		{
			if (_hintButtonSprite == null)
			{
				_hintButtonSprite = hintButton.GetComponent<tk2dSprite>();
			}
			return _hintButtonSprite;
		}
	}

	private Collider2D hintButtonCollider
	{
		get
		{
			if (_hintButtonCollider == null)
			{
				_hintButtonCollider = hintButton.GetComponent<Collider2D>();
			}
			return _hintButtonCollider;
		}
	}

	public int GetUID => uid;

	private void OnEnable()
	{
		Player.instance.MissionPinned += HandleMissionPinned;
		Player.instance.MissionCompleted += HandleMissionCompleted;
		hintButtonUIItem.OnClick += ClickedMissionHint;
		if (storyMission)
		{
			bool flag = false;
			for (int i = 0; i < Player.instance.activeMissions.Count; i++)
			{
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(Player.instance.activeMissions[i].uid);
				if (mission.type == MissionType.Story)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				animator.transform.localPosition = new Vector3(0f, animator.transform.localPosition.y, animator.transform.localPosition.z);
				animator.enabled = false;
				animator.Play("empty", 0, 0f);
			}
		}
		if (uid == -1)
		{
			hintButton.SetActive(value: false);
		}
		StopCoroutine("UpdateRemainingTimeRoutine");
		StartCoroutine("UpdateRemainingTimeRoutine");
	}

	private void OnDisable()
	{
		Player.instance.MissionPinned -= HandleMissionPinned;
		Player.instance.MissionCompleted -= HandleMissionCompleted;
		hintButtonUIItem.OnClick -= ClickedMissionHint;
	}

	private IEnumerator UpdateRemainingTimeRoutine()
	{
		while (!(timerText == null))
		{
			TimeSpan timeRemaining = DailyMissionManager.instance.currentDailyMission.GetTimeSpanRemaining();
			if (timeRemaining.TotalSeconds <= 0.0)
			{
				timerText.text = Language.Get("EXPIRED", "MENUS,GUI");
			}
			else
			{
				timerText.text = ((int)timeRemaining.TotalHours).ToString("00") + ":" + timeRemaining.Minutes.ToString("00") + ":" + timeRemaining.Seconds.ToString("00");
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public void CheckVisibility()
	{
		if (animator != null)
		{
			if (transform.position.y > 300f)
			{
				animator.gameObject.SetActive(value: false);
			}
			else
			{
				animator.gameObject.SetActive(value: true);
			}
		}
	}

	public void ScollerScrolled(tk2dUIScrollableArea scrollableArea)
	{
		if (GetComponent<tk2dUIItem>() != null)
		{
			GetComponent<tk2dUIItem>().Exit();
		}
		CheckVisibility();
	}

	private void HandleMissionPinned(int missionUID)
	{
		MissionData missionData = Player.instance.GetMissionData(missionUID);
		bool flag = false;
		if (missionData != null && missionData.pinned)
		{
			flag = true;
		}
		if (missionUID == uid && flag)
		{
			pinbox.SetSprite("gui_communicator_missions_pin");
		}
		else
		{
			pinbox.SetSprite("gui_communicator_missions_pinbox");
		}
	}

	private void HandleMissionCompleted(int missionUID)
	{
		if (missionUID == uid)
		{
			collectButton.SetActive(value: true);
			rewardMoney.SetActive(value: false);
			rewardMoneyPlus.SetActive(value: false);
			completeCheckMark.SetActive(value: true);
			if (timerText != null)
			{
				timerText.renderer.enabled = false;
			}
		}
	}

	public void UpdateWithEndGameDescription()
	{
		description.text = Language.Get("ENDGAMEMISSIONLABEL_TT2", "MENUS,GUI");
		pinbox.gameObject.SetActive(value: false);
		preventClick = true;
		if (hintButton != null)
		{
			hintButton.SetActive(value: false);
		}
		if (rewardMoney != null)
		{
			rewardMoney.SetActive(value: false);
		}
		if (rewardMoneyPlus != null)
		{
			rewardMoneyPlus.SetActive(value: false);
		}
		if (newBanner != null)
		{
			newBanner.SetActive(value: false);
		}
		if (icon != null)
		{
			icon.SetSprite("gui_communicator_missions_icon_mainmission");
		}
	}

	public void UpdateWithMissionUID(int missionUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (!(mission != null))
		{
			return;
		}
		if (mission.missionHintType == MissionHintType.None)
		{
			hintButton.SetActive(value: false);
		}
		else
		{
			hintButton.SetActive(value: true);
			if ((mission.missionLocationCity == CityController.instance.cityName || mission.missionLocationCity == CityName.Any) && !CityController.instance.isInterior)
			{
				hintButtonSprite.color = Color.white;
				hintButtonCollider.enabled = true;
			}
			else
			{
				hintButtonSprite.color = new Color(0.5f, 0.5f, 0.5f);
				hintButtonCollider.enabled = false;
			}
		}
		description.renderer.enabled = false;
		if (description != null)
		{
			description.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
		}
		string text = string.Empty;
		if (rewardCityAll != null)
		{
			rewardCityAll.renderer.enabled = false;
		}
		if (rewardCityPlusAll != null)
		{
			rewardCityPlusAll.renderer.enabled = false;
		}
		switch (mission.missionLocationCity)
		{
		case CityName.JumpCity:
			text = "gui_communicator_missions_tab_logo_jumpcity";
			break;
		case CityName.Gotham:
			text = "gui_communicator_missions_tab_logo_gotham";
			break;
		case CityName.Metropolis:
			text = "gui_communicator_missions_tab_logo_metropolis";
			break;
		default:
			if (rewardCityAll != null)
			{
				rewardCityAll.renderer.enabled = true;
				rewardCityAll.text = Language.Get((!mission.isDaily) ? "CITYMENU_ANY" : "QUESTION_MARK", "MENUS,GUI");
			}
			if (rewardCityPlusAll != null)
			{
				rewardCityPlusAll.renderer.enabled = true;
				rewardCityPlusAll.text = Language.Get((!mission.isDaily) ? "CITYMENU_ANY" : "QUESTION_MARK", "MENUS,GUI");
			}
			break;
		}
		if (string.IsNullOrEmpty(text))
		{
			if (rewardCitySprite != null)
			{
				rewardCitySprite.gameObject.SetActive(value: false);
			}
			if (rewardCityPlusSprite != null)
			{
				rewardCityPlusSprite.gameObject.SetActive(value: false);
			}
		}
		else
		{
			if (rewardCitySprite != null)
			{
				rewardCitySprite.SetSprite(text);
				rewardCitySprite.gameObject.SetActive(value: true);
			}
			if (rewardCityPlusSprite != null)
			{
				rewardCityPlusSprite.SetSprite(text);
				rewardCityPlusSprite.gameObject.SetActive(value: true);
			}
		}
		if (mission.rainbowCoinReward > 0)
		{
			rewardMoneyAmnt.text = $"<sprite name=\"icon_currency_rainbowCoin\">{mission.rainbowCoinReward}";
			rewardMoneyPlusAmnt.text = $"<sprite name=\"icon_currency_rainbowCoin\">{mission.rainbowCoinReward}";
		}
		else
		{
			rewardMoneyAmnt.text = $"<sprite name=\"icon_currency_coin\">{mission.coinReward}";
			rewardMoneyPlusAmnt.text = $"<sprite name=\"icon_currency_coin\">{mission.coinReward}";
		}
		uid = missionUID;
		MissionData missionData = Player.instance.GetMissionData(uid);
		if (missionData.pinned)
		{
			pinbox.SetSprite("gui_communicator_missions_pin");
		}
		else
		{
			pinbox.SetSprite("gui_communicator_missions_pinbox");
		}
		if (missionData.completed && !missionData.collected)
		{
			newBanner.SetActive(value: false);
			collectButton.SetActive(value: true);
			rewardMoney.SetActive(value: false);
			rewardMoneyPlus.SetActive(value: false);
			completeCheckMark.SetActive(value: true);
			if (timerText != null)
			{
				timerText.renderer.enabled = false;
			}
		}
		else
		{
			newBanner.SetActive(value: false);
			collectButton.SetActive(value: false);
			rewardMoney.SetActive(value: false);
			rewardMoneyPlus.SetActive(value: false);
			completeCheckMark.SetActive(value: false);
			if (timerText != null)
			{
				timerText.renderer.enabled = true;
			}
			if (mission.characterRewards.Count > 0 || mission.modChipReward > -1)
			{
				rewardMoney.SetActive(value: false);
				rewardMoneyPlus.SetActive(value: true);
				rewardSprite.gameObject.SetActive(value: true);
				if (mission.characterRewards.Count > 0)
				{
					rewardSprite.SetSprite("gui_communicator_missions_rewards_figure");
				}
				else if (mission.modChipReward > -1)
				{
					rewardSprite.SetSprite("gui_communicator_missions_rewards_modchip");
				}
			}
			else
			{
				rewardMoney.SetActive(value: true);
				rewardMoneyPlus.SetActive(value: false);
			}
			if (mission.modChipReward == -1 && mission.characterRewards.Count == 0)
			{
				rewardModChip.SetActive(value: false);
			}
			if (!missionData.viewed)
			{
				newBanner.SetActive(value: true);
			}
			else
			{
				newBanner.SetActive(value: false);
			}
		}
		if (mission.sideType == MissionSideType.Tournament)
		{
			numberCompleted.gameObject.SetActive(value: false);
			if (mission.descriptionOverrides)
			{
				description.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
				description.renderer.enabled = true;
				versus.SetActive(value: false);
			}
		}
		else if (mission.sideType == MissionSideType.Investigate || mission.sideType == MissionSideType.TellCharacter || mission.sideType == MissionSideType.TravelTo || mission.sideType == MissionSideType.Mailbox)
		{
			if (mission.isDaily && mission.sideType == MissionSideType.Investigate)
			{
				mission.iconSpriteName = "gui_communicator_missions_icon_find";
			}
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
			}
			else
			{
				icon.gameObject.SetActive(value: false);
			}
			description.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
			description.renderer.enabled = true;
			versus.SetActive(value: false);
			numberCompleted.gameObject.SetActive(value: false);
		}
		else if (mission.sideType == MissionSideType.Find || mission.sideType == MissionSideType.Deliver || mission.sideType == MissionSideType.ViewFigureWiki || mission.sideType == MissionSideType.MissionChecklist || mission.sideType == MissionSideType.DailyMissionCount)
		{
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
			}
			else
			{
				icon.gameObject.SetActive(value: false);
			}
			description.renderer.enabled = true;
			versus.SetActive(value: false);
			numberCompleted.gameObject.SetActive(value: true);
			numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			description.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS").Replace("{0}", Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString()) : mission.description);
		}
		else if (mission.sideType == MissionSideType.Battle)
		{
			icon.SetSprite("gui_communicator_missions_icon_battle");
			int num = 0;
			int num2 = 0;
			if (mission.battleConfigs.Length > 0)
			{
				for (int i = 0; i < mission.battleConfigs.Length; i++)
				{
					num += missionData.configBattleCounts[i];
					num2 += mission.battleConfigs[i].count;
				}
			}
			else
			{
				num = missionData.currentAmount;
				num2 = mission.amountNeeded;
			}
			if (num2 < 2)
			{
				numberCompleted.gameObject.SetActive(value: false);
			}
			else
			{
				numberCompleted.gameObject.SetActive(value: true);
				numberCompleted.text = num + "/" + num2;
			}
			if (mission.descriptionOverrides)
			{
				description.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
				description.renderer.enabled = true;
				versus.SetActive(value: false);
				return;
			}
			versus.SetActive(value: true);
			if (!string.IsNullOrEmpty(mission.shortTypeNameKey))
			{
				versusText.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
			}
			if (mission.iconSpriteCollection != null)
			{
				portrait.SetSprite(mission.iconSpriteCollection, mission.iconSpriteName);
			}
			else
			{
				portrait.SetSprite(mission.iconSpriteName);
			}
			portrait.transform.localScale = mission.iconScale;
			numberNeeded.text = Mathf.Max(0, num2 - num).ToString();
		}
		else if (mission.sideType == MissionSideType.Buy)
		{
			int num3 = 0;
			int num4 = 0;
			for (int j = 0; j < mission.buyConfigs.Length; j++)
			{
				num3 += missionData.counts[j];
				num4 += mission.buyConfigs[j].count;
			}
			icon.SetSprite("gui_communicator_missions_icon_shop");
			numberCompleted.text = Mathf.Min(num4, num3) + "/" + num4;
			if (mission.descriptionOverrides)
			{
				description.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
				description.renderer.enabled = true;
				versus.SetActive(value: false);
				return;
			}
			versus.SetActive(value: false);
			MissionTypeConfigBuy.FigureConfig.Type type = mission.buyConfigs[0].figureConfig.type;
			int num5 = mission.buyConfigs.Length;
			CharacterDatabase characterDatabase = GameManager.instance.characterDatabase;
			GameObject gameObject = collect1;
			if (num5 == 1)
			{
				gameObject = collect1;
			}
			if (num5 == 2)
			{
				gameObject = collect2;
			}
			if (num5 == 3)
			{
				gameObject = collect3;
			}
			if (num5 == 4)
			{
				gameObject = collect4;
			}
			if (num5 == 5)
			{
				gameObject = collect5;
			}
			gameObject.SetActive(value: true);
			MissionCollect[] componentsInChildren = gameObject.GetComponentsInChildren<MissionCollect>(includeInactive: true);
			for (int k = 0; k < componentsInChildren.Length; k++)
			{
				MissionCollect missionCollect = componentsInChildren[k];
				if (missionCollect.figsText != null)
				{
					missionCollect.figsText.renderer.enabled = false;
				}
				int num6 = missionData.counts[k];
				int count = mission.buyConfigs[k].count;
				switch (type)
				{
				case MissionTypeConfigBuy.FigureConfig.Type.Name:
				{
					CharacterDefinition character = characterDatabase.GetCharacter(mission.buyConfigs[k].figureConfig.validUids[0]);
					missionCollect.iconSprite.SetSprite(character.portraitSprite);
					missionCollect.iconSprite.GetComponent<tk2dAnimationAdapter>().scale = new Vector3(0.5f, 0.5f, 1f);
					missionCollect.nameText.text = Language.Get(character.characterNameKey, "NPCS,FIGS");
					missionCollect.nameText.transform.localPosition = new Vector3(missionCollect.nameText.transform.localPosition.x, -22.4f, missionCollect.nameText.transform.localPosition.z);
					missionCollect.nameText.rectTransform.sizeDelta = new Vector2(36f, 17f);
					missionCollect.nameText.enableWordWrapping = true;
					missionCollect.nameText.fontSizeMin = 4f;
					break;
				}
				case MissionTypeConfigBuy.FigureConfig.Type.Class:
					missionCollect.iconSprite.SetSprite(ClassBonusBox.classIconSpriteNames[(CharacterClass)mission.buyConfigs[k].figureConfig.validUids[0]]);
					missionCollect.iconSprite.GetComponent<tk2dAnimationAdapter>().scale = new Vector3(1f, 1f, 1f);
					missionCollect.nameText.text = string.Empty;
					if (componentsInChildren.Length == 1 && missionCollect.figsText != null)
					{
						missionCollect.figsText.renderer.enabled = true;
					}
					break;
				}
				if (num6 >= count)
				{
					missionCollect.checkmark.SetActive(value: true);
					if (missionCollect.countText != null)
					{
						missionCollect.countText.text = string.Empty;
					}
				}
				else
				{
					missionCollect.checkmark.SetActive(value: false);
					if (missionCollect.countText != null)
					{
						missionCollect.countText.text = (count - num6).ToString();
					}
				}
			}
		}
		else if (mission.sideType == MissionSideType.Collect)
		{
			Debug.Log("mission " + mission.uid);
			versus.SetActive(value: false);
			MissionTypeConfigBuy.FigureConfig.Type type2 = mission.buyConfigs[0].figureConfig.type;
			int num7 = mission.buyConfigs.Length;
			int num8 = 0;
			int num9 = 0;
			for (int l = 0; l < mission.buyConfigs.Length; l++)
			{
				num8 += missionData.counts[l];
				num9 += mission.buyConfigs[l].count;
			}
			CharacterDatabase characterDatabase2 = GameManager.instance.characterDatabase;
			GameObject gameObject2 = collect1;
			if (num7 == 1)
			{
				gameObject2 = collect1;
			}
			if (num7 == 2)
			{
				gameObject2 = collect2;
			}
			if (num7 == 3)
			{
				gameObject2 = collect3;
			}
			if (num7 == 4)
			{
				gameObject2 = collect4;
			}
			if (num7 == 5)
			{
				gameObject2 = collect5;
			}
			gameObject2.SetActive(value: true);
			MissionCollect[] componentsInChildren2 = gameObject2.GetComponentsInChildren<MissionCollect>(includeInactive: true);
			for (int m = 0; m < componentsInChildren2.Length; m++)
			{
				MissionCollect missionCollect2 = componentsInChildren2[m];
				int num10 = missionData.counts[m];
				int count2 = mission.buyConfigs[m].count;
				if (type2 == MissionTypeConfigBuy.FigureConfig.Type.Name)
				{
					CharacterDefinition character2 = characterDatabase2.GetCharacter(mission.buyConfigs[m].figureConfig.validUids[0]);
					missionCollect2.iconSprite.SetSprite(character2.portraitSprite);
					missionCollect2.nameText.text = Language.Get(character2.characterNameKey, "NPCS,FIGS");
					if (num10 >= count2)
					{
						missionCollect2.checkmark.SetActive(value: true);
					}
					else
					{
						missionCollect2.checkmark.SetActive(value: false);
					}
					missionCollect2.nameText.transform.localPosition = new Vector3(missionCollect2.nameText.transform.localPosition.x, -22.4f, missionCollect2.nameText.transform.localPosition.z);
					missionCollect2.nameText.rectTransform.sizeDelta = new Vector2(36f, 17f);
					missionCollect2.nameText.enableWordWrapping = true;
					missionCollect2.nameText.fontSizeMin = 4f;
				}
			}
			gameObject2.SetActive(value: true);
			numberCompleted.text = num8 + "/" + num9;
			versusText.text = Language.Get("COLLECT2", "MENUS,GUI");
		}
		else if (mission.sideType == MissionSideType.NPCScavengerHunt)
		{
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
			}
			else
			{
				icon.gameObject.SetActive(value: false);
			}
			description.renderer.enabled = true;
			versus.SetActive(value: false);
			numberCompleted.gameObject.SetActive(value: true);
			numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			description.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description).Replace("{0}", Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString());
		}
	}

	public void PlayIntroAnimation()
	{
		MissionData missionData = Player.instance.GetMissionData(uid);
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(uid);
		if (missionData != null && missionData.viewed)
		{
			if (animator != null && mission.type == MissionType.Story)
			{
				animator.transform.localPosition = new Vector3(0f, animator.transform.localPosition.y, animator.transform.localPosition.z);
				animator.enabled = false;
			}
			if (animator != null)
			{
				animator.Play("empty", 0, 0f);
			}
		}
		else
		{
			if (missionData == null)
			{
				return;
			}
			missionData.viewed = true;
			if (animator != null)
			{
				if (mission.type == MissionType.Story)
				{
					StartCoroutine("PlayNewStoryMissionAnimation");
				}
				if (mission.type == MissionType.Side)
				{
					animator.Play("MissionPopup_New_SideMission", 0, 0f);
					animator.Update(0f);
				}
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_missions_newmission", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private IEnumerator PlayNewStoryMissionAnimation()
	{
		animator.enabled = false;
		animator.transform.localPosition = new Vector3(400f, animator.transform.localPosition.y, animator.transform.localPosition.z);
		yield return new WaitForEndOfFrame();
		Communicator communicator = CityController.instance.communicator;
		while (communicator.missionsPopUpAnimator.IsName("communicator_missionpopup_intro") && communicator.missionsPopUpAnimator.NormalizedTime() < 0.7f)
		{
			yield return new WaitForEndOfFrame();
		}
		animator.enabled = true;
		animator.Play("MissionPopup_New_StoryMission", 0, 0f);
	}

	private void ClickedMission()
	{
		if (!preventClick)
		{
			pinbox.SetSprite("gui_communicator_missions_pin");
			MissionData pinnedMission = Player.instance.GetPinnedMission();
			if (pinnedMission != null && pinnedMission.uid != uid)
			{
				Player.instance.PinMission(uid);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_pinbutton", GFAudioManager.SFX_GROUP, string.Empty);
				CityController.instance.communicator.CheckForPinnedMission();
			}
			else if (pinnedMission == null)
			{
				Player.instance.PinMission(uid);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_pinbutton", GFAudioManager.SFX_GROUP, string.Empty);
				CityController.instance.communicator.CheckForPinnedMission();
			}
			CityController.instance.communicator.OnClickMissions();
		}
	}

	private IEnumerator CollectReward()
	{
		int tempUid = uid;
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(tempUid);
		if (mission.completionConvo != null)
		{
			yield return CityController.instance.StartCoroutine(CityController.instance.PlayConvo((Conversation)mission.completionConvo));
		}
		CityReward.instance.ShowRewardForMission(tempUid);
		Player.instance.CollectMission(tempUid);
	}

	private void ClickedCollect()
	{
		if (Player.instance.GetMissionData(uid) != null)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_communicator_missions_collect", GFAudioManager.SFX_GROUP, string.Empty);
			CityController.instance.StartCoroutine(CollectReward());
		}
	}

	private void ClickedDebugComplete()
	{
		MissionData missionData = Player.instance.GetMissionData(uid);
		if (missionData != null)
		{
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			missionData.debugCollected = true;
			missionData.completed = true;
			Player.instance.UpdateMission(uid);
			if (mission.type == MissionType.Story || mission.autoCollect)
			{
				Player.instance.CollectMission(uid);
			}
		}
	}

	private void ClickedMissionHint()
	{
		if (uid == -1)
		{
			return;
		}
		MissionData pinnedMission = Player.instance.GetPinnedMission();
		if (pinnedMission != null && pinnedMission.uid != uid && uid > -1)
		{
			ClickedMission();
		}
		CityController.instance.communicator.OnClickMissions();
		if (uid <= -1)
		{
			return;
		}
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(uid);
		if (!CityController.instance.pinchZoomer.isZoomedOut && mission.missionHintType != MissionHintType.Conversation)
		{
			CityController.instance.pinchZoomer.ZoomOut();
		}
		switch (mission.missionHintType)
		{
		case MissionHintType.Position:
			if (!string.IsNullOrEmpty(mission.missionLocation))
			{
				Vector3 vector = CityController.Vector3FromString(mission.missionLocation);
				CityController.instance.cameraHolder.transform.DOMove(vector, 0.6f);
				CityController.instance.MoveWayPointToPosition(vector);
			}
			break;
		case MissionHintType.NPC:
		{
			if (string.IsNullOrEmpty(mission.missionHintNPC))
			{
				break;
			}
			CityEntityNPC activeNPCEntity = CityController.instance.GetActiveNPCEntity(mission.missionHintNPC);
			if (activeNPCEntity != null)
			{
				Landmark landmark = activeNPCEntity.cityNPC.landmark;
				if (landmark != null && landmark.icon != null)
				{
					CityController.instance.LandmarkWayPointClicked(landmark.icon, unselectSelected: false);
					break;
				}
				Vector3 position3 = activeNPCEntity.transform.position;
				CityController.instance.cameraHolder.transform.DOMove(position3, 0.6f);
				CityController.instance.MoveWayPointToPosition(position3);
			}
			break;
		}
		case MissionHintType.Shop:
		{
			if (mission.missionLocationShop <= -1)
			{
				break;
			}
			for (int i = 0; i < CityController.instance.cityShops.Count; i++)
			{
				CityShop cityShop = CityController.instance.cityShops[i];
				if (cityShop.shopUID == mission.missionLocationShop)
				{
					Landmark component = cityShop.gameObject.GetComponent<Landmark>();
					if (component != null && component.icon != null)
					{
						CityController.instance.LandmarkWayPointClicked(component.icon, unselectSelected: false);
						break;
					}
					Vector3 position = cityShop.gameObject.transform.position;
					CityController.instance.cameraHolder.transform.DOMove(position, 0.6f);
					CityController.instance.MoveWayPointToPosition(position);
					break;
				}
			}
			break;
		}
		case MissionHintType.Tournament:
		{
			if (!(mission.missionTournamentLocation != null))
			{
				break;
			}
			for (int j = 0; j < CityController.instance.tournamentBuildings.Count; j++)
			{
				CityEvent component2 = CityController.instance.tournamentBuildings[j].GetComponent<CityEvent>();
				if (component2.tournamentDef.Equals(mission.missionTournamentLocation))
				{
					Landmark component3 = component2.gameObject.GetComponent<Landmark>();
					if (component3 != null && component3.icon != null)
					{
						CityController.instance.LandmarkWayPointClicked(component3.icon, unselectSelected: false);
						break;
					}
					Vector3 position2 = component2.gameObject.transform.position;
					CityController.instance.cameraHolder.transform.DOMove(position2, 0.6f);
					CityController.instance.MoveWayPointToPosition(position2);
					break;
				}
			}
			break;
		}
		case MissionHintType.Conversation:
			if (mission.missionHintConvo != null)
			{
				EventMessenger.Instance.Raise(new StartConversationEvent(mission.missionHintConvo));
			}
			break;
		case MissionHintType.None:
			break;
		}
	}
}
