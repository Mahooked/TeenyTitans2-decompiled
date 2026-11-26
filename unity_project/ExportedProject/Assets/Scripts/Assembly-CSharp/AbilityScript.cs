using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;

public abstract class AbilityScript
{
	public static readonly Dictionary<CharacterClass, CharacterClass> classStrengths = new Dictionary<CharacterClass, CharacterClass>
	{
		{
			CharacterClass.Beast,
			CharacterClass.Cute
		},
		{
			CharacterClass.Cute,
			CharacterClass.DarkArts
		},
		{
			CharacterClass.DarkArts,
			CharacterClass.Super
		},
		{
			CharacterClass.Super,
			CharacterClass.Tech
		},
		{
			CharacterClass.Tech,
			CharacterClass.MartialArts
		},
		{
			CharacterClass.MartialArts,
			CharacterClass.Beast
		}
	};

	public static readonly Dictionary<CharacterClass, CharacterClass> classWeaknesses = new Dictionary<CharacterClass, CharacterClass>
	{
		{
			CharacterClass.Beast,
			CharacterClass.MartialArts
		},
		{
			CharacterClass.Cute,
			CharacterClass.Beast
		},
		{
			CharacterClass.DarkArts,
			CharacterClass.Cute
		},
		{
			CharacterClass.Super,
			CharacterClass.DarkArts
		},
		{
			CharacterClass.Tech,
			CharacterClass.Super
		},
		{
			CharacterClass.MartialArts,
			CharacterClass.Tech
		}
	};

	private static int MODCHIP_SIMPLY_STUNNING_UID = 31;

	protected AbilityData _abilityData;

	protected BattleCharacter _caster;

	protected BattleCharacter _target;

	protected BattleCamera _casterCamera;

	protected BattleCamera _targetCamera;

	protected List<Character> _casterParty;

	protected List<Character> _targetParty;

	protected ActionMeter _casterMeter;

	protected ActionMeter _targetMeter;

	protected PortraitSwitcher _casterPortraits;

	protected PortraitSwitcher _targetPortraits;

	protected float _abilityPosition;

	private List<BattleCharacter> otherTargetCharacters = new List<BattleCharacter>();

	protected bool isAbilityShiny;

	protected Vector3 targetTopPosition
	{
		get
		{
			Camera guiCamera = BattleController.instance.guiCamera;
			Vector3 position = _casterCamera.camera.WorldToScreenPoint(_target.top.position);
			return guiCamera.ScreenToWorldPoint(position);
		}
	}

	public void Init(AbilityData abilityData, BattleCharacter caster, BattleCharacter target)
	{
		_abilityData = abilityData;
		_caster = caster;
		_target = target;
		_casterCamera = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
		_targetCamera = ((!(_target == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
		_casterParty = new List<Character> { caster.character };
		_targetParty = new List<Character> { target.character };
		_casterPortraits = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
		_targetPortraits = ((!(_target == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
		_casterMeter = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
		if (_abilityData.targetType == TargetType.Enemy)
		{
			_targetMeter = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.playerActionMeter : BattleController.instance.enemyActionMeter);
		}
		else
		{
			_targetMeter = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
		}
		OnInitialized();
	}

	public void Init(AbilityData abilityData, BattleCharacter caster, BattleCharacter target, List<Character> casterParty, List<Character> targetParty, float abilityPosition)
	{
		_abilityData = abilityData;
		_caster = caster;
		_target = target;
		_casterCamera = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
		_targetCamera = ((!(_target == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
		_casterParty = casterParty;
		_targetParty = targetParty;
		_casterPortraits = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
		_targetPortraits = ((!(_target == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
		_abilityPosition = abilityPosition;
		_casterMeter = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
		if (_abilityData.targetType == TargetType.Enemy)
		{
			_targetMeter = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.playerActionMeter : BattleController.instance.enemyActionMeter);
		}
		else
		{
			_targetMeter = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
		}
		OnInitialized();
	}

	private void OnInitialized()
	{
		if (_abilityData.numChargesRequired > 0)
		{
			int abilityChargeCount = _casterMeter.GetAbilityChargeCount(_caster.character, _abilityData);
			if (!string.IsNullOrEmpty(_abilityData.castSoundPath))
			{
				GFAudioManager.instance.PlayClipFromResources(_abilityData.castSoundPath, GFAudioManager.SFX_GROUP, string.Empty, 1f, _abilityData.castSoundDelay);
			}
			if (abilityChargeCount == _abilityData.numChargesRequired && !string.IsNullOrEmpty(_abilityData.soundPath))
			{
				GFAudioManager.instance.PlayClipFromResources(_abilityData.soundPath, GFAudioManager.SFX_GROUP, string.Empty, 1f, _abilityData.soundDelay);
			}
		}
		else if (!string.IsNullOrEmpty(_abilityData.soundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(_abilityData.soundPath, GFAudioManager.SFX_GROUP, string.Empty, 1f, _abilityData.soundDelay);
		}
		if (_abilityData.voSoundPaths.Count > 0 && UnityEngine.Random.value < _abilityData.voSoundProbability)
		{
			int index = UnityEngine.Random.Range(0, _abilityData.voSoundPaths.Count);
			GFAudioManager.instance.PlayClipFromResources(_abilityData.voSoundPaths[index], GFAudioManager.VOICEOVER_GROUP, string.Empty, 1f, _abilityData.voSoundDelays[index]);
		}
		_targetMeter.CharacterChanged += HandleCharacterChanged;
		_caster.animatorEventListener.EventTriggered += HandleEventTriggered;
		if (_caster.faceAnimatorEventListener != null)
		{
			_caster.faceAnimatorEventListener.EventTriggered += HandleFaceEventTriggered;
		}
		if (_caster.extrasAnimatorEventListener != null)
		{
			_caster.extrasAnimatorEventListener.EventTriggered += HandleExtrasEventTriggered;
		}
		_casterCamera.OffsetInStart += HandleOffsetInStart;
		_casterCamera.OffsetOutStart += HandleOffsetOutStart;
		if (_abilityData.targetType == TargetType.Friendly)
		{
			_casterCamera.OffsetInStart += HandleShowingTeam;
		}
		else
		{
			_casterCamera.OffsetInComplete += HandleShowingTeam;
		}
		_casterCamera.OffsetOutComplete += HandleRemovingTeam;
		isAbilityShiny = _caster.character.IsAbilityShiny(_abilityData);
		if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnInit)
		{
			ProcessShinyEffect();
		}
	}

	protected bool IsHealOrBuffBlocked(AbilityData ability)
	{
		if (ability.type == AbilityType.Heal)
		{
			return true;
		}
		if (ability.type == AbilityType.ActionMeter && (ability.actionMeterEffect == ActionMeterEffect.Speed || ability.actionMeterEffect == ActionMeterEffect.SpeedBonus))
		{
			return true;
		}
		if (ability.type == AbilityType.StatusEffect)
		{
			if (ability.statusEffect == StatusEffectName.Immunity)
			{
				return true;
			}
			if (ability.statusEffect == StatusEffectName.ActionMeterSpeedModifier && ability.bonus > 0)
			{
				return true;
			}
			if (ability.statusEffect == StatusEffectName.TeamStatBuff || ability.statusEffect == StatusEffectName.CharacterStatBuff)
			{
				return true;
			}
		}
		return false;
	}

	protected int GetTotalDamage()
	{
		return TTGAbilityCalculator.CalculateDamage(_abilityData, _caster.character, (int)_abilityPosition, true, true, true, isAbilityShiny);
	}

	protected void ProcessAbility()
	{
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
			bool flag = false;
			int num = 0;
			if (_abilityData.targetCount == TargetCount.All)
			{
				int totalDamage = GetTotalDamage();
				totalDamage = DiminishingReturns.instance.GetDiminishedValue(totalDamage, _caster.character, _abilityData);
				int mainCharacterDamage;
				int sideCharacterDamage;
				GetTeamAttackDamage(totalDamage, out mainCharacterDamage, out sideCharacterDamage);
				for (int i = 0; i < _targetParty.Count; i++)
				{
					if (_targetParty[i] == _target.character)
					{
						Debug.LogFormat("[AbilityScript::ProcessAbility] - Target #{0}: {1}    Main Damage: {2}", i + 1, _targetParty[i].characterDefinition.characterName, mainCharacterDamage);
						damageResult = GetDamageResult(_caster.character, _targetParty[i], mainCharacterDamage);
						damageResult.useRandomPositionOffset = false;
						_targetParty[i].DoDamage(damageResult);
						if (damageResult.isDodged)
						{
							flag = true;
						}
						else
						{
							num++;
						}
					}
					else if (!_targetParty[i].isDead && _targetParty[i].characterDefinition.characterName != "Shadow Fig")
					{
						Debug.LogFormat("[AbilityScript::ProcessAbility] - Target #{0}: {1}    Damage: {2}", i + 1, _targetParty[i].characterDefinition.characterName, sideCharacterDamage);
						damageResult = GetDamageResult(_caster.character, _targetParty[i], sideCharacterDamage);
						damageResult.useRandomPositionOffset = false;
						_targetParty[i].DoDamage(damageResult);
						if (!damageResult.isDodged)
						{
							num++;
						}
					}
				}
			}
			else if (_abilityData.targetCount == TargetCount.Single)
			{
				damageResult = GetDamageResult(_caster.character, _target.character, _abilityData);
				damageResult.damage = DiminishingReturns.instance.GetDiminishedValue(damageResult.damage, _caster.character, _abilityData);
				_target.character.DoDamage(damageResult);
				if (damageResult.isDodged)
				{
					flag = true;
				}
				else
				{
					num++;
				}
			}
			else if (_abilityData.targetCount == TargetCount.SelfAndTarget)
			{
				damageResult = GetDamageResult(_caster.character, _target.character, _abilityData);
				damageResult.damage = DiminishingReturns.instance.GetDiminishedValue(damageResult.damage, _caster.character, _abilityData);
				_target.character.DoDamage(damageResult);
				if (damageResult.isDodged)
				{
					flag = true;
				}
				else
				{
					num++;
				}
				damageResult = GetDamageResult(_caster.character, _caster.character, _abilityData, ImpactType.None);
				_caster.character.DoDamage(damageResult);
			}
			if (_abilityData.secondaryStatusEffect != StatusEffectName.None && _target.character.isAlive)
			{
				StatusEffectData data = _abilityData.secondaryStatusEffectData;
				if ((data.ignoresDodge || !flag) && damageResult != null && damageResult.totalDamage > 0 && data.probability > 0f && UnityEngine.Random.value <= data.probability)
				{
					if (isAbilityShiny && _abilityData.shinyEffect.effectType == AbilityShinyEffectType.ModifySecondaryStatusEffect)
					{
						StatusEffectData statusEffectData = new StatusEffectData();
						statusEffectData.intValue1 = _abilityData.shinyEffect.intValue1;
						statusEffectData.intValue2 = ((_abilityData.secondaryStatusEffect == StatusEffectName.TeamStatBuff) ? ((int)_abilityData.shinyEffect.statType) : ((_abilityData.secondaryStatusEffect != StatusEffectName.BuffDropper) ? _abilityData.shinyEffect.intValue2 : ((int)_abilityData.shinyEffect.beatBoxType)));
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
						data = statusEffectData;
						ProcessStatusEffect(_abilityData.secondaryStatusEffect, data);
					}
					else
					{
						ProcessStatusEffect(_abilityData.secondaryStatusEffect, _abilityData.secondaryStatusEffectData);
					}
				}
			}
			if (!flag || num > 0)
			{
				if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit && num > 0)
				{
					ProcessShinyEffect();
				}
				if (!flag)
				{
					CheckForItemSpawns();
				}
			}
			break;
		}
		case AbilityType.StatusEffect:
			if (isAbilityShiny && _abilityData.shinyEffect.effectType == AbilityShinyEffectType.ModifyAbilityValues)
			{
				StatusEffectData statusEffectData = new StatusEffectData();
				statusEffectData.intValue1 = _abilityData.shinyEffect.intValue1;
				statusEffectData.intValue2 = ((_abilityData.statusEffect == StatusEffectName.TeamStatBuff) ? ((int)_abilityData.shinyEffect.statType) : ((_abilityData.statusEffect != StatusEffectName.BuffDropper) ? _abilityData.shinyEffect.intValue2 : ((int)_abilityData.shinyEffect.beatBoxType)));
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
				StatusEffectData data = statusEffectData;
				ProcessStatusEffect(_abilityData.statusEffect, data);
			}
			else
			{
				ProcessStatusEffect(_abilityData.statusEffect);
			}
			if (_abilityData.secondaryStatusEffect != StatusEffectName.None && !_target.character.isDead)
			{
				StatusEffectData data = _abilityData.secondaryStatusEffectData;
				if (data.probability > 0f && UnityEngine.Random.value <= data.probability)
				{
					if (isAbilityShiny && _abilityData.shinyEffect.effectType == AbilityShinyEffectType.ModifySecondaryStatusEffect)
					{
						StatusEffectData statusEffectData = new StatusEffectData();
						statusEffectData.intValue1 = _abilityData.shinyEffect.intValue1;
						statusEffectData.intValue2 = ((_abilityData.secondaryStatusEffect == StatusEffectName.TeamStatBuff) ? ((int)_abilityData.shinyEffect.statType) : ((_abilityData.secondaryStatusEffect != StatusEffectName.BuffDropper) ? _abilityData.shinyEffect.intValue2 : ((int)_abilityData.shinyEffect.beatBoxType)));
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
						data = statusEffectData;
						ProcessStatusEffect(_abilityData.secondaryStatusEffect, data);
					}
					else
					{
						ProcessStatusEffect(_abilityData.secondaryStatusEffect, _abilityData.secondaryStatusEffectData);
					}
				}
			}
			if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit)
			{
				ProcessShinyEffect();
			}
			break;
		case AbilityType.ActionMeter:
			switch (_abilityData.actionMeterEffect)
			{
			case ActionMeterEffect.Speed:
			{
				float diminishedValue = DiminishingReturns.instance.GetDiminishedValue(_abilityData.floatValue2, _caster.character, _abilityData);
				StatusEffectManager.instance.AddActionMeterStatusEffect(_targetMeter, new ActionMeterSpeedModifier(_targetMeter, _abilityData.modifier, diminishedValue));
				break;
			}
			case ActionMeterEffect.Block:
			{
				float diminishedValue = DiminishingReturns.instance.GetDiminishedValue(_abilityData.floatValue2, _caster.character, _abilityData);
				StatusEffectManager.instance.AddActionMeterStatusEffect(_targetMeter, new ActionMeterBlocker(_targetMeter, _abilityData.floatValue1, diminishedValue));
				break;
			}
			case ActionMeterEffect.Fill:
				_targetMeter.progress += _abilityData.modifier;
				break;
			case ActionMeterEffect.SpeedBonus:
			{
				float diminishedValue = DiminishingReturns.instance.GetDiminishedValue(_abilityData.floatValue2, _caster.character, _abilityData);
				StatusEffectManager.instance.AddActionMeterStatusEffect(_targetMeter, new ActionMeterSpeedModifier(_targetMeter, _abilityData.intValue1, diminishedValue));
				break;
			}
			case ActionMeterEffect.Disable:
				if (UnityEngine.Random.value >= _targetMeter.character.character.stunResistanceChance)
				{
					float diminishedValue = DiminishingReturns.instance.GetDiminishedValue(_abilityData.floatValue2, _caster.character, _abilityData);
					StatusEffectManager.instance.AddActionMeterStatusEffect(_targetMeter, new ActionMeterDisabler(_targetMeter, _targetPortraits, diminishedValue));
				}
				else
				{
					BattleDisplayManager.instance.ShowMessage(Language.Get("RESIST", "ABILS,CHIPS,ACHVS,LDBS"), _targetMeter.character);
				}
				break;
			}
			break;
		case AbilityType.Heal:
		{
			int initialValue = TTGAbilityCalculator.CalculateHealAmount(_abilityData, _caster.character, (int)_abilityPosition, true, false, true, isAbilityShiny);
			initialValue = DiminishingReturns.instance.GetDiminishedValue(initialValue, _caster.character, _abilityData);
			List<Character> list = new List<Character>();
			switch (_abilityData.targetType)
			{
			default:
				if (_abilityData.targetCount == TargetCount.All)
				{
					int num2 = 0;
					int num3 = _targetParty.IndexOf(_caster.character);
					for (int j = 0; j < _targetParty.Count; j++)
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
					initialValue = Mathf.Max(1, initialValue);
					int num4 = Mathf.FloorToInt((float)initialValue / (float)num2);
					int num5 = num4 + initialValue % num2;
					List<int> list2 = new List<int>();
					if (initialValue < 3)
					{
						num4 = 1;
						num5 = 1;
					}
					int num6 = 0;
					for (int k = 0; k < num2; k++)
					{
						Character character = list[k];
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
					bool flag2 = false;
					while (num6 > 0 && !flag2)
					{
						int num7 = num6;
						for (int l = 0; l < num2; l++)
						{
							if (list[l].hitPoints + list2[l] < list[l].maxHitPoints)
							{
								list2[l]++;
								num6--;
								if (num6 == 0)
								{
									break;
								}
							}
						}
						if (num7 == num6)
						{
							flag2 = true;
						}
					}
					for (int m = 0; m < num2; m++)
					{
						list[m].DoHeal(GetHealResult(_caster.character, list[m], list2[m]));
					}
				}
				else if (_abilityData.targetCount == TargetCount.Single)
				{
					list.Add(_target.character);
					_target.character.DoHeal(GetHealResult(_caster.character, _target.character, Mathf.Max(1, initialValue)));
				}
				else
				{
					list.Add(_caster.character);
					_caster.character.DoHeal(GetHealResult(_caster.character, _caster.character, Mathf.Max(1, initialValue)));
				}
				break;
			case TargetType.Enemy:
			case TargetType.All:
				break;
			}
			Character randomItem = list.FindAll((Character obj) => obj != _caster.character).GetRandomItem();
			if (randomItem != null && randomItem.characterDefinition.sounds.healSounds.soundPaths.Count > 0 && UnityEngine.Random.value < randomItem.characterDefinition.sounds.healSounds.probability)
			{
				GFAudioManager.instance.PlayClipFromResources(randomItem.characterDefinition.sounds.healSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
			break;
		}
		case AbilityType.DropBuff:
		{
			Debug.Log("\t\tProcess Ability, Drop Buff");
			BeatBoxType beatBoxType = ((_abilityData.buffDropType != BeatBoxType.Random) ? _abilityData.buffDropType : ((BeatBoxType)UnityEngine.Random.Range(0, 2)));
			Vector3 vector3Value = _abilityData.vector3Value1;
			vector3Value.x *= Mathf.Sign(_caster.transform.localScale.z);
			switch (beatBoxType)
			{
			case BeatBoxType.AttackUp:
			{
				Debug.Log("\t\tProcess Ability, Drop Buff, ATTACK");
				FirePickup firePickup = BattleObjectManager.instance.SpawnPickupAt<FirePickup>("Prefabs/GUI/Pickups/FirePickup", GetTopPosition(_caster, _abilityData.vector3Value1), _caster == BattleController.instance.playerCharacter);
				firePickup.attackUpAmount = Mathf.RoundToInt((_abilityData.buffDropType != BeatBoxType.Random) ? _abilityData.rangedIntValue1.GetInterpolatedValue(_abilityPosition / 100f) : _abilityData.rangedIntValue2.GetInterpolatedValue(_abilityPosition / 100f));
				break;
			}
			case BeatBoxType.Heal:
			{
				HeartPickup heartPickup = BattleObjectManager.instance.SpawnPickupAt<HeartPickup>("Prefabs/GUI/Pickups/HeartPickup", GetTopPosition(_caster, _abilityData.vector3Value1), _caster == BattleController.instance.playerCharacter);
				heartPickup.healPercent = (float)Mathf.RoundToInt(_abilityData.rangedIntValue1.GetInterpolatedValue(_abilityPosition / 100f)) / 100f;
				break;
			}
			}
			break;
		}
		case AbilityType.BuffOrDebuff:
		case AbilityType.Custom:
		case AbilityType.Transformation:
		case AbilityType.None:
			break;
		}
	}

	protected void ProcessStatusEffect(StatusEffectName effect, StatusEffectData data = null, Character character = null, ActionMeter meter = null, PortraitSwitcher portraits = null)
	{
		if (data == null)
		{
			StatusEffectData statusEffectData = new StatusEffectData();
			statusEffectData.intValue1 = _abilityData.intValue1;
			statusEffectData.intValue2 = ((_abilityData.statusEffect == StatusEffectName.TeamStatBuff) ? ((int)_abilityData.statType) : ((_abilityData.statusEffect != StatusEffectName.BuffDropper) ? _abilityData.intValue2 : ((int)_abilityData.beatBoxType)));
			statusEffectData.floatValue1 = _abilityData.floatValue1;
			statusEffectData.floatValue2 = _abilityData.floatValue2;
			statusEffectData.stringValue1 = _abilityData.stringValue1;
			statusEffectData.stringValue2 = _abilityData.stringValue2;
			statusEffectData.rangedIntValue1 = _abilityData.rangedIntValue1;
			statusEffectData.rangedIntValue2 = _abilityData.rangedIntValue2;
			statusEffectData.rangedFloatValue1 = _abilityData.rangedFloatValue1;
			statusEffectData.rangedFloatValue2 = _abilityData.rangedFloatValue2;
			statusEffectData.rangedFloatValue3 = _abilityData.rangedFloatValue3;
			statusEffectData.rangedFloatValue4 = _abilityData.rangedFloatValue4;
			statusEffectData.boolValue1 = _abilityData.boolValue1;
			data = statusEffectData;
		}
		else
		{
			data = new StatusEffectData(data);
		}
		if (effect == StatusEffectName.TeamStatBuff && (data.statType == CharacterStatType.Attack || data.statType == CharacterStatType.Luck))
		{
			data.intValue1 = TTGAbilityCalculator.CalculateTeamStatBonus(data, _caster.character, (int)_abilityPosition);
		}
		if (effect == StatusEffectName.AttackDrone || effect == StatusEffectName.Poison)
		{
			data.intValue1 = TTGAbilityCalculator.CalculateDamage(_abilityData, _caster.character, (int)_abilityPosition, true, true, false, isAbilityShiny);
		}
		if (character == null)
		{
			character = _target.character;
		}
		if (meter == null)
		{
			meter = _targetMeter;
		}
		if (portraits == null)
		{
			portraits = _targetPortraits;
		}
		switch (effect)
		{
		case StatusEffectName.ActionMeterDisabler:
			if (UnityEngine.Random.value >= meter.character.character.stunResistanceChance)
			{
				float interpolatedValue = data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
				if (_caster.character.modChip != null && _caster.character.modChip.uid == MODCHIP_SIMPLY_STUNNING_UID)
				{
					interpolatedValue *= 1.5f;
				}
				interpolatedValue *= _casterMeter.stunDurationFactor;
				StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ActionMeterDisabler(meter, portraits, interpolatedValue));
			}
			else
			{
				BattleDisplayManager.instance.ShowMessage(Language.Get("RESIST", "ABILS,CHIPS,ACHVS,LDBS"), meter.character);
			}
			break;
		case StatusEffectName.ActionMeterSpeedModifier:
		{
			float interpolatedValue = data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
			interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(interpolatedValue, _caster.character, _abilityData);
			int diminishedValue7 = DiminishingReturns.instance.GetDiminishedValue(data.intValue1, _caster.character, _abilityData, 1, -100);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ActionMeterSpeedModifier(meter, diminishedValue7, interpolatedValue));
			break;
		}
		case StatusEffectName.AttackDrone:
		{
			Drone objectFromResource4 = ObjectPool.GetObjectFromResource<Drone>(data.stringValue1, _caster.model.position);
			if (objectFromResource4 != null)
			{
				objectFromResource4.transform.localScale = _caster.transform.localScale;
			}
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.floatValue1, _caster.character, _abilityData);
			int diminishedValue6 = DiminishingReturns.instance.GetDiminishedValue(data.intValue1, _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new AttackDrone(objectFromResource4, meter, _caster.character, interpolatedValue, diminishedValue6, data.floatValue2));
			break;
		}
		case StatusEffectName.BuffDropper:
		{
			Beatbox objectFromResource2 = ObjectPool.GetObjectFromResource<Beatbox>(string.IsNullOrEmpty(data.stringValue1) ? "Prefabs/FX/BeatBox" : data.stringValue1, _caster.model.position);
			objectFromResource2.transform.localScale = _caster.transform.localScale;
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.floatValue1, _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new BuffDropper(objectFromResource2, meter, data.beatBoxType, Mathf.RoundToInt(data.rangedIntValue1.GetInterpolatedValue(_abilityPosition / 100f)), data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), data.rangedFloatValue2.GetInterpolatedValue(_abilityPosition / 100f)));
			break;
		}
		case StatusEffectName.AttackMinion:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.floatValue1, _caster.character, _abilityData);
			int attackDamage = Mathf.RoundToInt(data.rangedIntValue1.GetInterpolatedValue(_abilityPosition / 100f));
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new AttackMinion((AttackMinion.MinionType)data.intValue2, _caster, meter, interpolatedValue, attackDamage));
			break;
		}
		case StatusEffectName.Cute:
		{
			float interpolatedValue = data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
			interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(interpolatedValue, _caster.character, _abilityData);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, new Cute(character, meter, interpolatedValue));
			break;
		}
		case StatusEffectName.KnockedOut:
		{
			float interpolatedValue = data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
			interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(interpolatedValue, _caster.character, _abilityData);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, new KnockedOut(character, interpolatedValue));
			break;
		}
		case StatusEffectName.Poison:
		{
			PoisonPellet objectFromResource3 = ObjectPool.GetObjectFromResource<PoisonPellet>(data.stringValue1, _caster.model.position);
			if (objectFromResource3 != null)
			{
				objectFromResource3.transform.localScale = _caster.transform.localScale;
			}
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.floatValue1, _caster.character, _abilityData);
			int diminishedValue5 = DiminishingReturns.instance.GetDiminishedValue(data.intValue1, _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new Poison(objectFromResource3, meter, _caster.character, interpolatedValue, diminishedValue5, data.floatValue2));
			break;
		}
		case StatusEffectName.ProtectionBubble:
		{
			float interpolatedValue = data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
			interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(interpolatedValue, _caster.character, _abilityData);
			int initialValue = Mathf.RoundToInt(data.rangedIntValue1.GetInterpolatedValue(_abilityPosition / 100f));
			initialValue = DiminishingReturns.instance.GetDiminishedValue(initialValue, _caster.character, _abilityData, 1, 0);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ProtectionBubble(data.stringValue1, meter, interpolatedValue, data.floatValue2, initialValue));
			break;
		}
		case StatusEffectName.Smokescreen:
		{
			SmokeBomb objectFromResource = ObjectPool.GetObjectFromResource<SmokeBomb>(data.stringValue1, _caster.model.position);
			if (objectFromResource != null)
			{
				objectFromResource.InitWithCharacter(_caster);
			}
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new Smokescreen(objectFromResource, meter, interpolatedValue, Mathf.RoundToInt(data.floatValue2 * 100f), data.intValue1));
			break;
		}
		case StatusEffectName.TeamStatBuff:
		{
			float interpolatedValue = data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new TeamStatBuff(meter, data.statType, data.intValue1, data.floatValue1, data.boolValue1, interpolatedValue));
			break;
		}
		case StatusEffectName.AbilityBlocker:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new AbilityBlocker(meter, interpolatedValue));
			break;
		}
		case StatusEffectName.Immunity:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new Immunity(meter, interpolatedValue));
			break;
		}
		case StatusEffectName.Cuteness:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new Cuteness(meter, interpolatedValue, data.floatValue2));
			break;
		}
		case StatusEffectName.ActionMeterFiller:
		{
			float diminishedValue4 = DiminishingReturns.instance.GetDiminishedValue(data.floatValue1, _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ActionMeterFiller(meter, diminishedValue4));
			break;
		}
		case StatusEffectName.ActionMeterDepleter:
		{
			float diminishedValue3 = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ActionMeterDepleter(meter, diminishedValue3, data.floatValue2, data.intValue1));
			break;
		}
		case StatusEffectName.HealAndBuffBlocker:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new HealBuffBlocker(meter, interpolatedValue));
			break;
		}
		case StatusEffectName.AmplifyDamage:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new AmplifyDamage(meter, interpolatedValue, data.floatValue2, data.stringValue1, data.stringValue2));
			break;
		}
		case StatusEffectName.CharacterStatBuff:
		{
			int diminishedValue = DiminishingReturns.instance.GetDiminishedValue(data.intValue1, _caster.character, _abilityData);
			float diminishedValue2 = DiminishingReturns.instance.GetDiminishedValue(data.floatValue1, _caster.character, _abilityData);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, new CharacterStatBuff(character, meter, data.statType, diminishedValue, diminishedValue2));
			break;
		}
		case StatusEffectName.ActionMeterFreeze:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			float barAmount = Mathf.Clamp01(_abilityPosition / 100f * 2f);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ActionMeterFreeze(meter, interpolatedValue, barAmount));
			break;
		}
		case StatusEffectName.Flight:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, new Flight(character, meter, _targetCamera, _abilityData.uid, interpolatedValue));
			break;
		}
		case StatusEffectName.ActionMeterElectrify:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			float interpolatedValue3 = data.rangedFloatValue3.GetInterpolatedValue(_abilityPosition / 100f);
			float interpolatedValue4 = data.rangedFloatValue2.GetInterpolatedValue(_abilityPosition / 100f);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ActionMeterElectrify(meter, character, interpolatedValue, interpolatedValue3, interpolatedValue4));
			break;
		}
		case StatusEffectName.Root:
		{
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			Debug.Log("String value 2: " + data.stringValue2);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new Root(meter, portraits, interpolatedValue, data.stringValue1, data.stringValue2));
			break;
		}
		case StatusEffectName.Counterattack:
		{
			float percent = _abilityPosition / 100f;
			float interpolatedValue = DiminishingReturns.instance.GetDiminishedValue(data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f), _caster.character, _abilityData);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, new Counterattack(character, meter, _targetCamera, _abilityData.uid, interpolatedValue, data.rangedFloatValue3.GetInterpolatedValue(percent), data.rangedFloatValue2.GetInterpolatedValue(percent), data.stringValue1));
			break;
		}
		case StatusEffectName.RemoteBomb:
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new RemoteBomb(meter, _casterMeter, data.stringValue1, data.intValue1, data.intValue2));
			break;
		case StatusEffectName.HealOverTime:
			StatusEffectManager.instance.AddCharacterStatusEffect(character, new HealOverTime(character, meter, data.intValue1, data.floatValue2, data.floatValue1));
			break;
		case StatusEffectName.Fear:
		{
			int casterStatBonus = Mathf.RoundToInt(data.rangedIntValue1.GetInterpolatedValue(_abilityPosition / 100f));
			int targetStatBonus = Mathf.RoundToInt(data.rangedIntValue2.GetInterpolatedValue(_abilityPosition / 100f));
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new Fear(_casterMeter, meter, data.statType, casterStatBonus, targetStatBonus));
			break;
		}
		case StatusEffectName.ActionMeterHack:
		{
			float interpolatedValue = data.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
			RangedFloat damageModifierRange = new RangedFloat(data.rangedFloatValue2.GetInterpolatedValue(_abilityPosition / 100f), data.rangedFloatValue3.GetInterpolatedValue(_abilityPosition / 100f));
			float interpolatedValue2 = data.rangedFloatValue4.GetInterpolatedValue(_abilityPosition / 100f);
			StatusEffectManager.instance.AddActionMeterStatusEffect(meter, new ActionMeterHack(meter, interpolatedValue, damageModifierRange, data.floatValue1, data.floatValue2, interpolatedValue2));
			break;
		}
		default:
			Debug.LogErrorFormat("Ability Script, ProcessStatusEffect(), Attempted to add status effect {0} but commands to add effect are not defined.", effect.ToString());
			break;
		}
	}

	public virtual void CleanUp()
	{
		_targetMeter.CharacterChanged -= HandleCharacterChanged;
		_caster.animatorEventListener.EventTriggered -= HandleEventTriggered;
		if (_caster.faceAnimatorEventListener != null)
		{
			_caster.faceAnimatorEventListener.EventTriggered -= HandleFaceEventTriggered;
		}
		if (_caster.extrasAnimatorEventListener != null)
		{
			_caster.extrasAnimatorEventListener.EventTriggered -= HandleExtrasEventTriggered;
		}
		_casterCamera.OffsetInStart -= HandleOffsetInStart;
		_casterCamera.OffsetOutStart -= HandleOffsetOutStart;
		_casterCamera.OffsetInStart -= HandleShowingTeam;
		_casterCamera.OffsetInComplete -= HandleShowingTeam;
		_casterCamera.OffsetOutComplete -= HandleRemovingTeam;
		otherTargetCharacters.Clear();
	}

	private void HandleCharacterChanged()
	{
		_target = _targetMeter.character;
	}

	protected virtual void HandleEventTriggered(AnimationEvent evt)
	{
	}

	protected virtual void HandleFaceEventTriggered(AnimationEvent evt)
	{
	}

	protected virtual void HandleExtrasEventTriggered(AnimationEvent evt)
	{
	}

	protected void CheckForItemSpawns(bool allowTofu = true)
	{
		if (BattleController.instance.enemyParty.Contains(_target.character))
		{
			UnityEngine.Random.State state = UnityEngine.Random.state;
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				UnityEngine.Random.InitState(Environment.TickCount);
			}
			Vector3 worldPoint = ((!BattleController.instance.splitCameras.player2Camera.camera.enabled) ? targetTopPosition : BattleController.instance.guiCamera.ViewportToWorldPoint(new Vector3(0.65f, 0.65f)));
			BattleObjectManager.instance.CheckForItemSpawns(worldPoint, allowTofu && !BattleController.instance.isEnemyPartyDead);
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				UnityEngine.Random.state = state;
			}
		}
	}

	protected void ProcessShinyEffect()
	{
		AbilityShinyEffect shinyEffect = _abilityData.shinyEffect;
		ProcessShinyEffect(shinyEffect);
		AbilityShinyEffect currentBatteryEffectOnMeterAbility = StatusEffectManager.instance.GetCurrentBatteryEffectOnMeterAbility(_casterMeter, _abilityData);
		if (currentBatteryEffectOnMeterAbility != null)
		{
			ProcessShinyEffect(currentBatteryEffectOnMeterAbility);
		}
	}

	protected void ProcessShinyEffect(AbilityShinyEffect effect)
	{
		if (effect.probability <= 0f || UnityEngine.Random.value > effect.probability)
		{
			return;
		}
		BattleCharacter battleCharacter;
		Character character;
		ActionMeter meter;
		PortraitSwitcher portraits;
		if (effect.targetType == TargetType.Friendly)
		{
			battleCharacter = _caster;
			character = battleCharacter.character;
			meter = _casterMeter;
			portraits = _casterPortraits;
		}
		else if (BattleController.instance.playerParty.Contains(_caster.character))
		{
			battleCharacter = BattleController.instance.enemyCharacter;
			character = battleCharacter.character;
			meter = BattleController.instance.enemyActionMeter;
			portraits = BattleController.instance.enemyPortraits;
		}
		else
		{
			battleCharacter = BattleController.instance.playerCharacter;
			character = battleCharacter.character;
			meter = BattleController.instance.playerActionMeter;
			portraits = BattleController.instance.playerPortraits;
		}
		bool flag = _caster == BattleController.instance.playerCharacter;
		switch (effect.effectType)
		{
		case AbilityShinyEffectType.AddStatusEffect:
			ProcessStatusEffect(effect.statusEffect, effect.statusEffectData, character, meter, portraits);
			break;
		case AbilityShinyEffectType.DropBuff:
			switch (effect.beatBoxType)
			{
			case BeatBoxType.AttackUp:
			{
				FirePickup firePickup = BattleObjectManager.instance.SpawnPickupAt<FirePickup>("Prefabs/GUI/Pickups/FirePickup", GetCenterPosition(battleCharacter), flag);
				break;
			}
			case BeatBoxType.Heal:
			{
				HeartPickup heartPickup = BattleObjectManager.instance.SpawnPickupAt<HeartPickup>("Prefabs/GUI/Pickups/HeartPickup", GetCenterPosition(battleCharacter), flag);
				heartPickup.healPercent = (float)Mathf.RoundToInt(effect.rangedIntValue1.GetInterpolatedValue(_abilityPosition / 100f)) / 100f;
				break;
			}
			}
			break;
		case AbilityShinyEffectType.DropTofu:
			BattleObjectManager.instance.SpawnTofuAt(GetCenterPosition(battleCharacter), flag);
			break;
		}
	}

	protected void ShakeCamera(BattleCamera battleCamera, float strength)
	{
		DOTween.Kill(battleCamera.camera.transform, true);
		battleCamera.camera.transform.DOShakePosition(strength + 0.1f, new Vector3(strength, strength, strength), 35, 70f);
	}

	private void HandleOffsetInStart()
	{
		if (_abilityData.type == AbilityType.Attack)
		{
			DOTween.To(() => _target.straightLookWeight, (float x) =>
			{
				_target.straightLookWeight = x;
			}, 1f, 0.25f);
		}
	}

	private void HandleOffsetOutStart()
	{
		if (_abilityData.type == AbilityType.Attack)
		{
			DOTween.To(() => _target.straightLookWeight, (float x) =>
			{
				_target.straightLookWeight = x;
			}, 0f, 0.25f);
		}
	}

	private void HandleShowingTeam()
	{
		if (_abilityData.targetCount != TargetCount.All || !(_abilityData.channelingData.speed <= 0f))
		{
			return;
		}
		int num = 0;
		otherTargetCharacters.Clear();
		int num2 = _targetParty.IndexOf(_target.character) + 1;
		if (num2 >= _targetParty.Count)
		{
			num2 = 0;
		}
		for (int i = 0; i < _targetParty.Count - 1; i++)
		{
			if (!_targetParty[num2].isDead && _targetParty[num2].characterDefinition.characterName != "Shadow Fig")
			{
				CharacterTransformation activeTransformation = _targetParty[num2].activeTransformation;
				string path = ((_targetParty == BattleController.instance.playerParty) ? ((activeTransformation != null) ? activeTransformation.battleCharacterPath : _targetParty[num2].characterDefinition.battleCharacterPath) : ((activeTransformation != null) ? ((!string.IsNullOrEmpty(activeTransformation.battleCharacterPathP2)) ? activeTransformation.battleCharacterPathP2 : activeTransformation.battleCharacterPath) : ((!string.IsNullOrEmpty(_targetParty[num2].characterDefinition.battleCharacterPathP2)) ? _targetParty[num2].characterDefinition.battleCharacterPathP2 : _targetParty[num2].characterDefinition.battleCharacterPath)));
				BattleCharacter battleCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(path));
				battleCharacter.transform.localScale = _target.transform.localScale;
				if (_caster == BattleController.instance.playerCharacter)
				{
					battleCharacter.transform.localPosition = _target.transform.localPosition + ((num != 0) ? (BattleController.instance.thirdCharacterOffset * -1f) : (BattleController.instance.secondCharacterOffset * -1f));
				}
				else
				{
					battleCharacter.transform.localPosition = _target.transform.localPosition + ((num != 0) ? BattleController.instance.secondCharacterOffset : BattleController.instance.thirdCharacterOffset);
				}
				battleCharacter.character = _targetParty[num2];
				battleCharacter.Play("swap in");
				if (Mathf.Approximately(_target.dancingWeight, 1f))
				{
					battleCharacter.dancingWeight = 1f;
				}
				else if (_abilityData.targetType == TargetType.Enemy)
				{
					battleCharacter.straightLookWeight = 1f;
				}
				otherTargetCharacters.Add(battleCharacter);
				num++;
			}
			num2++;
			if (num2 >= _targetParty.Count)
			{
				num2 = 0;
			}
		}
	}

	private void HandleRemovingTeam()
	{
		if (_abilityData.targetCount == TargetCount.All)
		{
			int count = otherTargetCharacters.Count;
			for (int i = 0; i < count; i++)
			{
				BattleController.instance.StartCoroutine(CharacterExitSequence(otherTargetCharacters[i]));
			}
		}
	}

	private IEnumerator CharacterExitSequence(BattleCharacter character)
	{
		if (!character.character.isDead)
		{
			character.Play("swap out");
			yield return null;
			yield return null;
			while (character.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
		}
		character.character = null;
		ObjectPool.PoolObject(character.gameObject);
	}

	protected DamageResult GetDamageResult(Character caster, Character target, int initialDamage, ImpactType impactType = ImpactType.Normal, bool isKillingBlow = true)
	{
		DamageResult damageResult = new DamageResult(caster, target);
		damageResult.impactType = impactType;
		damageResult.isKillingBlow = isKillingBlow;
		damageResult.abilityData = _abilityData;
		damageResult.damage = initialDamage;
		int num = UnityEngine.Random.Range(1, 100);
		int num2 = Mathf.FloorToInt((float)caster.luck * caster.luckModifier) + caster.tempLuckBonus;
		Debug.LogFormat("[AbilityScript::GetDamageResult] - Random Num: {0}    Luck Check Num: {1}", num, num2);
		if (num <= num2)
		{
			damageResult.luckBonusDamage = Mathf.CeilToInt((float)initialDamage * GameManager.instance.gameSettings.luckyBonusDamage);
		}
		float num3 = 0f;
		if (isKillingBlow && classStrengths.ContainsKey(caster.characterDefinition.characterClass) && classStrengths[caster.characterDefinition.characterClass] == target.characterDefinition.characterClass)
		{
			num3 = (int)Mathf.Max(1f, (float)initialDamage * GameManager.instance.gameSettings.classBonusDamage);
		}
		damageResult.classBonusDamage = (int)num3;
		damageResult.bonusClass = caster.characterDefinition.characterClass;
		if (CheckDodge())
		{
			num = UnityEngine.Random.Range(1, 100);
			int num4 = Mathf.FloorToInt((float)target.dodge * target.dodgeModifier) + target.tempDodgeBonus;
			if (!target.isDead && num <= num4)
			{
				damageResult.isDodged = true;
			}
			Debug.LogFormat("[AbilityScript::GetDamageResult] - Target Name: {0}    Target Dodge: {1}    Target Dodge Modifier: {2}    Target Temp Dodge Bonus: {3}    Random Num: {4}    Dodge Check Num: {5}    Is Dodged: {6}", target.characterDefinition.characterName, target.dodge, target.dodgeModifier, target.tempDodgeBonus, num, num4, damageResult.isDodged);
		}
		return damageResult;
	}

	protected bool CheckDodge()
	{
		return (!_caster.character.IsAbilityShiny(_abilityData) || _abilityData.shinyEffect.effectType != AbilityShinyEffectType.ModifyAbilityValues) ? _abilityData.isDodgeable : _abilityData.shinyEffect.isDodgeable;
	}

	protected DamageResult GetDamageResult(Character caster, Character target, AbilityData ability, ImpactType impactType = ImpactType.Normal, bool isKillingBlow = true)
	{
		int initialDamage = TTGAbilityCalculator.CalculateDamage(ability, caster, (int)_abilityPosition, true, true, true, isAbilityShiny);
		return GetDamageResult(caster, target, initialDamage, impactType, isKillingBlow);
	}

	protected void GetTeamAttackDamage(int totalDamage, out int mainCharacterDamage, out int sideCharacterDamage)
	{
		int num = 0;
		for (int i = 0; i < _targetParty.Count; i++)
		{
			if (!_targetParty[i].isDead && (_targetParty[i].characterDefinition.characterName != "Shadow Fig" || _targetParty[i] == _target.character))
			{
				num++;
			}
		}
		num = Mathf.Max(1, num);
		sideCharacterDamage = Mathf.FloorToInt((float)totalDamage / (float)num);
		mainCharacterDamage = sideCharacterDamage + totalDamage % num;
	}

	protected DamageResult GetSplashDamage(BattleCharacter caster, BattleCharacter target, AbilityData ability)
	{
		int damage = Mathf.Max(1, ability.splashDamage + (Mathf.FloorToInt((float)caster.character.AttackAbilityPositionModified(_abilityPosition) * caster.character.attackModifier) + caster.character.GetTempAttackBonusForSlotPosition(_abilityPosition)));
		return new DamageResult(damage, 0, caster.characterDefinition.characterClass, caster.character, target.character);
	}

	protected HealResult GetHealResult(Character caster, Character target, int initialAmount)
	{
		return new HealResult(caster, target, initialAmount);
	}

	public Vector3 GetTopPosition(BattleCharacter battleCharacter, Vector3 offset = default(Vector3))
	{
		Camera guiCamera = BattleController.instance.guiCamera;
		Vector3 position = _casterCamera.camera.WorldToScreenPoint(battleCharacter.top.position + offset);
		return guiCamera.ScreenToWorldPoint(position);
	}

	public Vector3 GetCenterPosition(BattleCharacter battleCharacter, Vector3 offset = default(Vector3))
	{
		Camera guiCamera = BattleController.instance.guiCamera;
		Vector3 position = _casterCamera.camera.WorldToScreenPoint(battleCharacter.center.position + offset);
		return guiCamera.ScreenToWorldPoint(position);
	}

	public abstract IEnumerator Play();
}
