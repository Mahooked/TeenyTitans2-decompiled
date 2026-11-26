using System.Collections;
using UnityEngine;

public class AbilityScript_MultiHit : AbilityScript
{
	private int[] hitDamages;

	private int hitCount;

	private bool isAttackReduced;

	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			yield return new WaitForSeconds(0.25f);
			ProcessAbility();
			yield return new WaitForSeconds(0.25f);
		}
		else
		{
			_caster.Play(_abilityData.animName, isAbilityAnimation: true);
			_casterCamera.Play(_abilityData.animName, isAbilityAnimation: true);
		}
		int totalDamageAmount = TTGAbilityCalculator.CalculateDamage(_abilityData, _caster.character, (int)_abilityPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true, isAbilityShiny);
		int numHits = _abilityData.numHits;
		totalDamageAmount = DiminishingReturns.instance.GetDiminishedValue(totalDamageAmount, _caster.character, _abilityData);
		hitDamages = new int[numHits];
		if (totalDamageAmount >= numHits)
		{
			int num = Mathf.Max(1, Mathf.FloorToInt((float)totalDamageAmount / (float)numHits));
			int num2 = num + totalDamageAmount % numHits;
			hitDamages[numHits - 1] = num2;
			for (int num3 = numHits - 2; num3 >= 0; num3--)
			{
				hitDamages[num3] = num;
			}
		}
		else
		{
			int num4 = totalDamageAmount;
			for (int num5 = numHits - 1; num5 >= 0; num5--)
			{
				hitDamages[num5] = ((num4 > 0) ? 1 : 0);
				num4--;
			}
		}
		isAttackReduced = _casterMeter.GetAbilityButton(_abilityData).abilityButtonNumber.isReduced;
		hitCount = 0;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		string text = array[0];
		bool flag = false;
		DamageResult damageResult = null;
		switch (text)
		{
		case "smallHit":
			if (_abilityData.targetCount == TargetCount.All)
			{
				GetTeamAttackDamage(hitDamages[hitCount], out var mainCharacterDamage2, out var sideCharacterDamage2);
				for (int j = 0; j < _targetParty.Count; j++)
				{
					Character character2 = _targetParty[j];
					if (character2 == _target.character)
					{
						damageResult = GetDamageResult(_caster.character, character2, mainCharacterDamage2, ImpactType.Small, isKillingBlow: false);
						if (damageResult.damage <= 0 && !isAttackReduced)
						{
							damageResult.showDamage = false;
						}
						character2.DoDamage(damageResult);
						if (!damageResult.isDodged)
						{
							flag = true;
						}
					}
					else if (!character2.isDead && character2.characterDefinition.characterName != "Shadow Fig")
					{
						damageResult = GetDamageResult(_caster.character, character2, sideCharacterDamage2, ImpactType.Small, isKillingBlow: false);
						if (damageResult.damage <= 0 && !isAttackReduced)
						{
							damageResult.showDamage = false;
						}
						character2.DoDamage(damageResult);
					}
				}
			}
			else
			{
				damageResult = GetDamageResult(_caster.character, _target.character, hitDamages[hitCount], ImpactType.Small, isKillingBlow: false);
				if (damageResult.damage <= 0 && !isAttackReduced)
				{
					damageResult.showDamage = false;
				}
				_target.character.DoDamage(damageResult);
				flag = !damageResult.isDodged;
			}
			if (flag)
			{
				if (array.Length > 1 && array[1].Equals("shake"))
				{
					ShakeCamera(_casterCamera, evt.floatParameter);
				}
				if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit && (_abilityData.shinyEffect.effectType != AbilityShinyEffectType.AddStatusEffect || _abilityData.shinyEffect.statusEffect != StatusEffectName.TeamStatBuff))
				{
					ProcessShinyEffect();
				}
			}
			hitCount++;
			break;
		case "hit":
			if (_abilityData.targetCount == TargetCount.All)
			{
				GetTeamAttackDamage(hitDamages[hitCount], out var mainCharacterDamage, out var sideCharacterDamage);
				for (int i = 0; i < _targetParty.Count; i++)
				{
					Character character = _targetParty[i];
					if (character == _target.character)
					{
						damageResult = GetDamageResult(_caster.character, character, mainCharacterDamage);
						character.DoDamage(damageResult);
						if (_abilityData.secondaryStatusEffect != StatusEffectName.None && _target.character.isAlive)
						{
							StatusEffectData secondaryStatusEffectData = _abilityData.secondaryStatusEffectData;
							if ((secondaryStatusEffectData.ignoresDodge || !damageResult.isDodged) && damageResult.totalDamage > 0 && secondaryStatusEffectData.probability > 0f && Random.value <= secondaryStatusEffectData.probability)
							{
								if (isAbilityShiny && _abilityData.shinyEffect.effectType == AbilityShinyEffectType.ModifySecondaryStatusEffect)
								{
									secondaryStatusEffectData = new StatusEffectData();
									secondaryStatusEffectData.intValue1 = _abilityData.shinyEffect.intValue1;
									StatusEffectData statusEffectData = secondaryStatusEffectData;
									int intValue;
									if (_abilityData.secondaryStatusEffect == StatusEffectName.TeamStatBuff)
									{
										intValue = (int)_abilityData.shinyEffect.statType;
									}
									else
									{
										intValue = ((_abilityData.secondaryStatusEffect != StatusEffectName.BuffDropper) ? _abilityData.shinyEffect.intValue2 : ((int)_abilityData.shinyEffect.beatBoxType));
									}
									statusEffectData.intValue2 = intValue;
									secondaryStatusEffectData.floatValue1 = _abilityData.shinyEffect.floatValue1;
									secondaryStatusEffectData.floatValue2 = _abilityData.shinyEffect.floatValue2;
									secondaryStatusEffectData.stringValue1 = _abilityData.shinyEffect.stringValue1;
									secondaryStatusEffectData.stringValue2 = _abilityData.shinyEffect.stringValue2;
									secondaryStatusEffectData.rangedIntValue1 = _abilityData.shinyEffect.rangedIntValue1;
									secondaryStatusEffectData.rangedIntValue2 = _abilityData.shinyEffect.rangedIntValue2;
									secondaryStatusEffectData.rangedFloatValue1 = _abilityData.shinyEffect.rangedFloatValue1;
									secondaryStatusEffectData.rangedFloatValue2 = _abilityData.shinyEffect.rangedFloatValue2;
									secondaryStatusEffectData.rangedFloatValue3 = _abilityData.shinyEffect.rangedFloatValue3;
									secondaryStatusEffectData.rangedFloatValue4 = _abilityData.shinyEffect.rangedFloatValue4;
									secondaryStatusEffectData.boolValue1 = _abilityData.shinyEffect.boolValue1;
									ProcessStatusEffect(_abilityData.secondaryStatusEffect, secondaryStatusEffectData);
								}
								else
								{
									ProcessStatusEffect(_abilityData.secondaryStatusEffect, _abilityData.secondaryStatusEffectData);
								}
							}
						}
						if (!damageResult.isDodged)
						{
							flag = true;
						}
					}
					else if (!character.isDead && character.characterDefinition.characterName != "Shadow Fig")
					{
						damageResult = GetDamageResult(_caster.character, character, sideCharacterDamage);
						character.DoDamage(damageResult);
					}
				}
			}
			else
			{
				damageResult = GetDamageResult(_caster.character, _target.character, hitDamages[hitCount]);
				_target.character.DoDamage(damageResult);
				flag = !damageResult.isDodged;
				if (_abilityData.secondaryStatusEffect != StatusEffectName.None && _target.character.isAlive)
				{
					StatusEffectData secondaryStatusEffectData2 = _abilityData.secondaryStatusEffectData;
					if ((secondaryStatusEffectData2.ignoresDodge || !damageResult.isDodged) && damageResult.totalDamage > 0 && secondaryStatusEffectData2.probability > 0f && Random.value <= secondaryStatusEffectData2.probability)
					{
						if (isAbilityShiny && _abilityData.shinyEffect.effectType == AbilityShinyEffectType.ModifySecondaryStatusEffect)
						{
							secondaryStatusEffectData2 = new StatusEffectData();
							secondaryStatusEffectData2.intValue1 = _abilityData.shinyEffect.intValue1;
							StatusEffectData statusEffectData2 = secondaryStatusEffectData2;
							int intValue2;
							if (_abilityData.secondaryStatusEffect == StatusEffectName.TeamStatBuff)
							{
								intValue2 = (int)_abilityData.shinyEffect.statType;
							}
							else
							{
								intValue2 = ((_abilityData.secondaryStatusEffect != StatusEffectName.BuffDropper) ? _abilityData.shinyEffect.intValue2 : ((int)_abilityData.shinyEffect.beatBoxType));
							}
							statusEffectData2.intValue2 = intValue2;
							secondaryStatusEffectData2.floatValue1 = _abilityData.shinyEffect.floatValue1;
							secondaryStatusEffectData2.floatValue2 = _abilityData.shinyEffect.floatValue2;
							secondaryStatusEffectData2.stringValue1 = _abilityData.shinyEffect.stringValue1;
							secondaryStatusEffectData2.stringValue2 = _abilityData.shinyEffect.stringValue2;
							secondaryStatusEffectData2.rangedIntValue1 = _abilityData.shinyEffect.rangedIntValue1;
							secondaryStatusEffectData2.rangedIntValue2 = _abilityData.shinyEffect.rangedIntValue2;
							secondaryStatusEffectData2.rangedFloatValue1 = _abilityData.shinyEffect.rangedFloatValue1;
							secondaryStatusEffectData2.rangedFloatValue2 = _abilityData.shinyEffect.rangedFloatValue2;
							secondaryStatusEffectData2.rangedFloatValue3 = _abilityData.shinyEffect.rangedFloatValue3;
							secondaryStatusEffectData2.rangedFloatValue4 = _abilityData.shinyEffect.rangedFloatValue4;
							secondaryStatusEffectData2.boolValue1 = _abilityData.shinyEffect.boolValue1;
							ProcessStatusEffect(_abilityData.secondaryStatusEffect, secondaryStatusEffectData2);
						}
						else
						{
							ProcessStatusEffect(_abilityData.secondaryStatusEffect, _abilityData.secondaryStatusEffectData);
						}
					}
				}
			}
			if (flag)
			{
				if (array.Length > 1 && array[1].Equals("shake"))
				{
					ShakeCamera(_casterCamera, evt.floatParameter);
				}
				if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit)
				{
					ProcessShinyEffect();
				}
				CheckForItemSpawns();
			}
			hitCount++;
			break;
		}
	}
}
