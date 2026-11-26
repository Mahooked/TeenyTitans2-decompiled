using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MissionFlag : MonoBehaviour
{
	private class MissionQueueItem
	{
		public int missionUID;

		public Character character;

		public ModChipDefinition modChip;
	}

	public GameObject missionFlagGroup;

	public Animator animator;

	public tk2dSprite icon;

	public tk2dSprite portrait;

	public GameObject checkMark;

	public TextMeshPro descriptionText;

	public TextMeshPro shortTypeText;

	public TextMeshPro shortTypeText2;

	public TextMeshPro numberNeeded;

	public TextMeshPro numberCompleted;

	public GameObject collect1;

	public GameObject collect2;

	public GameObject collect3;

	public GameObject collect4;

	public GameObject collect5;

	public GameObject battle;

	private List<MissionQueueItem> missionUIDQueue = new List<MissionQueueItem>();

	private bool inQueue;

	private void OnEnable()
	{
		Player.instance.CharacterAdded += HandleCharacterAdded;
		Player.instance.MissionUpdated += HandleMissionUpdatedEvent;
		Player.instance.MissionCompleted += HandleMissionCompletedEvent;
	}

	private void OnDisable()
	{
		Player.instance.CharacterAdded -= HandleCharacterAdded;
		Player.instance.MissionUpdated -= HandleMissionUpdatedEvent;
		Player.instance.MissionCompleted -= HandleMissionCompletedEvent;
	}

	private IEnumerator MissionFlagQueue()
	{
		inQueue = true;
		yield return new WaitForEndOfFrame();
		while (missionUIDQueue.Count > 0)
		{
			int missionUID = missionUIDQueue[0].missionUID;
			Character character = missionUIDQueue[0].character;
			ModChipDefinition modChip = missionUIDQueue[0].modChip;
			MissionData missionData = Player.instance.GetMissionData(missionUID);
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: false);
			bool waitForAnimation = false;
			if (missionData != null)
			{
				if (mission.sideType == MissionSideType.Buy)
				{
					waitForAnimation = true;
					AddToBuyMission(missionData, mission, character, modChip);
				}
				if (mission.sideType == MissionSideType.Collect && mission.collectUIDS.Contains(character.characterDefinition.uid))
				{
					waitForAnimation = true;
					AddToCollectMission(missionData, mission, character);
				}
				if (mission.isDaily && (mission.sideType == MissionSideType.Buy || mission.sideType == MissionSideType.Collect) && mission.descriptionOverrides)
				{
					descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
				}
			}
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			if (waitForAnimation)
			{
				while (animator.NormalizedTime() < 0.99f && !mission.skipMissionBanner)
				{
					yield return new WaitForEndOfFrame();
				}
			}
			missionUIDQueue.RemoveAt(0);
			missionUIDQueue.TrimExcess();
			inQueue = false;
			yield return new WaitForEndOfFrame();
		}
		inQueue = false;
		yield return new WaitForEndOfFrame();
	}

	private void AddToBuyMission(MissionData missionData, Mission mission, Character character = null, ModChipDefinition modChip = null)
	{
		if (character == null && modChip == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = -1;
		for (int i = 0; i < mission.buyConfigs.Length; i++)
		{
			num += missionData.counts[i];
			num2 += mission.buyConfigs[i].count;
			if (character != null)
			{
				if (num3 == -1 && missionData.counts[i] < mission.buyConfigs[i].count && mission.buyConfigs[i].figureConfig.IsCharacterValid(character))
				{
					num3 = i;
				}
			}
			else if (modChip != null && num3 == -1 && missionData.counts[i] < mission.buyConfigs[i].count && (!mission.buyConfigs[i].boolValue1 || modChip.uid == mission.buyConfigs[i].intValue1))
			{
				num3 = i;
			}
		}
		if (num > num2 || num3 == -1)
		{
			return;
		}
		missionData.counts[num3]++;
		num++;
		missionFlagGroup.SetActive(value: true);
		portrait.gameObject.SetActive(value: false);
		if (mission.descriptionOverrides)
		{
			battle.SetActive(value: false);
			descriptionText.gameObject.SetActive(value: true);
			descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
		}
		else if (mission.buyConfigs[0].toyType == ToyType.Figure)
		{
			MissionTypeConfigBuy.FigureConfig.Type type = mission.buyConfigs[0].figureConfig.type;
			int num4 = mission.buyConfigs.Length;
			CharacterDatabase characterDatabase = GameManager.instance.characterDatabase;
			GameObject gameObject = collect1;
			if (num4 == 1)
			{
				gameObject = collect1;
			}
			if (num4 == 2)
			{
				gameObject = collect2;
			}
			if (num4 == 3)
			{
				gameObject = collect3;
			}
			if (num4 == 4)
			{
				gameObject = collect4;
			}
			if (num4 == 5)
			{
				gameObject = collect5;
			}
			gameObject.SetActive(value: true);
			MissionCollect[] componentsInChildren = gameObject.GetComponentsInChildren<MissionCollect>(includeInactive: true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				MissionCollect missionCollect = componentsInChildren[j];
				if (missionCollect.figsText != null)
				{
					missionCollect.figsText.renderer.enabled = false;
				}
				int num5 = missionData.counts[j];
				int count = mission.buyConfigs[j].count;
				switch (type)
				{
				case MissionTypeConfigBuy.FigureConfig.Type.Name:
				{
					CharacterDefinition character2 = characterDatabase.GetCharacter(mission.buyConfigs[j].figureConfig.validUids[0]);
					missionCollect.iconSprite.SetSprite(character2.portraitSprite);
					missionCollect.iconSprite.GetComponent<tk2dAnimationAdapter>().scale = new Vector3(0.5f, 0.5f, 1f);
					missionCollect.nameText.text = Language.Get(character2.characterNameKey, "NPCS,FIGS");
					missionCollect.nameText.transform.localPosition = new Vector3(missionCollect.nameText.transform.localPosition.x, -22.4f, missionCollect.nameText.transform.localPosition.z);
					missionCollect.nameText.rectTransform.sizeDelta = new Vector2(36f, 17f);
					missionCollect.nameText.enableWordWrapping = true;
					missionCollect.nameText.fontSizeMin = 4f;
					break;
				}
				case MissionTypeConfigBuy.FigureConfig.Type.Class:
					missionCollect.iconSprite.SetSprite(ClassBonusBox.classIconSpriteNames[(CharacterClass)mission.buyConfigs[j].figureConfig.validUids[0]]);
					missionCollect.iconSprite.GetComponent<tk2dAnimationAdapter>().scale = new Vector3(1f, 1f, 1f);
					missionCollect.nameText.text = string.Empty;
					if (componentsInChildren.Length == 1 && missionCollect.figsText != null)
					{
						missionCollect.figsText.renderer.enabled = true;
					}
					break;
				}
				if (num5 >= count)
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
						missionCollect.countText.text = (count - num5).ToString();
					}
				}
			}
		}
		icon.SetSprite("gui_communicator_missions_icon_shop");
		numberNeeded.text = Mathf.Max(0, num2 - num).ToString();
		numberCompleted.text = num + "/" + num2;
		if (num >= num2 && string.IsNullOrEmpty(mission.deliveryNPC))
		{
			Player.instance.CompleteMission(mission.uid);
			if (mission.type == MissionType.Story)
			{
				if (num2 <= 1)
				{
					numberCompleted.gameObject.SetActive(value: false);
				}
				CollectMission(mission);
			}
			else if (mission.autoCollect)
			{
				CollectMission(mission);
			}
			if (!mission.skipMissionBanner)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
				animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
			}
		}
		else if (!mission.skipMissionBanner)
		{
			animator.Play("MissionFlag_Trigger", 0, 0f);
		}
	}

	private void AddToCollectMission(MissionData missionData, Mission mission, Character character)
	{
		int uid = mission.uid;
		missionFlagGroup.SetActive(value: true);
		icon.SetSprite("gui_communicator_missions_icon_shop");
		int count = mission.collectUIDS.Count;
		CharacterDatabase characterDatabase = GameManager.instance.characterDatabase;
		GameObject gameObject = collect1;
		if (count == 1)
		{
			gameObject = collect1;
		}
		if (count == 2)
		{
			gameObject = collect2;
		}
		if (count == 3)
		{
			gameObject = collect3;
		}
		if (count == 4)
		{
			gameObject = collect4;
		}
		if (count == 5)
		{
			gameObject = collect5;
		}
		gameObject.SetActive(value: true);
		MissionCollect[] componentsInChildren = gameObject.GetComponentsInChildren<MissionCollect>(includeInactive: true);
		int num = 0;
		bool flag = false;
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MissionCollect missionCollect = componentsInChildren[i];
			CharacterDefinition character2 = characterDatabase.GetCharacter(mission.collectUIDS[i]);
			missionCollect.iconSprite.SetSprite(character2.portraitSprite);
			missionCollect.nameText.text = Language.Get(character2.characterNameKey, "NPCS,FIGS");
			missionCollect.checkmark.SetActive(value: false);
			missionCollect.nameText.transform.localPosition = new Vector3(missionCollect.nameText.transform.localPosition.x, -22.4f, missionCollect.nameText.transform.localPosition.z);
			missionCollect.nameText.rectTransform.sizeDelta = new Vector2(36f, 17f);
			missionCollect.nameText.enableWordWrapping = true;
			missionCollect.nameText.fontSizeMin = 4f;
		}
		List<int> list = new List<int>();
		for (int j = 0; j < missionData.collectedUIDs.Count; j++)
		{
			bool flag2 = false;
			for (int k = 0; k < componentsInChildren.Length; k++)
			{
				if (!flag2)
				{
					MissionCollect missionCollect2 = componentsInChildren[k];
					int num2 = mission.collectUIDS[k];
					CharacterDefinition character3 = characterDatabase.GetCharacter(num2);
					if (missionData.collectedUIDs[j] == num2 && !list.Contains(k))
					{
						flag2 = true;
						list.Add(k);
						missionCollect2.checkmark.SetActive(value: true);
					}
				}
			}
		}
		missionData.collectedUIDs.Add(character.characterDefinition.uid);
		for (int l = 0; l < componentsInChildren.Length; l++)
		{
			MissionCollect missionCollect3 = componentsInChildren[l];
			int num3 = mission.collectUIDS[l];
			CharacterDefinition character4 = characterDatabase.GetCharacter(num3);
			if (character.characterDefinition.uid == num3 && !list.Contains(l))
			{
				missionCollect3.checkmark.SetActive(value: true);
				if (character != null && character.characterDefinition.uid == character4.uid)
				{
					flag = true;
				}
				break;
			}
		}
		num = missionData.collectedUIDs.Count;
		if (!flag)
		{
			missionFlagGroup.SetActive(value: false);
			return;
		}
		int num4 = count;
		numberNeeded.text = Mathf.Max(0, num4 - num).ToString();
		numberCompleted.text = num + "/" + num4;
		if (num >= num4)
		{
			if (mission.type == MissionType.Story)
			{
				CollectMission(mission);
			}
			else if (mission.type == MissionType.Side && mission.autoCollect)
			{
				CollectMission(mission);
			}
			if (!mission.skipMissionBanner)
			{
				Debug.Log("PlayClipFromResources(\"Audio/SFX/sfx_gui_missioncompletebanner");
				if (!mission.isDaily)
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
					animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
				}
				else if (mission.descriptionOverrides)
				{
					collect1.SetActive(value: false);
					collect2.SetActive(value: false);
					collect3.SetActive(value: false);
					collect4.SetActive(value: false);
					collect5.SetActive(value: false);
					battle.SetActive(value: false);
					descriptionText.gameObject.SetActive(value: true);
					descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
					Debug.Log("show " + descriptionText.text);
				}
			}
			else
			{
				Debug.Log("Skip mission banner");
			}
		}
		else if (!mission.skipMissionBanner)
		{
			animator.Play("MissionFlag_Trigger", 0, 0f);
		}
	}

	private int CharacterUIDNeededForMission(int missionUID, int characterUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		int num = 0;
		if (mission != null && (mission.sideType == MissionSideType.Buy || mission.sideType == MissionSideType.Collect))
		{
			if (mission.sideType == MissionSideType.Buy)
			{
				return mission.amountNeeded;
			}
			for (int i = 0; i < mission.collectUIDS.Count; i++)
			{
				if (mission.collectUIDS[i] == characterUID)
				{
					num++;
				}
			}
		}
		return num;
	}

	private int CharacterUIDCollectedForMission(int missionUID, int characterUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		MissionData missionData = Player.instance.GetMissionData(missionUID);
		int num = 0;
		if (mission != null && missionData != null && (mission.sideType == MissionSideType.Buy || mission.sideType == MissionSideType.Collect))
		{
			if (mission.sideType == MissionSideType.Buy)
			{
				return missionData.currentAmount;
			}
			for (int i = 0; i < missionData.collectedUIDs.Count; i++)
			{
				if (missionData.collectedUIDs[i] == characterUID)
				{
					num++;
				}
			}
		}
		return num;
	}

	public void HandleCharacterAdded(Character obj)
	{
		if (!(ShopController.instance == null) && Player.instance.GetMissionData(4) != null)
		{
			CheckCharacterAdded(obj);
		}
	}

	public void CheckCharacterAdded(Character character)
	{
		MissionData[] array = Player.instance.activeMissions.ToArray();
		foreach (MissionData missionData in array)
		{
			if (missionData == null || missionData.completed)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.Buy)
			{
				continue;
			}
			bool flag = false;
			for (int j = 0; j < mission.buyConfigs.Length; j++)
			{
				MissionTypeConfigBuy missionTypeConfigBuy = mission.buyConfigs[j];
				if (missionTypeConfigBuy.toyType == ToyType.Figure && missionData.counts[j] < missionTypeConfigBuy.count && missionTypeConfigBuy.figureConfig.IsCharacterValid(character))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				MissionQueueItem missionQueueItem = new MissionQueueItem();
				missionQueueItem.missionUID = mission.uid;
				missionQueueItem.character = character;
				missionUIDQueue.Add(missionQueueItem);
				if (!inQueue)
				{
					StartCoroutine("MissionFlagQueue");
				}
				break;
			}
		}
	}

	public void CheckModChipAdded(ModChipDefinition modChip)
	{
		MissionData[] array = Player.instance.activeMissions.ToArray();
		foreach (MissionData missionData in array)
		{
			if (missionData == null || missionData.completed)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.Buy)
			{
				continue;
			}
			bool flag = false;
			for (int j = 0; j < mission.buyConfigs.Length; j++)
			{
				MissionTypeConfigBuy missionTypeConfigBuy = mission.buyConfigs[j];
				if (missionTypeConfigBuy.toyType == ToyType.ModChip && missionData.counts[j] < missionTypeConfigBuy.count && (!missionTypeConfigBuy.boolValue1 || modChip.uid == missionTypeConfigBuy.intValue1))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				MissionQueueItem missionQueueItem = new MissionQueueItem();
				missionQueueItem.missionUID = mission.uid;
				missionQueueItem.modChip = modChip;
				missionUIDQueue.Add(missionQueueItem);
				if (!inQueue)
				{
					StartCoroutine("MissionFlagQueue");
				}
				break;
			}
		}
	}

	private void HandleMissionCompletedEvent(int missionUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (!(mission != null))
		{
			return;
		}
		if (!mission.skipMissionBanner)
		{
			missionFlagGroup.SetActive(value: true);
		}
		if (mission.isDaily)
		{
			numberCompleted.gameObject.SetActive(value: false);
			icon.transform.parent.gameObject.SetActive(value: false);
			descriptionText.gameObject.SetActive(value: true);
			descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
		}
		if (mission.sideType == MissionSideType.Investigate || mission.sideType == MissionSideType.TellCharacter || mission.sideType == MissionSideType.TravelTo || mission.sideType == MissionSideType.Mailbox || mission.sideType == MissionSideType.Find)
		{
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: false);
			numberCompleted.gameObject.SetActive(value: false);
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
			}
			descriptionText.gameObject.SetActive(value: true);
			descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
		}
		else if (mission.sideType == MissionSideType.Battle)
		{
			MissionData missionData = Player.instance.GetMissionData(missionUID);
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: true);
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
			icon.SetSprite("gui_communicator_missions_icon_battle");
			if (mission.iconSpriteCollection != null)
			{
				portrait.SetSprite(mission.iconSpriteCollection, mission.iconSpriteName);
			}
			else
			{
				portrait.SetSprite(mission.iconSpriteName);
			}
			portrait.transform.localScale = mission.iconScale;
			if (mission.descriptionOverrides)
			{
				battle.SetActive(value: false);
				descriptionText.gameObject.SetActive(value: true);
				descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
				numberCompleted.gameObject.SetActive(value: false);
			}
			else
			{
				descriptionText.gameObject.SetActive(value: false);
				battle.SetActive(value: true);
				shortTypeText.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
				numberNeeded.text = Mathf.Max(0, num2 - num).ToString();
				numberCompleted.text = num + "/" + num2;
			}
			if (num >= num2)
			{
				if (mission.type == MissionType.Story)
				{
					CollectMission(mission);
				}
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
				animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
			}
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
		animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
	}

	private void HandleMissionUpdatedEvent(int missionUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (!(mission != null))
		{
			return;
		}
		MissionData missionData = Player.instance.GetMissionData(missionUID);
		if (missionData == null)
		{
			Debug.LogError("MissionFlag.cs, MISSION DATA IS NULL");
			return;
		}
		if (!mission.skipMissionBanner)
		{
			missionFlagGroup.SetActive(value: true);
		}
		if (mission.sideType == MissionSideType.Tournament)
		{
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: false);
			icon.SetSprite("gui_communicator_missions_icon_mainmission");
			if (mission.descriptionOverrides)
			{
				battle.SetActive(value: false);
				descriptionText.gameObject.SetActive(value: true);
				descriptionText.text = Language.Get(mission.descriptionKey, "MISSIONS");
				numberCompleted.gameObject.SetActive(value: false);
			}
			else
			{
				descriptionText.gameObject.SetActive(value: false);
				battle.SetActive(value: true);
				shortTypeText.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			}
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				if (mission.type == MissionType.Story)
				{
					CollectMission(mission);
				}
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
				animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
			}
			else
			{
				animator.Play("MissionFlag_Trigger", 0, 0f);
			}
		}
		else if (mission.sideType == MissionSideType.Battle)
		{
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: true);
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
			icon.SetSprite("gui_communicator_missions_icon_battle");
			if (mission.iconSpriteCollection != null)
			{
				portrait.SetSprite(mission.iconSpriteCollection, mission.iconSpriteName);
			}
			else
			{
				portrait.SetSprite(mission.iconSpriteName);
			}
			portrait.transform.localScale = mission.iconScale;
			if (mission.descriptionOverrides)
			{
				battle.SetActive(value: false);
				descriptionText.gameObject.SetActive(value: true);
				descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
				if (num2 > 0)
				{
					numberCompleted.gameObject.SetActive(value: true);
					numberCompleted.text = num + "/" + num2;
				}
				else
				{
					numberCompleted.gameObject.SetActive(value: false);
				}
			}
			else
			{
				descriptionText.gameObject.SetActive(value: false);
				battle.SetActive(value: true);
				shortTypeText.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
				numberNeeded.text = Mathf.Max(0, num2 - num).ToString();
				numberCompleted.text = num + "/" + num2;
			}
			if (num >= num2)
			{
				if (mission.type == MissionType.Story)
				{
					CollectMission(mission);
				}
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
				animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
			}
			else
			{
				animator.Play("MissionFlag_Trigger", 0, 0f);
			}
		}
		else if (mission.sideType == MissionSideType.Find || mission.sideType == MissionSideType.ViewFigureWiki || mission.sideType == MissionSideType.MissionChecklist || mission.sideType == MissionSideType.DailyMissionCount)
		{
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: true);
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
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
			if (mission.descriptionOverrides)
			{
				battle.SetActive(value: false);
				descriptionText.gameObject.SetActive(value: true);
				numberCompleted.gameObject.SetActive(value: true);
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				string oldText = Mathf.Max(0, missionData.currentAmount - 1) + "/" + mission.amountNeeded;
				string newText = missionData.currentAmount + "/" + mission.amountNeeded;
				StartCoroutine(DelayNumberBump(numberCompleted, oldText, newText, 0.8f));
				descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS").Replace("{0}", numberNeeded.text) : mission.description);
			}
			else
			{
				descriptionText.gameObject.SetActive(value: false);
				battle.SetActive(value: true);
				shortTypeText.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			}
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				if (mission.type == MissionType.Story)
				{
					CollectMission(mission);
				}
				if (!mission.skipMissionBanner)
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
					animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
				}
			}
			else if (!mission.skipMissionBanner)
			{
				animator.Play("MissionFlag_Trigger", 0, 0f);
			}
		}
		else if (mission.sideType == MissionSideType.Investigate || mission.sideType == MissionSideType.TellCharacter || mission.sideType == MissionSideType.TravelTo)
		{
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: false);
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
			}
			if (mission.amountNeeded == 0)
			{
				numberCompleted.gameObject.SetActive(value: false);
			}
			else
			{
				numberCompleted.gameObject.SetActive(value: true);
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			}
			descriptionText.gameObject.SetActive(value: true);
			descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS").Replace("{0}", numberNeeded.text.ToString()) : mission.description);
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				if (mission.type == MissionType.Story)
				{
					CollectMission(mission);
				}
				if (!mission.skipMissionBanner)
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
					animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
				}
			}
			else if (!mission.skipMissionBanner)
			{
				animator.Play("MissionFlag_Trigger", 0, 0f);
			}
		}
		else if (mission.sideType == MissionSideType.Deliver || ((mission.sideType == MissionSideType.Buy || mission.sideType == MissionSideType.Collect) && mission.isDaily))
		{
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: false);
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
			}
			if (mission.descriptionOverrides)
			{
				battle.SetActive(value: false);
				descriptionText.gameObject.SetActive(value: true);
				descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
				numberCompleted.gameObject.SetActive(value: true);
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			}
			else
			{
				descriptionText.gameObject.SetActive(value: false);
				battle.SetActive(value: true);
				shortTypeText.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			}
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				if (mission.type == MissionType.Story)
				{
					CollectMission(mission);
				}
				if (!mission.skipMissionBanner)
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
					animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
				}
			}
			else if (!mission.skipMissionBanner)
			{
				animator.Play("MissionFlag_Trigger", 0, 0f);
			}
		}
		else
		{
			if (mission.sideType != MissionSideType.NPCScavengerHunt)
			{
				return;
			}
			collect1.SetActive(value: false);
			collect2.SetActive(value: false);
			collect3.SetActive(value: false);
			collect4.SetActive(value: false);
			collect5.SetActive(value: false);
			battle.SetActive(value: true);
			if (!string.IsNullOrEmpty(mission.iconSpriteName))
			{
				icon.SetSprite(mission.iconSpriteName);
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
			if (mission.descriptionOverrides)
			{
				battle.SetActive(value: false);
				descriptionText.gameObject.SetActive(value: true);
				numberCompleted.gameObject.SetActive(value: true);
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				string oldText2 = Mathf.Max(0, missionData.currentAmount - 1) + "/" + mission.amountNeeded;
				string newText2 = missionData.currentAmount + "/" + mission.amountNeeded;
				StartCoroutine(DelayNumberBump(numberCompleted, oldText2, newText2, 0.8f));
				descriptionText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description).Replace("{0}", numberNeeded.text.ToString());
			}
			else
			{
				descriptionText.gameObject.SetActive(value: false);
				battle.SetActive(value: true);
				shortTypeText.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
				numberNeeded.text = Mathf.Max(0, mission.amountNeeded - missionData.currentAmount).ToString();
				numberCompleted.text = missionData.currentAmount + "/" + mission.amountNeeded;
			}
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				if (mission.type == MissionType.Story)
				{
					CollectMission(mission);
				}
				if (!mission.skipMissionBanner)
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_missioncompletebanner", GFAudioManager.SFX_GROUP, string.Empty);
					animator.Play("MissionFlag_Trigger_Complete", 0, 0f);
				}
			}
			else if (!mission.skipMissionBanner)
			{
				animator.Play("MissionFlag_Trigger", 0, 0f);
			}
		}
	}

	private void CollectMission(Mission mission)
	{
		Player.instance.CollectMission(mission.uid, notify: false);
		if (mission.coinReward > 0)
		{
			if (BattleController.instance != null)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_coincollect", GFAudioManager.SFX_GROUP, string.Empty);
				BattleController.instance.walletGUI.AddToWallet(mission.coinReward);
			}
			else if (ShopController.instance != null)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_coincollect", GFAudioManager.SFX_GROUP, string.Empty);
				Player.instance.AddCurrency(mission.coinReward);
			}
		}
	}

	private IEnumerator DelayNumberBump(TextMeshPro textMesh, string oldText, string newText, float delay = 1f)
	{
		textMesh.text = oldText;
		yield return new WaitForSeconds(delay);
		textMesh.transform.DOPunchScale(new Vector3(2f, 2f, 2f), 0.1f).OnComplete(() =>
		{
			textMesh.text = newText;
		});
	}
}
