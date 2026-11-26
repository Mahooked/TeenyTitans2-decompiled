using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityButtonNumber : MonoBehaviour
{
	private static Color normal = Color.white;

	private static Color bonus = new Color(0.2f, 239f / 255f, 225f / 255f, 1f);

	private static Color reduced = new Color(1f, 64f / 255f, 64f / 255f, 1f);

	private static Color channeled = new Color(0.47f, 0.68f, 0.97f);

	private static float widthTargets3 = 62f;

	private static float widthTargets2 = 44f;

	private static float widthTargets1 = 26f;

	public tk2dSlicedSpriteHelper backingSprite;

	public TextMeshPro mainNumberText;

	public TextMeshPro leftNumberText;

	public TextMeshPro rightNumberText;

	public TextMeshPro leftPlus;

	public TextMeshPro rightPlus;

	public Color normalColor = Color.white;

	public Color bonusColor = new Color(0.2f, 239f / 255f, 225f / 255f, 1f);

	public Color reducedColor = new Color(1f, 64f / 255f, 64f / 255f, 1f);

	public Color channeledColor = new Color(0.47f, 0.68f, 0.97f);

	private List<TextMeshPro> numberTextFields;

	private bool isAbilityShiny;

	private float abilitySlotPosition;

	private AbilityData _abilityData;

	private Character _character;

	private BattleController _controller;

	public bool isBonus
	{
		get
		{
			if (_abilityData != null)
			{
				if ((_abilityData.type == AbilityType.Attack || _abilityData.type == AbilityType.Heal) && mainNumberText.color == bonusColor)
				{
					return true;
				}
				if (_abilityData.type == AbilityType.StatusEffect && (_abilityData.statusEffect == StatusEffectName.AttackDrone || _abilityData.statusEffect == StatusEffectName.Poison) && mainNumberText.color == bonusColor)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool isReduced
	{
		get
		{
			if (_abilityData != null)
			{
				if ((_abilityData.type == AbilityType.Attack || _abilityData.type == AbilityType.Heal) && mainNumberText.color == reducedColor)
				{
					return true;
				}
				if (_abilityData.type == AbilityType.StatusEffect && (_abilityData.statusEffect == StatusEffectName.AttackDrone || _abilityData.statusEffect == StatusEffectName.Poison) && mainNumberText.color == reducedColor)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool hasDiminishingReturns
	{
		get
		{
			if (_abilityData != null && DiminishingReturns.instance != null && _character != null)
			{
				return DiminishingReturns.instance.GetDiminishingReturnsFactor(_character, _abilityData) > 0f;
			}
			return false;
		}
	}

	private void Awake()
	{
		if (numberTextFields == null)
		{
			numberTextFields = new List<TextMeshPro> { mainNumberText, leftNumberText, rightNumberText };
		}
	}

	public void Init(AbilityData abilityData, Character character, BattleController controller, float abilityPosition)
	{
		_abilityData = abilityData;
		_character = character;
		_controller = controller;
		if (numberTextFields == null)
		{
			numberTextFields = new List<TextMeshPro> { mainNumberText, leftNumberText, rightNumberText };
		}
		isAbilityShiny = _character.IsAbilityShiny(_abilityData);
		abilitySlotPosition = abilityPosition;
		Refresh();
	}

	private void Refresh()
	{
		List<Character> targets = GetTargets();
		int num = 0;
		int num2 = 0;
		Color white = Color.white;
		if (_abilityData.type == AbilityType.Attack)
		{
			if (_abilityData.damageType == DamageType.RemoteBomb)
			{
				AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_abilityData.intValue1);
				bool flag = _character.IsAbilityShiny(ability);
				int num3 = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true, flag);
				int num4 = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true, flag, useRangedIntValue2: true);
				float num5 = ((!flag) ? _abilityData.floatValue1 : _abilityData.shinyEffect.floatValue1);
				ActionMeter actionMeter = ((targets[0] != BattleController.instance.playerCharacter.character) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
				RemoteBomb remoteBomb = (RemoteBomb)actionMeter.statusEffects.Find((StatusEffect obj) => obj is RemoteBomb && ((RemoteBomb)obj).CasterCharacter == _character);
				float timeElapsed = remoteBomb.timeElapsed;
				num = Mathf.FloorToInt(Mathf.Lerp(num3, num4, Mathf.Clamp01(timeElapsed / num5)));
				num3 = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: false, flag);
				num4 = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: false, flag, useRangedIntValue2: true);
				num2 = Mathf.FloorToInt(Mathf.Lerp(num3, num4, Mathf.Clamp01(timeElapsed / num5)));
			}
			else
			{
				num = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true, isAbilityShiny);
				num2 = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: false, isAbilityShiny);
			}
		}
		else if (_abilityData.type == AbilityType.Heal)
		{
			num = TTGAbilityCalculator.CalculateHealAmount(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: true, isAbilityShiny);
			num2 = TTGAbilityCalculator.CalculateHealAmount(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: false, isAbilityShiny);
		}
		else if (_abilityData.type == AbilityType.StatusEffect && (_abilityData.statusEffect == StatusEffectName.AttackDrone || _abilityData.statusEffect == StatusEffectName.Poison))
		{
			num = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true, isAbilityShiny);
			num2 = TTGAbilityCalculator.CalculateDamage(_abilityData, _character, (int)abilitySlotPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: false, isAbilityShiny);
		}
		if (num > num2)
		{
			white = bonusColor;
		}
		else if (num < num2)
		{
			white = reducedColor;
		}
		else
		{
			white = ((!(_abilityData.channelingData.speed > 0f)) ? normalColor : channeledColor);
		}
		mainNumberText.color = white;
		leftNumberText.color = white;
		rightNumberText.color = white;
		leftPlus.color = white;
		rightPlus.color = white;
		if (targets.Count == 3)
		{
			mainNumberText.gameObject.SetActive(value: true);
			leftNumberText.gameObject.SetActive(value: true);
			rightNumberText.gameObject.SetActive(value: true);
			leftPlus.gameObject.SetActive(value: true);
			rightPlus.gameObject.SetActive(value: true);
			mainNumberText.transform.localPosition = new Vector3(0f, 0f, -2f);
			leftNumberText.transform.localPosition = new Vector3(-16f, 0f, -2f);
			rightNumberText.transform.localPosition = new Vector3(16f, 0f, -2f);
			leftPlus.transform.localPosition = new Vector3(-8.8f, 0f, -2f);
			rightPlus.transform.localPosition = new Vector3(8.8f, 0f, -2f);
			backingSprite.width = widthTargets3;
		}
		else if (targets.Count == 2)
		{
			mainNumberText.gameObject.SetActive(value: true);
			leftNumberText.gameObject.SetActive(value: true);
			rightNumberText.gameObject.SetActive(value: false);
			leftPlus.gameObject.SetActive(value: true);
			rightPlus.gameObject.SetActive(value: false);
			mainNumberText.transform.localPosition = new Vector3(8f, 0f, -2f);
			leftNumberText.transform.localPosition = new Vector3(-8f, 0f, -2f);
			leftPlus.transform.localPosition = new Vector3(0f, 0f, -2f);
			backingSprite.width = widthTargets2;
		}
		else
		{
			mainNumberText.gameObject.SetActive(value: true);
			leftNumberText.gameObject.SetActive(value: false);
			rightNumberText.gameObject.SetActive(value: false);
			leftPlus.gameObject.SetActive(value: false);
			rightPlus.gameObject.SetActive(value: false);
			mainNumberText.transform.localPosition = new Vector3(0f, 0f, -2f);
			backingSprite.width = widthTargets1;
		}
		int minPossibleValue = 1;
		if (DiminishingReturns.instance != null)
		{
			num = DiminishingReturns.instance.GetDiminishedValue(num, _character, _abilityData, 1, minPossibleValue);
		}
		RefreshNumbers(targets, num);
	}

	private void RefreshNumbers(List<Character> targets, int totalAmount)
	{
		if (_abilityData.type == AbilityType.Attack && _abilityData.damageType == DamageType.TossObject)
		{
			for (int i = 0; i < numberTextFields.Count; i++)
			{
				numberTextFields[i].text = "?";
			}
		}
		else if (targets.Count > 1)
		{
			int count = targets.Count;
			int num = Mathf.FloorToInt((float)totalAmount / (float)count);
			int num2 = num + totalAmount % count;
			List<int> list = new List<int>();
			if (_abilityData.type == AbilityType.Heal)
			{
				if (totalAmount < 3)
				{
					num = 1;
					num2 = 1;
				}
				Character character = null;
				int num3 = 0;
				for (int j = 0; j < count; j++)
				{
					character = targets[j];
					if (character == _character || (_controller == null && j == 0))
					{
						list.Add(Mathf.Min(character.maxHitPoints, character.hitPoints + num2) - character.hitPoints);
						num3 += Mathf.Max(character.hitPoints + num2, character.maxHitPoints) - character.maxHitPoints;
					}
					else
					{
						list.Add(Mathf.Min(character.maxHitPoints, character.hitPoints + num) - character.hitPoints);
						num3 += Mathf.Max(character.hitPoints + num, character.maxHitPoints) - character.maxHitPoints;
					}
				}
				bool flag = false;
				int num4 = 0;
				while (num3 > 0 && !flag)
				{
					num4 = num3;
					for (int k = 0; k < count; k++)
					{
						if (targets[k].hitPoints + list[k] < targets[k].maxHitPoints)
						{
							list[k]++;
							num3--;
							if (num3 == 0)
							{
								break;
							}
						}
					}
					if (num4 == num3)
					{
						flag = true;
					}
				}
			}
			else
			{
				if (_abilityData.damageType == DamageType.MultiHit)
				{
					int num5 = 0;
					int num6 = 0;
					int num7 = Mathf.Max(1, Mathf.FloorToInt((float)totalAmount / (float)_abilityData.numHits));
					int num8 = num7 + totalAmount % _abilityData.numHits;
					for (int l = 0; l < _abilityData.numHits; l++)
					{
						int num9 = ((l != 0) ? num7 : num8);
						int num10 = Mathf.FloorToInt((float)num9 / (float)count);
						num5 += num10;
						num6 += num10 + num9 % count;
					}
					num = num5;
					num2 = num6;
				}
				for (int m = 0; m < count; m++)
				{
					list.Add((m != 0) ? num : num2);
				}
			}
			for (int n = 0; n < count; n++)
			{
				numberTextFields[n].text = list[n].ToString();
			}
		}
		else
		{
			if (_abilityData.type == AbilityType.Heal)
			{
				totalAmount = Mathf.Min(_character.maxHitPoints, _character.hitPoints + totalAmount) - _character.hitPoints;
			}
			mainNumberText.text = totalAmount.ToString();
		}
	}

	private List<Character> GetTargets()
	{
		List<Character> list = new List<Character>();
		if (_controller != null)
		{
			if (_abilityData.targetCount == TargetCount.All)
			{
				switch (_abilityData.targetType)
				{
				case TargetType.Friendly:
				{
					if (_controller.playerParty.Contains(_character))
					{
						int count3 = _controller.playerParty.Count;
						int num3 = _controller.playerParty.IndexOf(_character);
						for (int k = 0; k < count3; k++)
						{
							if (!_controller.playerParty[num3].isDead)
							{
								list.Add(_controller.playerParty[num3]);
							}
							num3++;
							if (num3 >= count3)
							{
								num3 = 0;
							}
						}
						break;
					}
					int count4 = _controller.enemyParty.Count;
					int num4 = _controller.enemyParty.IndexOf(_character);
					for (int l = 0; l < count4; l++)
					{
						if (!_controller.enemyParty[num4].isDead)
						{
							list.Add(_controller.enemyParty[num4]);
						}
						num4++;
						if (num4 >= count4)
						{
							num4 = 0;
						}
					}
					break;
				}
				case TargetType.Enemy:
				{
					if (_controller.playerParty.Contains(_character))
					{
						int count = _controller.enemyParty.Count;
						int num = _controller.enemyParty.IndexOf(_controller.enemyCharacter.character);
						if (num <= -1)
						{
							break;
						}
						for (int i = 0; i < count; i++)
						{
							if (!_controller.enemyParty[num].isDead && (_controller.enemyParty[num].characterDefinition.characterName != "Shadow Fig" || _controller.enemyParty[num] == _controller.enemyCharacter.character))
							{
								list.Add(_controller.enemyParty[num]);
							}
							num++;
							if (num >= count)
							{
								num = 0;
							}
						}
						break;
					}
					int count2 = _controller.playerParty.Count;
					int num2 = _controller.playerParty.IndexOf(_controller.playerCharacter.character);
					if (num2 <= -1)
					{
						break;
					}
					for (int j = 0; j < count2; j++)
					{
						if (!_controller.playerParty[num2].isDead)
						{
							list.Add(_controller.playerParty[num2]);
						}
						num2++;
						if (num2 >= count2)
						{
							num2 = 0;
						}
					}
					break;
				}
				}
			}
			else if (_abilityData.targetCount == TargetCount.Self)
			{
				list.Add(_character);
			}
			else
			{
				TargetType targetType = _abilityData.targetType;
				if (targetType != TargetType.Friendly && targetType == TargetType.Enemy)
				{
					if (_character == _controller.playerCharacter.character)
					{
						list.Add(_controller.enemyCharacter.character);
					}
					else
					{
						list.Add(_controller.playerCharacter.character);
					}
				}
			}
		}
		else if (_abilityData.targetCount == TargetCount.All)
		{
			switch (_abilityData.targetType)
			{
			case TargetType.Friendly:
				list.Add(new Character(-1, _character.characterDefinition));
				list.GetLastItem().hitPoints = 1;
				list.Add(new Character(-1, _character.characterDefinition));
				list.GetLastItem().hitPoints = 1;
				list.Add(new Character(-1, _character.characterDefinition));
				list.GetLastItem().hitPoints = 1;
				break;
			case TargetType.Enemy:
				list.Add(new Character(-1, _character.characterDefinition));
				list.Add(new Character(-1, _character.characterDefinition));
				list.Add(new Character(-1, _character.characterDefinition));
				break;
			}
		}
		else if (_abilityData.targetCount == TargetCount.Self)
		{
			list.Add(_character);
		}
		else
		{
			TargetType targetType2 = _abilityData.targetType;
			if (targetType2 != TargetType.Friendly && targetType2 == TargetType.Enemy)
			{
				list.Add(new Character(-1, _character.characterDefinition));
			}
		}
		return list;
	}
}
