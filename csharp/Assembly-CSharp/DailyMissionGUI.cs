using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyMissionGUI : MonoBehaviour, IPoolable
{
	[Serializable]
	public class ColorSettings
	{
		public Color flatTextFaceColor;

		public Color descriptionTextFaceColor = Color.white;

		public Color descriptionTextOutlineColor;

		public Color descriptionBGTintColor = Color.white;

		public Color rewardBGTintColor = Color.white;

		public Color missionIconTintColor = Color.white;

		public Color difficultyStarTintColor = Color.white;
	}

	private static Dictionary<CityName, string> headerSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_jumpcity"
		},
		{
			CityName.Gotham,
			"menu_dailymission_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_metropolis"
		}
	};

	private static Dictionary<CityName, string> bgSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_bg"
		},
		{
			CityName.Gotham,
			"menu_dailymission_bg_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_bg_metropolis"
		}
	};

	private static Dictionary<CityName, string> bgTextureSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_bg_texture"
		},
		{
			CityName.Gotham,
			"menu_dailymission_bg_texture_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_bg_texture_metropolis"
		}
	};

	private static Dictionary<CityName, string> separatorSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_line"
		},
		{
			CityName.Gotham,
			"menu_dailymission_line_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_line_metropolis"
		}
	};

	private static Dictionary<CityName, string> bottomSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_tabs"
		},
		{
			CityName.Gotham,
			"menu_dailymission_bottom_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_bottom_metropolis"
		}
	};

	private static Dictionary<CityName, string> bottomBackingSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_tabs"
		},
		{
			CityName.Gotham,
			"menu_dailymission_bg_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_bg_metropolis"
		}
	};

	private static Dictionary<CityName, string> arrowSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_arrow"
		},
		{
			CityName.Gotham,
			"menu_dailymission_arrow_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_arrow_metropolis"
		}
	};

	private static Dictionary<CityName, string> textBgSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_textbg"
		},
		{
			CityName.Gotham,
			"menu_dailymission_textbg_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_textbg_metropolis"
		}
	};

	private static Dictionary<CityName, string> starSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"menu_dailymission_star"
		},
		{
			CityName.Gotham,
			"menu_dailymission_star_gotham"
		},
		{
			CityName.Metropolis,
			"menu_dailymission_star_metropolis"
		}
	};

	private static Dictionary<DailyMissionType, string> missionTypeSprites = new Dictionary<DailyMissionType, string>
	{
		{
			DailyMissionType.DefeatNPC,
			"menu_dailymission_missionicon_vs"
		},
		{
			DailyMissionType.DefeatNPCType,
			"menu_dailymission_missionicon_vs"
		},
		{
			DailyMissionType.Shopping,
			"menu_dailymission_missionicon_shop"
		},
		{
			DailyMissionType.ScavengerHunt,
			"menu_dailymission_missionicon_shop"
		}
	};

	public tk2dSprite headerIcon;

	public tk2dSprite missionTypeIcon;

	public tk2dSprite arrowSpriteLeft;

	public tk2dSprite arrowSpriteRight;

	public tk2dSprite bottomSprite;

	public tk2dSlicedSprite bottomBGSprite;

	public tk2dSlicedSprite bgSprite;

	public tk2dSlicedSprite bgTextureSprite;

	public tk2dSlicedSprite separatorSprite;

	public tk2dSlicedSprite missionBackingSprite;

	public tk2dSlicedSprite rewardBackingSprite;

	public TextMeshPro headerText;

	public TextMeshPro missionTypeText;

	public TextMeshPro missionOrdersText;

	public TextMeshPro rewardText;

	public TextMeshPro labelDiffText;

	public TextMeshPro labelRewardText;

	public TextMeshPro comeBackText;

	public TextMeshPro resetText;

	public TextMeshPro timerText;

	public tk2dUIItem confirmButton;

	public tk2dUIItem cancelButton;

	public tk2dUIItem progressButton;

	public tk2dSprite difficultyStar1;

	public tk2dSprite difficultyStar2;

	public tk2dSprite difficultyStar3;

	public ColorSettings jumpCityColors;

	public ColorSettings gothamColors;

	public ColorSettings metropolisColors;

	[Header("Sound Effects")]
	public AudioClip openCloseSFX;

	[Header("DEBUG")]
	public tk2dUIItem debugDailyMissionsToggleBtn;

	public tk2dUIItem debugGenerateMission;

	public tk2dUIItem debugFastForwardHour;

	public tk2dUIItem debugFastForwardMin;

	public tk2dUIItem debugFastForwardSec;

	private TextMeshPro debugDailyMissionsText;

	private DailyMissionData _missionData;

	public DailyMissionData missionData => _missionData;

	private void Awake()
	{
		Refresh();
	}

	private void OnEnable()
	{
		confirmButton.OnClick += HandleConfirmClick;
		cancelButton.OnClick += HandleCancelClick;
		progressButton.OnClick += HandleProgressClicked;
		StopCoroutine("UpdateRemainingTimeRoutine");
		StartCoroutine("UpdateRemainingTimeRoutine");
		if (CityController.instance != null)
		{
			CityController.instance.cityCharacter.AllowInput = false;
			CityController.instance.communicator.MissionScrollerWillAppear += HandleMissionScrollerWillAppear;
		}
		if (DailyMissionManager.instance != null)
		{
			DailyMissionManager instance = DailyMissionManager.instance;
			instance.DailyMissionGenerated = (Action)Delegate.Combine(instance.DailyMissionGenerated, new Action(Refresh));
		}
	}

	private void OnDisable()
	{
		confirmButton.OnClick -= HandleConfirmClick;
		cancelButton.OnClick -= HandleCancelClick;
		progressButton.OnClick -= HandleProgressClicked;
		if (CityController.instance != null)
		{
			CityController.instance.cityCharacter.AllowInput = true;
			CityController.instance.communicator.MissionScrollerWillAppear -= HandleMissionScrollerWillAppear;
		}
		if (DailyMissionManager.instance != null)
		{
			DailyMissionManager instance = DailyMissionManager.instance;
			instance.DailyMissionGenerated = (Action)Delegate.Remove(instance.DailyMissionGenerated, new Action(Refresh));
		}
	}

	private void Start()
	{
		debugDailyMissionsText = debugDailyMissionsToggleBtn.GetComponentInChildren<TextMeshPro>();
		debugDailyMissionsToggleBtn.OnClick += HandleDebugDailyMissionsToggle;
		debugGenerateMission.OnClick += HandleDebugGenerateMissionClicked;
		debugFastForwardHour.OnClick += HandleDebugFastForwardHour;
		debugFastForwardMin.OnClick += HandleDebugFastForwardMin;
		debugFastForwardSec.OnClick += HandleDebugFastForwardSec;
	}

	private void OnDestroy()
	{
		debugDailyMissionsToggleBtn.OnClick -= HandleDebugDailyMissionsToggle;
		debugGenerateMission.OnClick -= HandleDebugGenerateMissionClicked;
		debugFastForwardHour.OnClick -= HandleDebugFastForwardHour;
		debugFastForwardMin.OnClick -= HandleDebugFastForwardMin;
		debugFastForwardSec.OnClick -= HandleDebugFastForwardSec;
	}

	private void HandleMissionScrollerWillAppear()
	{
		HandleCancelClick();
	}

	public void Init()
	{
		GFAudioManager.instance.PlayClip(openCloseSFX, 1f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
		Refresh();
	}

	private void Refresh()
	{
		Debug.Log("Refresh Mission GUI...");
		_missionData = DailyMissionManager.instance.currentDailyMission;
		if (_missionData != null)
		{
			CityName cityName = CityController.instance.cityName;
			tk2dSprite tk2dSprite2 = headerIcon;
			Color white = Color.white;
			missionTypeIcon.color = white;
			tk2dSprite2.color = white;
			headerIcon.SetSprite(headerSprites[cityName]);
			bgSprite.SetSprite(bgSprites[cityName]);
			bgTextureSprite.SetSprite(bgTextureSprites[cityName]);
			separatorSprite.SetSprite(separatorSprites[cityName]);
			arrowSpriteLeft.SetSprite(arrowSprites[cityName]);
			arrowSpriteRight.SetSprite(arrowSprites[cityName]);
			difficultyStar1.SetSprite(starSprites[cityName]);
			difficultyStar2.SetSprite(starSprites[cityName]);
			difficultyStar3.SetSprite(starSprites[cityName]);
			missionOrdersText.text = _missionData.description;
			rewardText.text = GetRewardText();
			missionBackingSprite.SetSprite(textBgSprites[cityName]);
			rewardBackingSprite.SetSprite(textBgSprites[cityName]);
			bottomSprite.SetSprite(bottomSprites[cityName]);
			bottomSprite.color = ((cityName != CityName.JumpCity) ? Color.white : Color.clear);
			bottomBGSprite.SetSprite(bottomBackingSprites[cityName]);
			confirmButton.gameObject.SetActive(!Player.instance.HasActiveMission(_missionData.uid));
			ColorSettings colorSettings = cityName switch
			{
				CityName.JumpCity => jumpCityColors, 
				CityName.Metropolis => metropolisColors, 
				_ => gothamColors, 
			};
			missionTypeIcon.SetSprite(missionTypeSprites[_missionData.type] + ((cityName != CityName.Gotham && cityName != CityName.Metropolis) ? string.Empty : ("_" + cityName.ToString().ToLower())));
			missionBackingSprite.color = colorSettings.descriptionBGTintColor;
			rewardBackingSprite.color = colorSettings.rewardBGTintColor;
			headerText.faceColor = colorSettings.flatTextFaceColor;
			labelDiffText.faceColor = colorSettings.flatTextFaceColor;
			labelRewardText.faceColor = colorSettings.flatTextFaceColor;
			rewardText.faceColor = colorSettings.descriptionTextFaceColor;
			rewardText.outlineColor = colorSettings.descriptionTextOutlineColor;
			missionOrdersText.faceColor = colorSettings.descriptionTextFaceColor;
			missionOrdersText.outlineColor = colorSettings.descriptionTextOutlineColor;
			missionTypeIcon.color = colorSettings.missionIconTintColor;
			tk2dSprite tk2dSprite3 = difficultyStar1;
			white = colorSettings.difficultyStarTintColor;
			difficultyStar3.color = white;
			white = white;
			difficultyStar2.color = white;
			tk2dSprite3.color = white;
			if (_missionData.difficulty <= 1)
			{
				difficultyStar1.gameObject.SetActive(value: true);
				difficultyStar2.gameObject.SetActive(value: false);
				difficultyStar3.gameObject.SetActive(value: false);
			}
			else if (_missionData.difficulty == 2)
			{
				difficultyStar1.gameObject.SetActive(value: true);
				difficultyStar2.gameObject.SetActive(value: true);
				difficultyStar3.gameObject.SetActive(value: false);
			}
			else
			{
				difficultyStar1.gameObject.SetActive(value: true);
				difficultyStar2.gameObject.SetActive(value: true);
				difficultyStar3.gameObject.SetActive(value: true);
			}
			if (DailyMissionManager.instance.IsDailyMissionComplete())
			{
				comeBackText.gameObject.SetActive(value: true);
				confirmButton.gameObject.SetActive(value: false);
				missionOrdersText.text = string.Empty;
				missionOrdersText.renderer.enabled = false;
				missionTypeIcon.gameObject.SetActive(value: false);
				difficultyStar1.gameObject.SetActive(value: false);
				difficultyStar2.gameObject.SetActive(value: false);
				difficultyStar3.gameObject.SetActive(value: false);
				rewardText.text = string.Empty;
				labelDiffText.renderer.enabled = false;
				labelRewardText.renderer.enabled = false;
			}
			else
			{
				comeBackText.gameObject.SetActive(value: false);
				missionOrdersText.renderer.enabled = true;
				missionTypeIcon.gameObject.SetActive(value: true);
				labelDiffText.renderer.enabled = true;
				labelRewardText.renderer.enabled = true;
			}
			StopCoroutine("UpdateRemainingTimeRoutine");
			StartCoroutine("UpdateRemainingTimeRoutine");
		}
		else
		{
			tk2dSprite tk2dSprite4 = headerIcon;
			Color white = Color.clear;
			missionTypeIcon.color = white;
			tk2dSprite4.color = white;
			missionTypeText.text = string.Empty;
			missionOrdersText.text = string.Empty;
			rewardText.text = string.Empty;
			timerText.text = string.Empty;
		}
	}

	private IEnumerator UpdateRemainingTimeRoutine()
	{
		while (true)
		{
			RefreshRemainingTime();
			yield return new WaitForSeconds(1f);
		}
	}

	private void RefreshRemainingTime()
	{
		if (DailyMissionManager.instance.currentDailyMission != null)
		{
			TimeSpan timeSpanRemaining = DailyMissionManager.instance.currentDailyMission.GetTimeSpanRemaining();
			int num = (int)timeSpanRemaining.TotalHours;
			int num2 = timeSpanRemaining.Minutes;
			int num3 = timeSpanRemaining.Seconds;
			if (num < 0 && num2 <= 0 && num3 <= 0)
			{
				num = 0;
				num2 = 0;
				num3 = 0;
			}
			timerText.text = Language.Get("DAILY_MISSION_TIME_REMAINING", "MENUS,GUI") + " " + num.ToString("00") + ":" + num2.ToString("00") + ":" + num3.ToString("00");
		}
	}

	private string GetRewardText()
	{
		if (_missionData.coinReward > 0)
		{
			return $"{_missionData.coinReward}<sprite name=\"icon_currency_coin\">";
		}
		if (_missionData.rainbowCoinReward > 0)
		{
			return $"{_missionData.rainbowCoinReward}<sprite name=\"icon_currency_rainbowCoin\">";
		}
		if (_missionData.modChipReward > -1)
		{
			return string.Format("{0}", Language.Get(GameManager.instance.itemDatabase.GetItem(_missionData.modChipReward).itemNameKey, "ABILS,CHIPS,ACHVS,LDBS"));
		}
		if (_missionData.characterRewards.Count > 0)
		{
			return string.Format("{0}", Language.Get(GameManager.instance.characterDatabase.GetCharacter(_missionData.characterRewards[0].characterUID).characterNameKey, "NPCS,FIGS"));
		}
		return string.Empty;
	}

	private void HandleConfirmClick()
	{
		GameManager.instance.communicatorDatabase.AddDailyMission(_missionData);
		Player.instance.AddMission(_missionData.uid, isDaily: true);
		CityController.instance.communicator.ActivateMissions();
		CityController.instance.communicator.OnClickMissions();
		ObjectPool.PoolObject(gameObject);
	}

	public void ForceClose()
	{
		HandleCancelClick();
	}

	private void HandleCancelClick()
	{
		GFAudioManager.instance.PlayClip(openCloseSFX, 1f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
		ObjectPool.PoolObject(gameObject);
	}

	private void HandleProgressClicked()
	{
		Debug.Log("Progress button clicked");
		DailyMissionProgressMenu.instance.Show(openAsSubmenu: true);
		DailyMissionProgressMenu.instance.Closed -= HandleProgressMenuClosed;
		DailyMissionProgressMenu.instance.Closed += HandleProgressMenuClosed;
		cancelButton.enabled = false;
		confirmButton.enabled = false;
		progressButton.gameObject.SetActive(value: false);
		debugDailyMissionsToggleBtn.gameObject.SetActive(value: false);
		debugGenerateMission.gameObject.SetActive(value: false);
		debugFastForwardHour.gameObject.SetActive(value: false);
		debugFastForwardMin.gameObject.SetActive(value: false);
		debugFastForwardSec.gameObject.SetActive(value: false);
	}

	private void HandleProgressMenuClosed()
	{
		DailyMissionProgressMenu.instance.Closed -= HandleProgressMenuClosed;
		cancelButton.enabled = true;
		confirmButton.enabled = true;
		progressButton.gameObject.SetActive(value: true);
	}

	private void HandleDebugDailyMissionsToggle()
	{
		if (DailyMissionManager.debugMissions)
		{
			DailyMissionManager.debugMissions = false;
			debugDailyMissionsText.text = "Debug Random Missions:OFF";
		}
		else
		{
			DailyMissionManager.debugMissions = true;
			debugDailyMissionsText.text = "Debug Random Missions:ON";
		}
	}

	private void HandleDebugGenerateMissionClicked()
	{
		DailyMissionManager.instance.ForceGenerateMission();
		Refresh();
	}

	private void HandleDebugFastForwardHour()
	{
		DailyMissionManager.debugTimeOffset += DateTime.Now.AddHours(1.0) - DateTime.Now;
		RefreshRemainingTime();
	}

	private void HandleDebugFastForwardMin()
	{
		DailyMissionManager.debugTimeOffset += DateTime.Now.AddMinutes(1.0) - DateTime.Now;
		RefreshRemainingTime();
	}

	private void HandleDebugFastForwardSec()
	{
		DailyMissionManager.debugTimeOffset += DateTime.Now.AddSeconds(1.0) - DateTime.Now;
		RefreshRemainingTime();
	}

	public void ResetObject()
	{
	}
}
