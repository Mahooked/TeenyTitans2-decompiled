using System.Collections;
using UnityEngine;

public class AbilityScript_RemoteBombDetonate : AbilityScript
{
	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			yield return new WaitForSeconds(0.1f);
			ProcessAbility();
			yield return new WaitForSeconds(0.1f);
		}
		else
		{
			_caster.Play(_abilityData.animName, isAbilityAnimation: true);
			_casterCamera.Play(_abilityData.animName, isAbilityAnimation: true);
		}
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		switch (array[0])
		{
		case "hit":
		{
			AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_abilityData.intValue1);
			bool flag = _caster.character.IsAbilityShiny(ability);
			RemoteBomb remoteBomb = (RemoteBomb)_targetMeter.statusEffects.Find((StatusEffect obj) => obj is RemoteBomb && ((RemoteBomb)obj).CasterCharacter == _caster.character);
			int num = TTGAbilityCalculator.CalculateDamage(_abilityData, _caster.character, (int)_abilityPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true, flag);
			int num2 = TTGAbilityCalculator.CalculateDamage(_abilityData, _caster.character, (int)_abilityPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true, flag, useRangedIntValue2: true);
			float num3 = ((!flag) ? _abilityData.floatValue1 : _abilityData.shinyEffect.floatValue1);
			float timeElapsed = remoteBomb.timeElapsed;
			int initialDamage = Mathf.FloorToInt(Mathf.Lerp(num, num2, Mathf.Clamp01(timeElapsed / num3)));
			Transform child = remoteBomb.BombObj.GetComponentInChildren<MeshRenderer>().transform.GetChild(0);
			ObjectPool.GetObjectFromResource(_abilityData.stringValue1, child.position);
			GFAudioManager.instance.PlayClipFromResources(_abilityData.stringValue2, GFAudioManager.SFX_GROUP, string.Empty);
			DamageResult damageResult = GetDamageResult(_caster.character, _target.character, initialDamage);
			damageResult.damage = DiminishingReturns.instance.GetDiminishedValue(damageResult.damage, _caster.character, _abilityData);
			_target.character.DoDamage(damageResult);
			remoteBomb.Remove();
			if (_abilityData.secondaryStatusEffect != StatusEffectName.None && _target.character.isAlive)
			{
				StatusEffectData secondaryStatusEffectData = _abilityData.secondaryStatusEffectData;
				if (secondaryStatusEffectData.probability > 0f && Random.value <= secondaryStatusEffectData.probability)
				{
					if (isAbilityShiny && _abilityData.shinyEffect.effectType == AbilityShinyEffectType.ModifySecondaryStatusEffect)
					{
						StatusEffectData statusEffectData = new StatusEffectData();
						statusEffectData.intValue1 = _abilityData.shinyEffect.intValue1;
						StatusEffectData statusEffectData2 = statusEffectData;
						int intValue;
						if (_abilityData.secondaryStatusEffect == StatusEffectName.TeamStatBuff)
						{
							intValue = (int)_abilityData.shinyEffect.statType;
						}
						else
						{
							intValue = ((_abilityData.secondaryStatusEffect != StatusEffectName.BuffDropper) ? _abilityData.shinyEffect.intValue2 : ((int)_abilityData.shinyEffect.beatBoxType));
						}
						statusEffectData2.intValue2 = intValue;
						statusEffectData.floatValue1 = _abilityData.shinyEffect.floatValue1;
						statusEffectData.floatValue2 = _abilityData.shinyEffect.floatValue2;
						statusEffectData.stringValue1 = _abilityData.shinyEffect.stringValue1;
						statusEffectData.stringValue2 = _abilityData.shinyEffect.stringValue2;
						statusEffectData.rangedIntValue1 = _abilityData.shinyEffect.rangedIntValue1;
						statusEffectData.rangedIntValue2 = _abilityData.shinyEffect.rangedIntValue2;
						statusEffectData.rangedFloatValue1 = _abilityData.shinyEffect.rangedFloatValue1;
						statusEffectData.rangedFloatValue2 = _abilityData.shinyEffect.rangedFloatValue2;
						statusEffectData.rangedFloatValue3 = _abilityData.shinyEffect.rangedFloatValue3;
						statusEffectData.rangedFloatValue4 = _abilityData.shinyEffect.rangedFloatValue4;
						statusEffectData.boolValue1 = _abilityData.shinyEffect.boolValue1;
						secondaryStatusEffectData = statusEffectData;
						ProcessStatusEffect(_abilityData.secondaryStatusEffect, secondaryStatusEffectData);
					}
					else
					{
						ProcessStatusEffect(_abilityData.secondaryStatusEffect, _abilityData.secondaryStatusEffectData);
					}
				}
			}
			if (array.Length > 1 && array[1].Equals("shake"))
			{
				ShakeCamera(_casterCamera, evt.floatParameter);
			}
			break;
		}
		case "shake":
			ShakeCamera(_casterCamera, evt.floatParameter);
			break;
		}
	}
}
