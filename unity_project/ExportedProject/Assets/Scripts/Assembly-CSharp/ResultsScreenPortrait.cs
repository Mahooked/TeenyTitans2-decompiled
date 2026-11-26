using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ResultsScreenPortrait : MonoBehaviour
{
	private static Dictionary<CharacterClass, string> levelBarSprites = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"icon_lvlbar_beast"
		},
		{
			CharacterClass.Cute,
			"icon_lvlbar_cute"
		},
		{
			CharacterClass.DarkArts,
			"icon_lvlbar_darkarts"
		},
		{
			CharacterClass.MartialArts,
			"icon_lvlbar_martialarts"
		},
		{
			CharacterClass.None,
			"icon_lvlbar_super"
		},
		{
			CharacterClass.Super,
			"icon_lvlbar_super"
		},
		{
			CharacterClass.Tech,
			"icon_lvlbar_tech"
		}
	};

	public tk2dSprite portraitSprite;

	public tk2dSprite portraitWhiteSprite;

	public tk2dSlicedSprite levelBarBaseSprite;

	public tk2dSlicedSpriteHelper levelBarFillSprite;

	public TextMeshPro levelText;

	public TextMeshPro levelNumberText;

	public TextMeshPro xpRewardText;

	public GameObject levelUpGroup;

	public float maxBarWidth = 54f;

	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	private int xpAmount;

	private bool showLevelUp;

	private Character _character;

	public Character character
	{
		get
		{
			return _character;
		}
	}

	private void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleEventTriggered;
	}

	private void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleEventTriggered;
		levelUpGroup.SetActive(false);
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Leveled -= HandleLeveled;
			_character.GainedXP -= HandleGainedXP;
		}
	}

	public void Init(Character character, int xp)
	{
		if (character == _character)
		{
			return;
		}
		if (_character != null)
		{
			_character.Leveled -= HandleLeveled;
			_character.GainedXP -= HandleGainedXP;
		}
		xpAmount = xp;
		xpRewardText.text = string.Format("+{0}" + Language.Get("XP", "MENUS,GUI"), xpAmount);
		showLevelUp = false;
		_character = character;
		if (_character != null)
		{
			if (_character.level < _character.levelTable.maxLevel)
			{
				_character.Leveled += HandleLeveled;
				_character.GainedXP += HandleGainedXP;
			}
			else
			{
				xpAmount = 0;
				xpRewardText.text = string.Empty;
			}
			UpdateView();
		}
	}

	private void RefreshXPBar()
	{
		levelBarFillSprite.width = ((!_character.isMaxLevel) ? (maxBarWidth * _character.currentLevelProgress) : maxBarWidth);
	}

	private void HandleGainedXP()
	{
		RefreshXPBar();
	}

	private void HandleEventTriggered(AnimationEvent obj)
	{
		if (obj.stringParameter == "addXp")
		{
			StartCoroutine(AddXPSequence());
		}
		else if (obj.stringParameter == "introComplete" && showLevelUp)
		{
			showLevelUp = false;
			BattleController.instance.levelUpScreen.QueueUpCharacter(_character);
		}
	}

	private IEnumerator AddXPSequence()
	{
		int remainingXP = xpAmount;
		while (remainingXP > 0)
		{
			if (_character.xp + remainingXP >= _character.currentLevelData.xpToNextLevel)
			{
				Tweener tween = DOTween.To(() => levelBarFillSprite.width, (float x) =>
				{
					levelBarFillSprite.width = x;
				}, maxBarWidth, 50f).SetSpeedBased().SetEase(Ease.Linear);
				yield return tween.WaitForCompletion();
				if (levelUpGroup.activeSelf)
				{
					levelUpGroup.SetActive(false);
				}
				levelUpGroup.SetActive(true);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_results_levelup", GFAudioManager.SFX_GROUP, string.Empty);
				int amountToLevel = _character.currentLevelData.xpToNextLevel - _character.xp;
				remainingXP -= amountToLevel;
				_character.AddXP(amountToLevel);
				if (_character.level >= _character.levelTable.maxLevel)
				{
					break;
				}
			}
			else
			{
				Tweener tween2 = DOTween.To(endValue: maxBarWidth * ((float)(_character.xp + remainingXP - _character.currentLevelData.xpStart) / (float)(_character.currentLevelData.xpToNextLevel - _character.currentLevelData.xpStart)), getter: () => levelBarFillSprite.width, setter: (float x) =>
				{
					levelBarFillSprite.width = x;
				}, duration: 30f).SetSpeedBased().SetEase(Ease.Linear);
				yield return tween2.WaitForCompletion();
				_character.AddXP(remainingXP);
				remainingXP = 0;
			}
		}
	}

	private void HandleLeveled()
	{
		showLevelUp = true;
		UpdateView();
	}

	private void UpdateView()
	{
		portraitSprite.SetSprite(_character.portraitSprite);
		portraitWhiteSprite.SetSprite(_character.portraitSprite);
		levelNumberText.text = _character.level.ToString();
		levelNumberText.color = ((_character.level < _character.levelTable.maxLevel) ? Color.white : TTGColors.MAX_LEVEL_TEXT_COLOR);
		levelBarBaseSprite.SetSprite(levelBarSprites[_character.characterClass]);
		levelBarBaseSprite.color = ((_character.characterClass != CharacterClass.None) ? Color.white : new Color(248f / 255f, 24f / 255f, 180f / 255f));
		RefreshXPBar();
	}
}
