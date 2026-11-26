using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript_Channeling : AbilityScript
{
	private List<GameObject> addedObjects = new List<GameObject>();

	private float prevItemTickTime;

	private const float itemTickDelay = 2f;

	public BattleCharacter target => _target;

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

	public void Tick(ChannelingData.ChannelingStep channelingStep, out bool resolved)
	{
		Debug.Log(string.Concat(GetType(), ", TICK Ability"));
		resolved = true;
		if (_caster.character == null)
		{
			Debug.Log("AbilityScript_Channeling, _caster.character is NULL. _caster.name: " + _caster.gameObject.name);
			resolved = false;
			return;
		}
		if ((_target == null || _target.character == null || _target.character.isDead) && _abilityData.targetType == TargetType.Enemy)
		{
			_target = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.playerCharacter : BattleController.instance.enemyCharacter);
			Debug.Log("Channeling Target changed to new enemy character");
		}
		if (IsHealOrBuffBlocked(_abilityData) && _casterMeter.GetStatusEffect<HealBuffBlocker>() != null)
		{
			BattleDisplayManager.instance.ShowMessage(Language.Get("BLOCKED", "ABILS,CHIPS,ACHVS,LDBS"), _casterMeter.character);
			return;
		}
		switch (_abilityData.type)
		{
		case AbilityType.Attack:
		{
			DamageResult damageResult = null;
			bool flag2 = false;
			int num8 = 0;
			if (_abilityData.targetCount == TargetCount.All)
			{
				int initialValue = Mathf.RoundToInt(GetTotalDamage());
				initialValue = DiminishingReturns.instance.GetDiminishedValue(initialValue, _caster.character, _abilityData);
				int num9 = 0;
				for (int num10 = 0; num10 < _targetParty.Count; num10++)
				{
					if (!_targetParty[num10].isDead && (_targetParty[num10].characterDefinition.characterName != "Shadow Fig" || _targetParty[num10] == _target.character))
					{
						num9++;
					}
				}
				int num11 = Mathf.FloorToInt((float)initialValue / (float)num9);
				int initialDamage = num11 + initialValue % num9;
				for (int num12 = 0; num12 < _targetParty.Count; num12++)
				{
					if (_targetParty[num12] == _target.character)
					{
						damageResult = GetDamageResult(_caster.character, _targetParty[num12], initialDamage, ImpactType.Small);
						_targetParty[num12].DoDamage(damageResult);
						if (damageResult.isDodged)
						{
							flag2 = true;
						}
						else
						{
							num8++;
						}
					}
					else if (!_targetParty[num12].isDead && _targetParty[num12].characterDefinition.characterName != "Shadow Fig")
					{
						damageResult = GetDamageResult(_caster.character, _targetParty[num12], num11, ImpactType.Small);
						_targetParty[num12].DoDamage(damageResult);
						if (!damageResult.isDodged)
						{
							num8++;
						}
					}
				}
			}
			else if (_abilityData.targetCount == TargetCount.Single)
			{
				damageResult = GetDamageResult(_caster.character, _target.character, _abilityData, ImpactType.Small);
				damageResult.damage = DiminishingReturns.instance.GetDiminishedValue(damageResult.damage, _caster.character, _abilityData);
				if (_target.character != null)
				{
					_target.character.DoDamage(damageResult);
				}
				if (damageResult.isDodged)
				{
					flag2 = true;
				}
				else
				{
					num8++;
				}
			}
			else if (_abilityData.targetCount == TargetCount.SelfAndTarget)
			{
				damageResult = GetDamageResult(_caster.character, _target.character, _abilityData, ImpactType.Small);
				damageResult.damage = DiminishingReturns.instance.GetDiminishedValue(damageResult.damage, _caster.character, _abilityData);
				if (_target.character != null)
				{
					_target.character.DoDamage(damageResult);
				}
				if (damageResult.isDodged)
				{
					flag2 = true;
				}
				else
				{
					num8++;
				}
				damageResult = GetDamageResult(_caster.character, _caster.character, _abilityData, ImpactType.None);
				_caster.character.DoDamage(damageResult);
			}
			if (_abilityData.secondaryStatusEffect != StatusEffectName.None && _target.character != null && _target.character.isAlive)
			{
				StatusEffectData secondaryStatusEffectData = _abilityData.secondaryStatusEffectData;
				if ((secondaryStatusEffectData.ignoresDodge || !flag2) && damageResult != null && damageResult.totalDamage > 0 && secondaryStatusEffectData.probability > 0f && Random.value <= secondaryStatusEffectData.probability)
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
			if (!flag2 || num8 > 0)
			{
				if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit && num8 > 0)
				{
					ProcessShinyEffect();
				}
				if (!flag2 && Time.time > prevItemTickTime + 2f)
				{
					prevItemTickTime = Time.time;
					CheckForItemSpawns(allowTofu: false);
				}
			}
			break;
		}
		case AbilityType.Heal:
		{
			int num = TTGAbilityCalculator.CalculateHealAmount(_abilityData, _caster.character, (int)_abilityPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: true, isAbilityShiny);
			num = Mathf.RoundToInt(num);
			num = DiminishingReturns.instance.GetDiminishedValue(num, _caster.character, _abilityData);
			List<Character> list = new List<Character>();
			TargetType targetType = _abilityData.targetType;
			if (targetType != TargetType.All && targetType != TargetType.Enemy)
			{
				if (_abilityData.targetCount == TargetCount.All)
				{
					int num2 = 0;
					int num3 = _targetParty.IndexOf(_caster.character);
					for (int i = 0; i < _targetParty.Count; i++)
					{
						if (!_targetParty[num3].isDead && (_targetParty[num3].characterDefinition.characterName != "Shadow Fig" || _targetParty[num3] == _caster.character))
						{
							list.Add(_targetParty[num3]);
							num2++;
						}
						num3++;
						if (num3 >= _targetParty.Count)
						{
							num3 = 0;
						}
					}
					num = Mathf.Max(1, num);
					int num4 = Mathf.FloorToInt((float)num / (float)num2);
					int num5 = num4 + num % num2;
					List<int> list2 = new List<int>();
					int num6 = 0;
					for (int j = 0; j < num2; j++)
					{
						Character character = list[j];
						if (character == _caster.character)
						{
							list2.Add(Mathf.Min(character.maxHitPoints, character.hitPoints + num5) - character.hitPoints);
							num6 += Mathf.Max(character.hitPoints + num5, character.maxHitPoints) - character.maxHitPoints;
						}
						else
						{
							list2.Add(Mathf.Min(character.maxHitPoints, character.hitPoints + num4) - character.hitPoints);
							num6 += Mathf.Max(character.hitPoints + num4, character.maxHitPoints) - character.maxHitPoints;
						}
					}
					bool flag = false;
					while (num6 > 0 && !flag)
					{
						int num7 = num6;
						for (int k = 0; k < num2; k++)
						{
							if (list[k].hitPoints + list2[k] < list[k].maxHitPoints)
							{
								list2[k]++;
								num6--;
								if (num6 == 0)
								{
									break;
								}
							}
						}
						if (num7 == num6)
						{
							flag = true;
						}
					}
					for (int l = 0; l < num2; l++)
					{
						list[l].DoHeal(GetHealResult(_caster.character, list[l], list2[l]));
					}
				}
				else if (_abilityData.targetCount == TargetCount.Single)
				{
					list.Add(_target.character);
					if (_target.character != null)
					{
						_target.character.DoHeal(GetHealResult(_caster.character, _target.character, Mathf.Max(1, num)));
					}
				}
				else
				{
					Debug.Log(_caster.gameObject.name + ", Heal Self");
					list.Add(_caster.character);
					_caster.character.DoHeal(GetHealResult(_caster.character, _caster.character, Mathf.Max(1, num)));
				}
			}
			Character randomItem = list.FindAll((Character obj) => obj != _caster.character).GetRandomItem();
			if (randomItem != null && randomItem.characterDefinition.sounds.healSounds.soundPaths.Count > 0 && Random.value < randomItem.characterDefinition.sounds.healSounds.probability)
			{
				GFAudioManager.instance.PlayClipFromResources(randomItem.characterDefinition.sounds.healSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
			break;
		}
		}
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		switch (array[0])
		{
		case "hit":
			ProcessAbility();
			if (array.Length > 1 && array[1].Equals("shake"))
			{
				ShakeCamera(_casterCamera, evt.floatParameter);
			}
			break;
		case "shake":
			ShakeCamera(_casterCamera, evt.floatParameter);
			break;
		case "addObject":
			if (array.Length > 1)
			{
				GameObject objectFromResource = ObjectPool.GetObjectFromResource(array[1], _caster.model.position);
				if (objectFromResource != null)
				{
					objectFromResource.transform.localScale = _caster.transform.localScale;
					addedObjects.Add(objectFromResource);
				}
			}
			else if (evt.objectReferenceParameter != null)
			{
				ObjectPool.instance.AddType((GameObject)evt.objectReferenceParameter, 1);
				GameObject gameObject2 = ObjectPool.GetObject(evt.objectReferenceParameter.name, _caster.model.position);
				if (gameObject2 != null)
				{
					gameObject2.transform.localScale = _caster.transform.localScale;
					addedObjects.Add(gameObject2);
				}
			}
			break;
		case "addObjectTo":
			if (array.Length > 1 && evt.objectReferenceParameter != null)
			{
				ObjectPool.instance.AddType((GameObject)evt.objectReferenceParameter, 1);
				GameObject gameObject = ObjectPool.GetObject(evt.objectReferenceParameter.name, _caster.model.position);
				if (gameObject != null)
				{
					Transform transform = _caster.transform.Find(array[1]);
					Debug.Log(array[1] + "     " + transform);
					gameObject.transform.parent = transform;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.identity;
					gameObject.transform.localScale = Vector3.one;
					addedObjects.Add(gameObject);
				}
			}
			break;
		case "playHit":
			_target.PlayHit();
			break;
		case "playSmallHit":
			_target.PlaySmallHit();
			break;
		}
	}

	protected override void HandleExtrasEventTriggered(AnimationEvent evt)
	{
		base.HandleExtrasEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		switch (array[0])
		{
		case "enable":
			if (array.Length > 1)
			{
				Transform transform2 = _caster.extrasAnimator.transform.Find(array[1]);
				if (transform2 != null)
				{
					transform2.gameObject.SetActive(value: true);
				}
			}
			break;
		case "disable":
			if (array.Length > 1)
			{
				Transform transform = _caster.extrasAnimator.transform.Find(array[1]);
				if (transform != null)
				{
					transform.gameObject.SetActive(value: false);
				}
			}
			break;
		}
	}

	public override void CleanUp()
	{
		base.CleanUp();
		while (addedObjects.Count > 0)
		{
			ObjectPool.PoolObject(addedObjects.Pop());
		}
	}
}
