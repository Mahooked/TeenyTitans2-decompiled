using System.Collections;

public class AbilityScript_Counterattack : AbilityScript
{
	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			ProcessAbility();
			yield return null;
		}
		else
		{
			_caster.Play(_abilityData.animName, isAbilityAnimation: true);
			ProcessAbility();
		}
	}
}
