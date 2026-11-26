using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TeamStatBuff : StatusEffect
{
	private ActionMeter targetMeter;

	private CharacterStatType statType;

	private int statBonus;

	private float statModifier;

	private bool removeOnlyOnDurationElapsed;

	private float duration;

	private Character target;

	private float timer;

	private GameObject particle;

	public override StatusEffectType type
	{
		get
		{
			return (statBonus > 0 || statModifier > 0f) ? StatusEffectType.Buff : StatusEffectType.Debuff;
		}
	}

	public override bool isStackable
	{
		get
		{
			return true;
		}
	}

	public override bool isResettable
	{
		get
		{
			return false;
		}
	}

	public override bool isZoomedOut
	{
		get
		{
			return false;
		}
	}

	public TeamStatBuff(ActionMeter targetMeter, CharacterStatType statType, int statBonus, float statModifier = 0f, bool removeOnlyOnDurationElapsed = false, float duration = -1f, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.statType = statType;
		this.statBonus = statBonus;
		this.statModifier = statModifier;
		this.removeOnlyOnDurationElapsed = removeOnlyOnDurationElapsed;
		this.duration = duration;
	}

	private void HandleCharacterChanged()
	{
		if (target != null)
		{
			if (statBonus != 0)
			{
				target.DoBuff(new BuffResult(statType, -statBonus, null, null, false));
			}
			if (statModifier != 0f)
			{
				target.DoBuff(new BuffResult(0f - statModifier, statType, null, null, false));
			}
			target.DamageDealt -= HandleDamageDealt;
			target.HealDealt -= HandleHealDealt;
		}
		target = ((!(targetMeter.character == null)) ? targetMeter.character.character : null);
		if (target != null)
		{
			if (statBonus != 0)
			{
				target.DoBuff(new BuffResult(statType, statBonus, null, null, false));
			}
			if (statModifier != 0f)
			{
				target.DoBuff(new BuffResult(statModifier, statType, null, null, false));
			}
			target.DamageDealt += HandleDamageDealt;
			target.HealDealt += HandleHealDealt;
		}
	}

	private void HandleAbilityUsed(ActionMeter meter, Character character, AbilityData abilityData)
	{
		if (!removeOnlyOnDurationElapsed && abilityData.type == AbilityType.StatusEffect && (abilityData.statusEffect == StatusEffectName.AttackDrone || abilityData.statusEffect == StatusEffectName.Poison))
		{
			Remove();
		}
	}

	protected virtual void HandleDamageDealt(DamageResult damageResult)
	{
		if (!removeOnlyOnDurationElapsed && damageResult.isDirect && damageResult.isKillingBlow)
		{
			Remove();
		}
	}

	protected virtual void HandleHealDealt(HealResult healResult)
	{
		if (!removeOnlyOnDurationElapsed && statType == CharacterStatType.Attack)
		{
			Remove();
		}
	}

	protected override void InitEffect()
	{
		targetMeter.CharacterChanged += HandleCharacterChanged;
		targetMeter.AbilityUsed += HandleAbilityUsed;
		if (targetMeter.character != null && targetMeter.character.character != null)
		{
			target = targetMeter.character.character;
			if (statBonus != 0)
			{
				target.DoBuff(new BuffResult(statType, statBonus, null, null, statType == CharacterStatType.Attack || statType == CharacterStatType.Luck));
			}
			if (statModifier != 0f)
			{
				target.DoBuff(new BuffResult(statModifier, statType, null, null, statType == CharacterStatType.Attack || statType == CharacterStatType.Luck));
			}
			target.DamageDealt += HandleDamageDealt;
			target.HealDealt += HandleHealDealt;
			if (statType == CharacterStatType.Luck)
			{
				particle = ObjectPool.GetObjectFromResource("Prefabs/FX/LuckBuff_Particle");
				particle.transform.localScale = targetMeter.character.transform.localScale;
			}
		}
		if (removeOnlyOnDurationElapsed)
		{
			timer = duration;
		}
	}

	protected override void UpdateEffect()
	{
		if (removeOnlyOnDurationElapsed && !BattleController.instance.waiting)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				Remove();
			}
		}
	}

	protected override void ResetEffect()
	{
	}

	protected override void RemoveEffect()
	{
		targetMeter.CharacterChanged -= HandleCharacterChanged;
		targetMeter.AbilityUsed -= HandleAbilityUsed;
		if (target != null)
		{
			if (statBonus != 0)
			{
				target.DoBuff(new BuffResult(statType, -statBonus, null, null, false));
			}
			if (statModifier != 0f)
			{
				target.DoBuff(new BuffResult(0f - statModifier, statType, null, null, false));
			}
			target.DamageDealt -= HandleDamageDealt;
			target.HealDealt -= HandleHealDealt;
		}
		if (particle != null)
		{
			BattleController.instance.StartCoroutine(RemoveParticles());
		}
	}

	private IEnumerator RemoveParticles()
	{
		ParticleSystem pSystem = particle.GetComponentInChildren<ParticleSystem>();
		pSystem.EnableEmission(false);
		DOTween.To(() => pSystem.playbackSpeed, (float x) =>
		{
			pSystem.playbackSpeed = x;
		}, 4f, 0.5f).SetEase(Ease.Linear);
		while (pSystem.particleCount > 0)
		{
			yield return null;
		}
		pSystem.EnableEmission(true);
		pSystem.playbackSpeed = 1f;
		ObjectPool.PoolObject(particle.gameObject);
		particle = null;
	}

	protected override void CleanUp()
	{
	}
}
