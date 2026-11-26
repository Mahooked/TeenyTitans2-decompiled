using UnityEngine;

[RequireComponent(typeof(AbilityButton))]
public class AbilityButtonShinyEffect : MonoBehaviour
{
	[SerializeField]
	private AbilityButton button;

	private Character _character;

	private AbilityData _abilityData;

	private bool isPrepared;

	private void Awake()
	{
		if (button == null)
		{
			button = GetComponent<AbilityButton>();
		}
	}

	private void Reset()
	{
		button = GetComponent<AbilityButton>();
	}

	private void OnDestroy()
	{
		if (_character != null && _abilityData != null)
		{
			Unprepare();
		}
		_character = null;
		_abilityData = null;
	}

	public void Init(Character character, AbilityData abilityData)
	{
		if (character != _character || !(abilityData == _abilityData))
		{
			if (isPrepared)
			{
				Unprepare();
			}
			_character = character;
			_abilityData = abilityData;
			if (_character != null && _abilityData != null)
			{
				Prepare();
			}
		}
	}

	private void Prepare()
	{
		if (!isPrepared)
		{
			AbilityShinyEffect abilityShinyEffect = null;
			if (abilityShinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit && _abilityData.type == AbilityType.Attack)
			{
				_character.DamageDealt += HandleDamageDealt;
			}
			isPrepared = true;
		}
	}

	private void Unprepare()
	{
		if (isPrepared)
		{
			AbilityShinyEffect abilityShinyEffect = null;
			if (abilityShinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit && _abilityData.type == AbilityType.Attack)
			{
				_character.DamageDealt -= HandleDamageDealt;
			}
			isPrepared = false;
		}
	}

	private void ProcessEffect(Character target)
	{
		AbilityShinyEffect abilityShinyEffect = null;
		AbilityShinyEffectType effectType = abilityShinyEffect.effectType;
		if (effectType == AbilityShinyEffectType.AddStatusEffect)
		{
		}
	}

	private void HandleDamageDealt(DamageResult damageResult)
	{
		if (!damageResult.isDodged)
		{
			ProcessEffect(damageResult.target);
		}
	}
}
