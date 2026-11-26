using System.Collections;
using UnityEngine;

public class AbilityScript_BurpSurprise : AbilityScript
{
	private ObjectTosser objectTosser;

	private TossObjectData tossObjectData;

	private TossObject tossObject;

	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			_caster.Play("burp surprise", isAbilityAnimation: true);
			_casterCamera.Play("burp surprise", isAbilityAnimation: true);
		}
		else
		{
			_caster.Play(_abilityData.animName, isAbilityAnimation: true);
			_casterCamera.Play(_abilityData.animName, isAbilityAnimation: true);
		}
		objectTosser = ObjectPool.GetObjectFromResource<ObjectTosser>("Prefabs/FX/SilkieObjectTosser");
		objectTosser.transform.localScale = _caster.transform.localScale;
		if (_abilityData.tossObjects.Count > 1)
		{
			int num = 0;
			int count = _abilityData.tossObjects.Count;
			for (int i = 0; i < count; i++)
			{
				num += _abilityData.tossObjects[i].weight;
			}
			int num2 = Random.Range(0, num);
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				num3 += _abilityData.tossObjects[j].weight;
				if (num2 < num3)
				{
					tossObjectData = _abilityData.tossObjects[j];
					break;
				}
			}
		}
		else
		{
			tossObjectData = _abilityData.tossObjects[0];
		}
		tossObject = ObjectPool.GetObjectFromResource<TossObject>(tossObjectData.path);
		if (tossObject != null)
		{
			tossObject.transform.parent = objectTosser.objectContainer;
			tossObject.transform.localPosition = Vector3.zero;
			tossObject.transform.localRotation = Quaternion.identity;
			tossObject.transform.localScale = Vector3.one * 0.1f;
		}
		yield break;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		switch (evt.stringParameter)
		{
		case "launch":
			if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnLaunch)
			{
				ProcessShinyEffect();
			}
			break;
		case "hit":
		{
			int initialDamage = TTGAbilityCalculator.CalculateTossObjectDamage(tossObjectData, _caster.character, (int)_abilityPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true);
			DamageResult damageResult = GetDamageResult(_caster.character, _target.character, initialDamage);
			_target.character.DoDamage(damageResult);
			if (!damageResult.isDodged)
			{
				ShakeCamera(_casterCamera, tossObject.shakeIntensity);
				CheckForItemSpawns();
			}
			break;
		}
		}
	}

	public override void CleanUp()
	{
		base.CleanUp();
		ObjectPool.PoolObject(tossObject.gameObject);
		ObjectPool.PoolObject(objectTosser.gameObject);
		objectTosser = null;
		tossObject = null;
	}
}
