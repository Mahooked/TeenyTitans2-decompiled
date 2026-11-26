using System.Collections;
using UnityEngine;

public class AbilityScript_Tofu : AbilityScript
{
	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			_caster.Play("tofu", isAbilityAnimation: true);
			_casterCamera.Play("tofu", isAbilityAnimation: true);
		}
		else
		{
			_caster.Play(_abilityData.animName, isAbilityAnimation: true);
			_casterCamera.Play(_abilityData.animName, isAbilityAnimation: true);
		}
		yield break;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		string text = array[0];
		if (text == null || !(text == "hit"))
		{
			return;
		}
		Transform transform = null;
		if (!string.IsNullOrEmpty(_abilityData.stringValue1))
		{
			transform = _caster.animator.transform.Find(_abilityData.stringValue1);
		}
		if (transform == null)
		{
			transform = _caster.center;
		}
		if (transform != null)
		{
			Camera guiCamera = BattleController.instance.guiCamera;
			Vector3 position = _casterCamera.camera.WorldToScreenPoint(transform.position);
			Vector3 position2 = guiCamera.ScreenToWorldPoint(position);
			position2.x += _abilityData.vector3Value1.x * Mathf.Sign(_caster.transform.localScale.z);
			position2.y += _abilityData.vector3Value1.y;
			BattleObjectManager.instance.SpawnTofuAt(position2, _caster == BattleController.instance.playerCharacter);
			if (isAbilityShiny && _abilityData.shinyEffect.triggerType == AbilityShinyEffectTriggerType.OnHit)
			{
				ProcessShinyEffect();
			}
		}
	}
}
