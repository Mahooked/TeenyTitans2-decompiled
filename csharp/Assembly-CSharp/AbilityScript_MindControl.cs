using System.Collections;
using UnityEngine;

public class AbilityScript_MindControl : AbilityScript
{
	private int amountToHeal;

	private int[] amountsToHeal;

	private int minLifeSteal;

	private float lifeStealPercent;

	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			_caster.Play("mind control", isAbilityAnimation: true);
			_casterCamera.Play("mind control", isAbilityAnimation: true);
		}
		else
		{
			_caster.Play(_abilityData.animName, isAbilityAnimation: true);
			_casterCamera.Play(_abilityData.animName, isAbilityAnimation: true);
		}
		if (isAbilityShiny && _abilityData.shinyEffect.effectType == AbilityShinyEffectType.ModifyAbilityValues)
		{
			minLifeSteal = _abilityData.shinyEffect.intValue2;
			lifeStealPercent = _abilityData.shinyEffect.floatValue1;
		}
		else
		{
			minLifeSteal = _abilityData.minLifeSteal;
			lifeStealPercent = _abilityData.lifeStealPercent;
		}
		yield break;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		switch (array[0])
		{
		case "hit":
			if (_abilityData.targetCount == TargetCount.All)
			{
				int[] array2 = new int[_targetParty.Count];
				array2[0] = _target.character.hitPoints;
				int num = 1;
				for (int i = 0; i < array2.Length; i++)
				{
					if (_targetParty[i] != _target.character && !(_targetParty[i].characterDefinition.characterName == "Shadow Fig"))
					{
						array2[num] = _targetParty[i].hitPoints;
						num++;
					}
				}
				ProcessAbility();
				amountsToHeal = new int[array2.Length];
				amountsToHeal[0] = Mathf.Max(minLifeSteal, Mathf.FloorToInt((float)(array2[0] - _target.character.hitPoints) * lifeStealPercent));
				num = 1;
				for (int j = 0; j < amountsToHeal.Length; j++)
				{
					if (_targetParty[j] != _target.character && !(_targetParty[j].characterDefinition.characterName == "Shadow Fig"))
					{
						amountsToHeal[num] = Mathf.Max(minLifeSteal, Mathf.FloorToInt((float)(array2[num] - _targetParty[j].hitPoints) * lifeStealPercent));
						num++;
					}
				}
			}
			else
			{
				int hitPoints = _target.character.hitPoints;
				ProcessAbility();
				amountToHeal = Mathf.Max(minLifeSteal, Mathf.FloorToInt((float)(hitPoints - _target.character.hitPoints) * lifeStealPercent));
			}
			if (array.Length > 1 && array[1].Equals("shake"))
			{
				ShakeCamera(_casterCamera, evt.floatParameter);
			}
			break;
		case "heal":
			if (amountToHeal > 0)
			{
				_caster.character.DoHeal(new HealResult(null, _caster.character, Mathf.Max(0, amountToHeal)));
			}
			break;
		case "heal1":
			if (amountsToHeal.Length > 0 && amountsToHeal[0] > 0)
			{
				_caster.character.DoHeal(new HealResult(null, _caster.character, Mathf.Max(0, amountsToHeal[0])));
			}
			break;
		case "heal2":
			if (amountsToHeal.Length > 1 && amountsToHeal[1] > 0)
			{
				_caster.character.DoHeal(new HealResult(null, _caster.character, Mathf.Max(0, amountsToHeal[1])));
			}
			break;
		case "heal3":
			if (amountsToHeal.Length > 2 && amountsToHeal[2] > 0)
			{
				_caster.character.DoHeal(new HealResult(null, _caster.character, Mathf.Max(0, amountsToHeal[2])));
			}
			break;
		}
	}
}
