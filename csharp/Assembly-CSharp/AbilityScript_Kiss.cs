using System.Collections;
using UnityEngine;

public class AbilityScript_Kiss : AbilityScript
{
	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			_caster.Play("nuhuh", isAbilityAnimation: true);
			_casterCamera.Play("nuhuh", isAbilityAnimation: true);
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
		if (text != null && text == "hit")
		{
			ProcessAbility();
			if (array.Length > 1 && array[1].Equals("shake"))
			{
				ShakeCamera(_casterCamera, evt.floatParameter);
			}
		}
	}
}
