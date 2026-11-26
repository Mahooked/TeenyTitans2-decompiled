using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RankUpBar : MonoBehaviour
{
	public static Dictionary<CityCharacterType, string> characterPortraits = new Dictionary<CityCharacterType, string>
	{
		{
			CityCharacterType.BeastBoy,
			"TT2_multi-portrait-BeastBoy"
		},
		{
			CityCharacterType.Cyborg,
			"TT2_multi-portrait-Cyborg"
		},
		{
			CityCharacterType.Raven,
			"TT2_multi-portrait-Raven"
		},
		{
			CityCharacterType.Robin,
			"TT2_multi-portrait-Robin"
		},
		{
			CityCharacterType.Starfire,
			"TT2_multi-portrait-Starfire"
		}
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnOpenPortraitSelectorClicked__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnPlayerNameBtnClicked__BackingField;

	public tk2dSlicedSprite barFillSprite;

	public tk2dSprite portrait;

	public tk2dUIItem portraitSelectorBtn;

	public tk2dSprite border;

	public tk2dSprite rankPlateSprite;

	public float barFillUpSpeed = 10f;

	public TextMeshPro levelTextMesh;

	public tk2dSprite rankUpRewardSprite;

	public Animator rankUpBarAnimator;

	public tk2dUIItem playerNameBtn;

	public TextMeshPro playerNameTextMesh;

	public ParticleAttractor particleAttractor;

	public tk2dSprite newPortraitSprite;

	[Header("Sound Effects")]
	public AudioClip fillSFX;

	private LegionOfDoomController lodController;

	private LegionOfDoomLevelTable levelTable;

	private LegionOfDoomSettings lodSettings;

	private PvpData pvpData;

	private float maxWidthBarFill;

	private Tweener fillTween;

	private Sequence bounceSequence;

	public event Action OnOpenPortraitSelectorClicked
	{
		add
		{
			Action action = OnOpenPortraitSelectorClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnOpenPortraitSelectorClicked__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnOpenPortraitSelectorClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnOpenPortraitSelectorClicked__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnPlayerNameBtnClicked
	{
		add
		{
			Action action = OnPlayerNameBtnClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerNameBtnClicked__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnPlayerNameBtnClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerNameBtnClicked__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		maxWidthBarFill = barFillSprite.dimensions.x;
	}

	private void Start()
	{
		lodController = LegionOfDoomController.instance;
		levelTable = GameManager.instance.legionOfDoomLevelTable;
		lodSettings = GameManager.instance.legionOfDoomSettings;
		pvpData = Player.instance.pvpData;
		if (string.IsNullOrEmpty(pvpData.PortraitSpriteName))
		{
			pvpData.PortraitSpriteName = characterPortraits[CityCharacter.characterType];
			Player.instance.Save();
		}
		UpdatePortraitBorder();
		pvpData.PlayerNameUpdated += HandlePlayerNameUpdated;
		pvpData.PortraitSpriteUpdated += HandlePortraitSpriteUpdated;
		pvpData.PortraitsViewedUpdated += HandlePortraitsViewedUpdated;
		pvpData.PreviousLevelUpdated += HandlePreviousLevelUpdated;
		pvpData.PreviousXPUpdated += HandlePreviousXPUpdated;
		newPortraitSprite.gameObject.SetActive(false);
		HandlePlayerNameUpdated();
		HandlePortraitSpriteUpdated();
		HandlePortraitsViewedUpdated();
		HandlePreviousLevelUpdated();
		UpdateFill(false);
		if (lodController != null)
		{
			lodController.PageUpdated += HandleLODControllerPageUpdated;
			lodController.portraitSelector.PortraitSelected += HandlePortraitSelected;
		}
		else
		{
			portraitSelectorBtn.gameObject.SetActive(false);
		}
		portraitSelectorBtn.OnClick += HandlePortraitSelectorBtnClicked;
		playerNameBtn.OnClick += HandlePlayerNameClicked;
		playerNameBtn.enabled = !string.IsNullOrEmpty(pvpData.PlayerName);
	}

	private void OnDestroy()
	{
		if (pvpData != null)
		{
			pvpData.PlayerNameUpdated -= HandlePlayerNameUpdated;
			pvpData.PortraitSpriteUpdated -= HandlePortraitSpriteUpdated;
			pvpData.PortraitsViewedUpdated -= HandlePortraitsViewedUpdated;
			pvpData.PreviousLevelUpdated -= HandlePreviousLevelUpdated;
			pvpData.PreviousXPUpdated -= HandlePreviousXPUpdated;
		}
		if (lodController != null)
		{
			lodController.portraitSelector.PortraitSelected -= HandlePortraitSelected;
		}
		portraitSelectorBtn.OnClick -= HandlePortraitSelectorBtnClicked;
		playerNameBtn.OnClick -= HandlePlayerNameClicked;
	}

	private void HandlePlayerNameUpdated()
	{
		playerNameTextMesh.text = pvpData.PlayerName;
		playerNameBtn.enabled = !string.IsNullOrEmpty(pvpData.PlayerName);
	}

	private void HandlePortraitSpriteUpdated()
	{
		portrait.SetSprite(pvpData.PortraitSpriteName);
	}

	private void HandlePreviousLevelUpdated()
	{
		levelTextMesh.text = pvpData.PreviousLevel.ToString();
		string sprite = string.Empty;
		LegionOfDoomLevelData levelData = levelTable.GetLevelData(pvpData.PreviousLevel + 1);
		int num = levelData.rewards.Length;
		for (int i = 0; i < num; i++)
		{
			PlayerReward playerReward = levelData.rewards[i];
			switch (playerReward.type)
			{
			case PlayerRewardType.Figure:
				sprite = ((!playerReward.figure.goldBox) ? "multiplayermenu_levelbar_reward_box" : "multiplayermenu_levelbar_reward_boxgold");
				break;
			case PlayerRewardType.Currency:
				sprite = "multiplayermenu_levelbar_reward_rainbowcoins";
				break;
			case PlayerRewardType.Repaint:
				sprite = "multiplayermenu_levelbar_reward_egg";
				break;
			default:
				sprite = "multiplayermenu_levelbar_reward_box";
				break;
			}
		}
		rankUpRewardSprite.SetSprite(sprite);
		UpdatePortraitBorder();
	}

	private void HandlePreviousXPUpdated()
	{
	}

	private void HandleLODControllerPageUpdated(LegionOfDoomController.PageName pageName)
	{
		bool flag = pageName == LegionOfDoomController.PageName.RankUp;
		portraitSelectorBtn.enabled = !flag;
		portraitSelectorBtn.GetComponent<tk2dSprite>().color = ((!flag) ? Color.white : Color.gray);
		playerNameBtn.enabled = !flag && !string.IsNullOrEmpty(pvpData.PlayerName);
	}

	private void HandlePortraitSelected()
	{
		portrait.SetSprite(pvpData.PortraitSpriteName);
	}

	private void HandlePortraitsViewedUpdated()
	{
		if (Player.instance.pvpData.AllPortraitsViewed)
		{
			if (newPortraitSprite.gameObject.activeInHierarchy)
			{
				newPortraitSprite.transform.DOKill();
				newPortraitSprite.transform.DOScale(0f, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
				{
					newPortraitSprite.gameObject.SetActive(false);
				})
					.easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude * 4f;
			}
		}
		else if (!newPortraitSprite.gameObject.activeInHierarchy)
		{
			newPortraitSprite.gameObject.SetActive(true);
			newPortraitSprite.transform.DOKill();
			newPortraitSprite.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude * 4f;
			newPortraitSprite.transform.DOShakeRotation(0.2f, new Vector3(0f, 0f, 90f), 50).SetDelay(0.125f);
		}
	}

	private void HandlePortraitSelectorBtnClicked()
	{
		if (OnOpenPortraitSelectorClicked__BackingField != null)
		{
			OnOpenPortraitSelectorClicked__BackingField();
		}
	}

	private void HandlePlayerNameClicked()
	{
		if (OnPlayerNameBtnClicked__BackingField != null)
		{
			OnPlayerNameBtnClicked__BackingField();
		}
	}

	public YieldInstruction UpdateFill(bool useTween, bool playFillSFX = true)
	{
		float num = Mathf.Clamp01((float)pvpData.PreviousXP / (float)levelTable.GetLevelData(pvpData.PreviousLevel).xpToLevelUp);
		Vector2 vector = new Vector2(maxWidthBarFill * num, barFillSprite.dimensions.y);
		YieldInstruction result = null;
		if (useTween)
		{
			if (fillTween != null)
			{
				fillTween.Kill();
			}
			fillTween = DOTween.To(() => barFillSprite.dimensions, (Vector2 x) =>
			{
				barFillSprite.dimensions = x;
			}, vector, barFillUpSpeed).SetEase(Ease.OutSine).SetSpeedBased()
				.OnUpdate(UpdateParticleAttractorPos);
			fillTween.ForceInit();
			result = fillTween.WaitForCompletion();
			if (playFillSFX && !Mathf.Approximately(vector.x, barFillSprite.dimensions.x))
			{
				GFAudioManager.instance.PlayClip(fillSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			}
		}
		else
		{
			barFillSprite.dimensions = vector;
			UpdateParticleAttractorPos();
		}
		return result;
	}

	private void UpdateParticleAttractorPos()
	{
		particleAttractor.transform.position = barFillSprite.transform.position + new Vector3(barFillSprite.dimensions.x, 0f);
	}

	public void AttractParticleSystem(ParticleSystem particleSystem)
	{
		particleAttractor.AttractParticleSystem(particleSystem);
	}

	private void UpdatePortraitBorder()
	{
		border.SetSprite(lodSettings.GetBorderSpriteForRank(pvpData.level));
		rankPlateSprite.SetSprite(lodSettings.GetNumberPlateSpriteForRank(pvpData.level));
	}

	public void BounceRewardSprite()
	{
		if (bounceSequence == null)
		{
			bounceSequence = DOTween.Sequence();
			bounceSequence.Append(rankUpRewardSprite.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 3f, 3, 0.3f));
			bounceSequence.Append(DOTween.To(() => rankUpRewardSprite.scale, (Vector3 x) =>
			{
				rankUpRewardSprite.scale = x;
			}, Vector3.one * 1.4f, 0.4f).SetEase(Ease.Linear).SetLoops(100, LoopType.Yoyo));
		}
	}

	public void KillRewardSpriteBounce()
	{
		if (bounceSequence != null)
		{
			bounceSequence.Kill(true);
			bounceSequence = null;
			rankUpRewardSprite.transform.localScale = Vector3.one;
		}
	}

	public void BumpLevelPlate()
	{
		rankPlateSprite.transform.DOKill();
		rankPlateSprite.transform.localScale = Vector3.one * 1.2f;
		rankPlateSprite.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBounce);
	}
}
