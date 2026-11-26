using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AbilityScript_SoopaPortal : AbilityScript
{
	private int smallHitDamage;

	private int mainHitDamage;

	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			_caster.Play("soopa portal", true);
			_casterCamera.Play("soopa portal", true);
		}
		else
		{
			_caster.Play(_abilityData.animName, true);
			_casterCamera.Play(_abilityData.animName, true);
		}
		int num = TTGAbilityCalculator.CalculateDamage(_abilityData, _caster.character, (int)_abilityPosition, true, true, true, isAbilityShiny);
		int numHits = _abilityData.numHits;
		smallHitDamage = Mathf.FloorToInt((float)num / (float)numHits);
		mainHitDamage = smallHitDamage + num % numHits;
		yield break;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		DamageResult damageResult = null;
		switch (evt.stringParameter)
		{
		case "lowerTarget":
			_target.transform.DOLocalMoveY(-2.5f, (float)evt.intParameter / 60f).SetEase(Ease.InCubic);
			break;
		case "resetTarget":
		{
			Vector3 localPosition = _target.transform.localPosition;
			localPosition.y = 0f;
			_target.transform.localPosition = localPosition;
			break;
		}
		case "smallHit":
			damageResult = GetDamageResult(_caster.character, _target.character, smallHitDamage, ImpactType.Small, false);
			_target.character.DoDamage(damageResult);
			if (!damageResult.isDodged)
			{
				DOTween.Kill(_casterCamera.camera.transform, true);
				_casterCamera.camera.transform.DOShakePosition(0.25f, new Vector3(0.2f, 0.2f, 0.2f), 35, 70f);
				if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit)
				{
					ProcessShinyEffect();
				}
			}
			break;
		case "hit":
			damageResult = GetDamageResult(_caster.character, _target.character, mainHitDamage);
			damageResult.damage = DiminishingReturns.instance.GetDiminishedValue(damageResult.damage, _caster.character, _abilityData);
			_target.character.DoDamage(damageResult);
			if (!damageResult.isDodged)
			{
				DOTween.Kill(_casterCamera.camera.transform, true);
				_casterCamera.camera.transform.DOShakePosition(0.5f, new Vector3(0.4f, 0.4f, 0.4f), 35, 70f);
				if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit)
				{
					ProcessShinyEffect();
				}
				CheckForItemSpawns();
			}
			break;
		}
	}
}
