using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
	private List<StatusEffect> _allStatusEffects = new List<StatusEffect>();

	private int nextUid;

	private static StatusEffectManager _instance;

	private int NextUid => nextUid++;

	public static StatusEffectManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("StatusEffectManager").AddComponent<StatusEffectManager>();
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Object.Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	private void OnDestroy()
	{
		if (_instance != null && _instance == this)
		{
			_instance = null;
		}
	}

	private void Update()
	{
		int num = _allStatusEffects.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (num2 < _allStatusEffects.Count)
			{
				_allStatusEffects[num2].Update();
			}
		}
	}

	public void AddCharacterStatusEffect(Character character, StatusEffect effect)
	{
		if (!effect.isStackable)
		{
			StatusEffect statusEffect = character.statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(effect.GetType()));
			if (statusEffect != null)
			{
				if (effect.isResettable)
				{
					statusEffect.Reset();
					return;
				}
				statusEffect.Remove();
			}
		}
		effect.uid = NextUid;
		effect.Removed += HandleStatusEffectRemoved;
		_allStatusEffects.Add(effect);
		character.AddStatusEffect(effect);
		if (!effect.isRemoved)
		{
			effect.Init();
		}
	}

	public void AddActionMeterStatusEffect(ActionMeter meter, StatusEffect effect)
	{
		if (!effect.isStackable)
		{
			StatusEffect statusEffect = meter.statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(effect.GetType()));
			if (statusEffect != null)
			{
				if (effect.isResettable)
				{
					statusEffect.Reset();
					return;
				}
				statusEffect.Remove();
			}
		}
		effect.uid = NextUid;
		effect.Removed += HandleStatusEffectRemoved;
		_allStatusEffects.Add(effect);
		meter.AddStatusEffect(effect);
		if (!effect.isRemoved)
		{
			effect.Init();
			Debug.LogFormat("[StatusEffectManager::AddActionMeterStatusEffect] - Successfully added Action Meter status effect of type \"{0}\" with UID {1}", effect.GetType(), effect.uid);
		}
	}

	public StatusEffect GetStatusEffectByUid(int uid)
	{
		for (int i = 0; i < _allStatusEffects.Count; i++)
		{
			if (_allStatusEffects[i].uid == uid)
			{
				return _allStatusEffects[i];
			}
		}
		return null;
	}

	public bool CharacterHasAbilityBlockerOnButton(Character character, AbilityButton button)
	{
		if (character == null)
		{
			return false;
		}
		for (int i = 0; i < character.statusEffects.Count; i++)
		{
			StatusEffect statusEffect = character.statusEffects[i];
			if (statusEffect is CharacterAbilityBlocker && ((CharacterAbilityBlocker)statusEffect).button == button)
			{
				return true;
			}
		}
		return false;
	}

	public bool MeterHasAbilityBlockerOnButton(ActionMeter meter, AbilityButton button)
	{
		if (meter == null)
		{
			return false;
		}
		for (int i = 0; i < meter.statusEffects.Count; i++)
		{
			StatusEffect statusEffect = meter.statusEffects[i];
			if (statusEffect is AbilityBlocker && ((AbilityBlocker)statusEffect).button == button)
			{
				return true;
			}
		}
		return false;
	}

	public AbilityShinyEffect GetCurrentBatteryEffectOnMeterAbility(ActionMeter meter, AbilityData abilityData)
	{
		return instance.GetAbilityBatteryBuffOnButton(meter, abilityData)?.CurrentAbilityBatteryEffect();
	}

	public AbilityBatteryBuff GetAbilityBatteryBuffOnButton(ActionMeter meter, AbilityData abilityData)
	{
		if (meter == null)
		{
			return null;
		}
		for (int i = 0; i < meter.statusEffects.Count; i++)
		{
			StatusEffect statusEffect = meter.statusEffects[i];
			if (statusEffect is AbilityBatteryBuff && ((AbilityBatteryBuff)statusEffect).abilityData == abilityData)
			{
				return (AbilityBatteryBuff)statusEffect;
			}
		}
		return null;
	}

	private void HandleStatusEffectRemoved(StatusEffect effect)
	{
		effect.Removed -= HandleStatusEffectRemoved;
		_allStatusEffects.Remove(effect);
		Debug.LogFormat("[StatusEffectManager::HandleStatusEffectRemoved] - Successfully removed status effect of type \"{0}\" with UID {1}", effect.GetType(), effect.uid);
	}
}
