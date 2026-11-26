using System.Collections;
using UnityEngine;

public class AbilityScript_RemoteBombPlace : AbilityScript
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
			_caster.Play(_abilityData.animName, true);
			_casterCamera.Play(_abilityData.animName, true);
		}
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		switch (array[0])
		{
		case "hit":
		{
			ProcessStatusEffect(_abilityData.statusEffect);
			AbilityButton abilityButton = _casterMeter.abilityButtons.Find((AbilityButton x) => x.abilityData.scriptName == GetType().ToString());
			RemoteBomb remoteBomb = (RemoteBomb)_targetMeter.statusEffects.Find((StatusEffect obj) => obj is RemoteBomb && ((RemoteBomb)obj).CasterCharacter == _caster.character);
			if (remoteBomb != null)
			{
				remoteBomb.SetAbilityButton(abilityButton);
				AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_abilityData.intValue1);
				ActionMeter targetMeterLocal = _targetMeter;
				abilityButton.SwapAbility(ability, () => targetMeterLocal.HasStatusEffect<RemoteBomb>());
			}
			if (array.Length > 1 && array[1].Equals("shake"))
			{
				ShakeCamera(_casterCamera, evt.floatParameter);
			}
			break;
		}
		case "shake":
			ShakeCamera(_casterCamera, evt.floatParameter);
			break;
		}
	}
}
