using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AbilityScript_BatteryDrain : AbilityScript
{
	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			_caster.Play("batterydrain", isAbilityAnimation: true);
			_casterCamera.Play("batterydrain", isAbilityAnimation: true);
		}
		else
		{
			_caster.Play(_abilityData.animName, isAbilityAnimation: true);
			_casterCamera.Play(_abilityData.animName, isAbilityAnimation: true);
		}
		yield return null;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string stringParameter = evt.stringParameter;
		if (stringParameter != null && stringParameter == "hit")
		{
			float interpolatedValue = _abilityData.rangedFloatValue1.GetInterpolatedValue(_abilityPosition / 100f);
			DOTween.To(() => _targetMeter.progress, (float x) =>
			{
				_targetMeter.progress = x;
			}, Mathf.Clamp01(_targetMeter.progress + interpolatedValue), (float)evt.intParameter / 60f).SetEase(Ease.Linear);
			_caster.character.DoDamage(new DamageResult(_abilityData.GetHealthCost(_caster.character), ImpactType.None, isKillingBlow: false));
			if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit)
			{
				ProcessShinyEffect();
			}
		}
	}
}
