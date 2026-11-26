using System.Collections;

public class AbilityScript_Charge : AbilityScript
{
	public override IEnumerator Play()
	{
		_caster.Play(_abilityData.animName + " charge", isAbilityAnimation: true);
		_casterCamera.Play(_abilityData.animName + " charge", isAbilityAnimation: true);
		yield break;
	}
}
