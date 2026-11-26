using System.Collections;
using UnityEngine;

public class AbilityScript_ThreeArrows : AbilityScript
{
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

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string stringParameter = evt.stringParameter;
		if (stringParameter != null && stringParameter == "hit")
		{
			ProcessAbility();
		}
	}

	protected override void HandleExtrasEventTriggered(AnimationEvent evt)
	{
		base.HandleExtrasEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		string text = array[0];
		if (text != null && text == "addExplosion")
		{
			Transform transform = _caster.extrasAnimator.transform.Find(array[1]);
			if (transform != null)
			{
				ObjectPool.GetObjectFromResource("Prefabs/FX/OmniExplosion", transform.position);
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_abilities_beatboxexplode", GFAudioManager.SFX_GROUP, string.Empty);
			ShakeCamera(_casterCamera, 0.4f);
		}
	}
}
