using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyMissionData
{
	public int uid;

	public DailyMissionType type;

	public string missionLocationNPC = string.Empty;

	public MissionTypeConfigBattle battleConfig;

	public MissionTypeConfigBuy buyConfig;

	public BattleCondition battleCondition;

	public TournamentRoundFigureData[] figureOverrides;

	public int coinReward;

	public int rainbowCoinReward;

	public int modChipReward = -1;

	public List<CharacterRewardData> characterRewards = new List<CharacterRewardData>();

	public int difficulty = 1;

	[SerializeField]
	private DateTime _startDateTime;

	[SerializeField]
	private DateTime _completedDateTime;

	public DateTime startDateTime
	{
		get
		{
			return _startDateTime;
		}
	}

	public DateTime completedDataTime
	{
		get
		{
			return _completedDateTime;
		}
	}

	public string description
	{
		get
		{
			switch (type)
			{
			case DailyMissionType.DefeatNPC:
				return GetDescription_DefeatNPC();
			case DailyMissionType.DefeatNPCType:
				return GetDescription_DefeatNPCType();
			case DailyMissionType.Shopping:
				return GetDescription_Shopping();
			default:
				return string.Empty;
			}
		}
	}

	public bool isExpired
	{
		get
		{
			return GetSecondsLeft() <= 0;
		}
	}

	public DailyMissionData()
	{
		_startDateTime = DateTime.Now + DailyMissionManager.debugTimeOffset;
	}

	public void RecordCompletedTime()
	{
		_completedDateTime = DateTime.Now;
	}

	public TimeSpan GetTimeSpanRemaining()
	{
		DateTime dateTime = _startDateTime.Date.AddDays(1.0).AddTicks(-1L);
		return dateTime - (DateTime.Now + DailyMissionManager.debugTimeOffset);
	}

	public int GetSecondsLeft()
	{
		return (int)GetTimeSpanRemaining().TotalSeconds;
	}

	private string ReplaceClassStringWithSprite(string enterString)
	{
		string text = enterString.Replace("Tech", Language.Get("CLASS_TECH", "MENUS,GUI") + " <sprite name=\"icon_class_tech\">");
		text = text.Replace("Beast", Language.Get("CLASS_BEAST", "MENUS,GUI") + " <sprite name=\"icon_class_beast\">");
		text = text.Replace("Cute", Language.Get("CLASS_CUTE", "MENUS,GUI") + " <sprite name=\"icon_class_cute\">");
		text = text.Replace("DarkArts", Language.Get("CLASS_DARKARTS", "MENUS,GUI") + " <sprite name=\"icon_class_darkArts\">");
		text = text.Replace("MartialArts", Language.Get("CLASS_MARTIALARTS", "MENUS,GUI") + " <sprite name=\"icon_class_martialArts\">");
		text = text.Replace("None", Language.Get("CLASS_NONE", "MENUS,GUI") + " <sprite name=\"icon_class_none\">");
		return text.Replace("Super", Language.Get("CLASS_SUPER", "MENUS,GUI") + " <sprite name=\"icon_class_super\">");
	}

	private string GetClassSpriteText(CharacterClass charClass)
	{
		switch (charClass)
		{
		case CharacterClass.Beast:
			return Language.Get("CLASS_BEAST", "MENUS,GUI") + " <sprite name=\"icon_class_beast\">";
		case CharacterClass.Cute:
			return Language.Get("CLASS_CUTE", "MENUS,GUI") + " <sprite name=\"icon_class_cute\">";
		case CharacterClass.DarkArts:
			return Language.Get("CLASS_DARKARTS", "MENUS,GUI") + " <sprite name=\"icon_class_darkArts\">";
		case CharacterClass.MartialArts:
			return Language.Get("CLASS_MARTIALARTS", "MENUS,GUI") + " <sprite name=\"icon_class_martialArts\">";
		case CharacterClass.None:
			return Language.Get("CLASS_NONE", "MENUS,GUI") + " <sprite name=\"icon_class_none\">";
		case CharacterClass.Super:
			return Language.Get("CLASS_SUPER", "MENUS,GUI") + " <sprite name=\"icon_class_super\">";
		case CharacterClass.Tech:
			return Language.Get("CLASS_TECH", "MENUS,GUI") + " <sprite name=\"icon_class_tech\">";
		default:
			return string.Empty;
		}
	}

	private string GetDescription_DefeatNPC()
	{
		string text = "Get Description Defeat NPC...\n";
		string text2 = string.Format("<color=#7ad5ff>{0}</color>", Language.Get(GameManager.instance.npcDatabase.GetNPCDef(battleConfig.npcGuid).nameKey, "NPCS,FIGS"));
		text = text + "target NPC: " + text2 + "\n";
		switch (battleCondition.conditionType)
		{
		case BattleCondition.ConditionType.FigureName:
		{
			text = text + "fig character name: " + GameManager.instance.characterDatabase.GetCharacter(battleCondition.intValue2).characterName + "\n";
			string text4 = string.Format("<color=#fa8efe>{0}</color>", Language.Get(GameManager.instance.characterDatabase.GetCharacter(battleCondition.intValue2).characterNameKey, "NPCS,FIGS"));
			text = text + "fig localized character name: " + text4 + "\n";
			text = text + "localized string: " + Language.Get("DAILY_DEFEATNPC_FIG", "MISSIONS") + "\n";
			Debug.Log(text);
			return string.Format(Language.Get("DAILY_DEFEATNPC_FIG", "MISSIONS"), text2, text4);
		}
		case BattleCondition.ConditionType.FigureTags:
		{
			string text5 = GameManager.instance.tagCollection.GetTagItem(battleCondition.stringValue1).displayTextSingular;
			text = text + "tagName: " + text5 + "\n";
			string text4 = string.Format("<color=#fa8efe>{0}</color>", Language.Get(GameManager.instance.tagCollection.GetTagItem(battleCondition.stringValue1).displayTextSingular.key, "NPCS,FIGS"));
			text = text + "fig: " + text4 + "\n";
			text = text + "localized string: " + Language.Get("DAILY_DEFEATNPC_TAG", "MISSIONS") + "\n";
			Debug.Log(text);
			return string.Format(Language.Get("DAILY_DEFEATNPC_TAG", "MISSIONS"), text2, text4);
		}
		case BattleCondition.ConditionType.FigureClass:
		{
			CharacterClass intValue = (CharacterClass)battleCondition.intValue2;
			string text3 = text;
			text = string.Concat(text3, "charClass: ", intValue, "\n");
			string text4 = ReplaceClassStringWithSprite(string.Format("<color=#fa8efe>{0}</color>", intValue.ToString()));
			text = text + "fig: " + text4 + "\n";
			text = text + "localized string: " + Language.Get("DAILY_DEFEATNPC_TAG", "MISSIONS") + "\n";
			Debug.Log(text);
			return string.Format(Language.Get("DAILY_DEFEATNPC_TAG", "MISSIONS"), text2, text4);
		}
		default:
			return "DESCRIPTION NOT FOUND";
		}
	}

	private string GetDescription_DefeatNPCType()
	{
		string text = "Get Description Defeat NPC Type...\n";
		TagItem tagItem = GameManager.instance.tagCollection.GetTagItem(battleConfig.npcTag);
		string text2 = text;
		text = text2 + "amountNeeded: " + battleConfig.count + "\n";
		text = string.Concat(text, "tagItem: ", (battleConfig.count <= 1) ? tagItem.displayTextSingular : tagItem.displayTextPlural, "\n");
		string text3 = string.Format("<color=#7ad5ff>{0}</color>", (battleConfig.count <= 1) ? Language.Get(tagItem.displayTextSingular.key, "NPCS,FIGS") : Language.Get(tagItem.displayTextPlural.key, "NPCS,FIGS"));
		text = text + "condition Text: " + text3 + "\n";
		text = text + "localized string: " + Language.Get("DAILY_DEFEAT_TAG", "MISSIONS") + "\n";
		Debug.Log(text);
		return string.Format(Language.Get("DAILY_DEFEAT_TAG", "MISSIONS"), battleConfig.count, text3);
	}

	private string GetDescription_Shopping()
	{
		string text = "Get Description Shopping...\n";
		if (buyConfig.toyType == ToyType.Figure)
		{
			string arg = "<color=#fa8efe>" + Language.Get(GameManager.instance.npcDatabase.GetNPCDef(missionLocationNPC).nameKey, "NPCS,FIGS") + "</color>";
			switch (buyConfig.figureConfig.type)
			{
			case MissionTypeConfigBuy.FigureConfig.Type.Class:
			{
				CharacterClass charClass = (CharacterClass)buyConfig.figureConfig.validUids[0];
				return string.Format(Language.Get("DAILY_BUY_DELIVER_FIG_MULTI"), buyConfig.count, "<color=#7ad5ff>" + GetClassSpriteText(charClass) + "</color> " + Language.Get("FIGS", "MENUS,GUI"), arg);
			}
			case MissionTypeConfigBuy.FigureConfig.Type.Tag:
			{
				TagItem tagItem = GameManager.instance.tagCollection.GetTagItem(buyConfig.figureConfig.validTags[0]);
				string arg2 = "<color=#7ad5ff>" + Language.Get((buyConfig.count != 1) ? tagItem.displayTextPlural.key : tagItem.displayTextSingular.key, "NPCS,FIGS") + "</color> " + Language.Get("FIGS", "MENUS,GUI");
				return string.Format(Language.Get("DAILY_BUY_DELIVER_FIG_MULTI"), buyConfig.count, arg2, arg);
			}
			}
		}
		return "Description Needed";
	}

	public DailyMissionData Copy()
	{
		DailyMissionData dailyMissionData = new DailyMissionData();
		dailyMissionData.uid = uid;
		dailyMissionData.type = type;
		dailyMissionData.missionLocationNPC = missionLocationNPC;
		dailyMissionData.battleConfig = ((battleConfig == null) ? null : battleConfig.Copy());
		dailyMissionData.buyConfig = ((buyConfig == null) ? null : buyConfig.Copy());
		dailyMissionData.battleCondition = ((battleCondition == null) ? null : battleCondition.Copy());
		if (figureOverrides != null)
		{
			dailyMissionData.figureOverrides = new TournamentRoundFigureData[figureOverrides.Length];
			for (int i = 0; i < figureOverrides.Length; i++)
			{
				dailyMissionData.figureOverrides[i] = figureOverrides[i].Copy();
			}
		}
		dailyMissionData.coinReward = coinReward;
		dailyMissionData.rainbowCoinReward = rainbowCoinReward;
		dailyMissionData.modChipReward = modChipReward;
		if (characterRewards != null)
		{
			dailyMissionData.characterRewards = new List<CharacterRewardData>(characterRewards.Count);
			for (int j = 0; j < characterRewards.Count; j++)
			{
				dailyMissionData.characterRewards.Add(characterRewards[j].Copy());
			}
		}
		dailyMissionData.difficulty = difficulty;
		dailyMissionData._startDateTime = _startDateTime;
		dailyMissionData._completedDateTime = _completedDateTime;
		return dailyMissionData;
	}
}
