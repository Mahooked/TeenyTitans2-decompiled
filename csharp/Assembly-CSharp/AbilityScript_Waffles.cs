using System.Collections;

public class AbilityScript_Waffles : AbilityScript_Default
{
	public override IEnumerator Play()
	{
		string text = ((!string.IsNullOrEmpty(_abilityData.animName)) ? _abilityData.animName : "waffle");
		text += ((!(_caster == BattleController.instance.playerCharacter)) ? " p2" : " p1");
		_caster.Play(text, isAbilityAnimation: true);
		_casterCamera.Play(text, isAbilityAnimation: true);
		yield break;
	}
}
