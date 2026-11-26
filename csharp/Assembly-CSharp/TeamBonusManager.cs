using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamBonusManager : MonoBehaviour
{
	public GameObject characterLeftBonusGrp;

	public GameObject characterCenterBonusGrp;

	public GameObject characterRightBonusGrp;

	public GameObject teamBonusNameGroup;

	public TextMeshPro teamBonusNameText;

	public TextMeshPro characterLeftBonusText;

	public TextMeshPro characterCenterBonusText;

	public TextMeshPro characterRightBonusText;

	public tk2dSprite teamBackingSprite;

	public tk2dSprite statSpriteLeft;

	public tk2dSprite statSpriteCenter;

	public tk2dSprite statSpriteRight;

	private Player player;

	private TeamStatBonusDatabase _teamStatBonusDatabase;

	public PreBattleArrow leftArrow { get; set; }

	public PreBattleArrow centerArrow { get; set; }

	public PreBattleArrow rightArrow { get; set; }

	private TeamStatBonusDatabase teamStatBonusDatabase
	{
		get
		{
			if (_teamStatBonusDatabase == null)
			{
				_teamStatBonusDatabase = GameManager.instance.teamStatBonusDatabase;
			}
			return _teamStatBonusDatabase;
		}
	}

	private void Awake()
	{
		teamBonusNameGroup.SetActive(value: false);
		characterLeftBonusGrp.SetActive(value: false);
		characterCenterBonusGrp.SetActive(value: false);
		characterRightBonusGrp.SetActive(value: false);
	}

	private void Start()
	{
		player = Player.instance;
		player.TeamChanged += CheckActiveParty;
	}

	private void OnDestroy()
	{
		if (player != null)
		{
			player.TeamChanged -= CheckActiveParty;
		}
	}

	private void CheckActiveParty()
	{
		StopCoroutine("CheckActivePartyRoutine");
		StartCoroutine("CheckActivePartyRoutine");
	}

	private IEnumerator CheckActivePartyRoutine()
	{
		yield return new WaitForEndOfFrame();
		if (PreBattleController.battleDefinition == null)
		{
			yield break;
		}
		if (ChallengeModeManager.isChallengeModeActive && ChallengeModeManager.instance != null && ChallengeModeManager.modeDefinition != null && ChallengeModeManager.modeDefinition.challengeModeType == ChallengeModeType.SurpriseSquad)
		{
			characterLeftBonusGrp.SetActive(value: false);
			characterCenterBonusGrp.SetActive(value: false);
			characterRightBonusGrp.SetActive(value: false);
			teamBonusNameGroup.SetActive(value: false);
		}
		else if (player.activeTeam.Count > 1)
		{
			int num = -1;
			int num2 = 0;
			int num3 = -1;
			bool flag = false;
			bool flag2 = characterLeftBonusGrp.activeSelf && characterCenterBonusGrp.activeSelf && characterRightBonusGrp.activeSelf;
			for (int i = 0; i < teamStatBonusDatabase.items.Count; i++)
			{
				List<int> list = new List<int>();
				TeamStatBonusDefinition teamStatBonusDefinition = teamStatBonusDatabase.items[i];
				for (int j = 0; j < player.activeTeam.Count; j++)
				{
					Character character = player.activeTeam[j];
					int uid = character.characterDefinition.uid;
					if (teamStatBonusDefinition.characterUIDS.Contains(uid) && !list.Contains(uid))
					{
						list.Add(uid);
					}
				}
				if (list.Count > 1 && num != teamStatBonusDefinition.uid && (list.Count > num2 || (teamStatBonusDefinition.characterUIDS.Count == 2 && list.Count == 2 && !flag) || (num2 == 2 && list.Count == 2 && teamStatBonusDefinition.priority > num3) || (num2 == 3 && list.Count == 3 && teamStatBonusDefinition.priority > num3)))
				{
					if (list.Count == 3)
					{
						flag = true;
					}
					num2 = list.Count;
					num = teamStatBonusDefinition.uid;
					num3 = teamStatBonusDefinition.priority;
				}
			}
			if (num > -1)
			{
				TeamStatBonusDefinition item = teamStatBonusDatabase.GetItem(num);
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				if (PreBattleController.battleDefinition != null)
				{
					PreBattleController.battleDefinition.playerCharactersTeamBoosted.Clear();
				}
				for (int k = 0; k < item.characterUIDS.Count; k++)
				{
					int num4 = item.characterUIDS[k];
					if (leftArrow.character != null && leftArrow.character.characterDefinition.uid == num4)
					{
						flag3 = true;
						characterLeftBonusGrp.SetActive(value: true);
						if (PreBattleController.battleDefinition != null && !PreBattleController.battleDefinition.playerCharactersTeamBoosted.Contains(leftArrow.character))
						{
							PreBattleController.battleDefinition.playerCharactersTeamBoosted.Add(leftArrow.character);
						}
					}
					else if (centerArrow.character != null && centerArrow.character.characterDefinition.uid == num4)
					{
						flag4 = true;
						characterCenterBonusGrp.SetActive(value: true);
						if (PreBattleController.battleDefinition != null && !PreBattleController.battleDefinition.playerCharactersTeamBoosted.Contains(centerArrow.character))
						{
							PreBattleController.battleDefinition.playerCharactersTeamBoosted.Add(centerArrow.character);
						}
					}
					else if (rightArrow.character != null && rightArrow.character.characterDefinition.uid == num4)
					{
						flag5 = true;
						characterRightBonusGrp.SetActive(value: true);
						if (PreBattleController.battleDefinition != null && !PreBattleController.battleDefinition.playerCharactersTeamBoosted.Contains(rightArrow.character))
						{
							PreBattleController.battleDefinition.playerCharactersTeamBoosted.Add(rightArrow.character);
						}
					}
				}
				if (flag && !flag2)
				{
					characterLeftBonusGrp.SetActive(value: false);
					characterCenterBonusGrp.SetActive(value: false);
					characterRightBonusGrp.SetActive(value: false);
					characterLeftBonusGrp.SetActive(value: true);
					characterCenterBonusGrp.SetActive(value: true);
					characterRightBonusGrp.SetActive(value: true);
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_teambonusappear", GFAudioManager.SFX_GROUP, string.Empty);
				}
				if (!teamBonusNameGroup.activeSelf)
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_teambonusappear", GFAudioManager.SFX_GROUP, string.Empty);
				}
				if (!flag3)
				{
					characterLeftBonusGrp.SetActive(value: false);
				}
				if (!flag4)
				{
					characterCenterBonusGrp.SetActive(value: false);
				}
				if (!flag5)
				{
					characterRightBonusGrp.SetActive(value: false);
				}
				PreBattleController.battleDefinition.playerStatTeamBoosted = item.statType;
				teamBonusNameGroup.SetActive(value: true);
				teamBonusNameText.text = Language.Get(item.teamNameKey, "MENUS,GUI");
				teamBonusNameText.faceColor = item.fontFaceColor;
				teamBonusNameText.outlineColor = item.fontOutlineColor;
				teamBackingSprite.SetSprite(item.teamBackingSpriteName);
				if (item.statType == CharacterStatType.Attack)
				{
					statSpriteLeft.SetSprite("menu_upgrade_icon_strength");
					statSpriteCenter.SetSprite("menu_upgrade_icon_strength");
					statSpriteRight.SetSprite("menu_upgrade_icon_strength");
				}
				else if (item.statType == CharacterStatType.HitPoints)
				{
					statSpriteLeft.SetSprite("menu_upgrade_icon_health");
					statSpriteCenter.SetSprite("menu_upgrade_icon_health");
					statSpriteRight.SetSprite("menu_upgrade_icon_health");
				}
				else if (item.statType == CharacterStatType.Luck)
				{
					statSpriteLeft.SetSprite("menu_upgrade_icon_luck");
					statSpriteCenter.SetSprite("menu_upgrade_icon_luck");
					statSpriteRight.SetSprite("menu_upgrade_icon_luck");
				}
				else if (item.statType == CharacterStatType.Dodge)
				{
					statSpriteLeft.SetSprite("menu_upgrade_icon_speed");
					statSpriteCenter.SetSprite("menu_upgrade_icon_speed");
					statSpriteRight.SetSprite("menu_upgrade_icon_speed");
				}
				if (num2 == 2)
				{
					PreBattleController.battleDefinition.playerStatTeamBoostAmount = item.statBonus1;
					characterLeftBonusText.text = "+" + item.statBonus1 + "\n";
					characterCenterBonusText.text = "+" + item.statBonus1 + "\n";
					characterRightBonusText.text = "+" + item.statBonus1 + "\n";
				}
				if (num2 >= 3)
				{
					PreBattleController.battleDefinition.playerStatTeamBoostAmount = item.statBonus2;
					characterLeftBonusText.text = "+" + item.statBonus2 + "\n";
					characterCenterBonusText.text = "+" + item.statBonus2 + "\n";
					characterRightBonusText.text = "+" + item.statBonus2 + "\n";
				}
			}
			else
			{
				if (PreBattleController.battleDefinition != null)
				{
					PreBattleController.battleDefinition.playerCharactersTeamBoosted.Clear();
					PreBattleController.battleDefinition.playerStatTeamBoostAmount = 0;
				}
				characterLeftBonusGrp.SetActive(value: false);
				characterCenterBonusGrp.SetActive(value: false);
				characterRightBonusGrp.SetActive(value: false);
				teamBonusNameGroup.SetActive(value: false);
			}
		}
		else
		{
			if (PreBattleController.battleDefinition != null)
			{
				PreBattleController.battleDefinition.playerCharactersTeamBoosted.Clear();
				PreBattleController.battleDefinition.playerStatTeamBoostAmount = 0;
			}
			characterLeftBonusGrp.SetActive(value: false);
			characterCenterBonusGrp.SetActive(value: false);
			characterRightBonusGrp.SetActive(value: false);
			teamBonusNameGroup.SetActive(value: false);
		}
	}

	private string LocalizationForStatType(CharacterStatType statType)
	{
		return statType switch
		{
			CharacterStatType.Attack => Language.Get("STRENGTH", "MENUS,GUI"), 
			CharacterStatType.Dodge => Language.Get("DODGESTAT", "MENUS,GUI"), 
			CharacterStatType.HitPoints => Language.Get("HEALTH", "MENUS,GUI"), 
			CharacterStatType.Luck => Language.Get("LUCK", "MENUS,GUI"), 
			CharacterStatType.Speed => Language.Get("SPEED", "MENUS,GUI"), 
			_ => string.Empty, 
		};
	}
}
