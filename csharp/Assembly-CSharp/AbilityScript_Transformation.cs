using System.Collections;
using System.IO;
using UnityEngine;

public class AbilityScript_Transformation : AbilityScript
{
	private BattleCharacter newBattleCharacter;

	public override IEnumerator Play()
	{
		CharacterTransformation characterTransformationById = _caster.characterDefinition.GetCharacterTransformationById(_abilityData.stringValue1);
		_ = string.Empty;
		string path;
		if (_caster == BattleController.instance.playerCharacter)
		{
			path = ((characterTransformationById != null) ? characterTransformationById.battleCharacterPath : _caster.characterDefinition.battleCharacterPath);
		}
		else if (characterTransformationById != null)
		{
			path = ((!string.IsNullOrEmpty(characterTransformationById.battleCharacterPathP2)) ? characterTransformationById.battleCharacterPathP2 : characterTransformationById.battleCharacterPath);
		}
		else
		{
			path = ((!string.IsNullOrEmpty(_caster.characterDefinition.battleCharacterPathP2)) ? _caster.characterDefinition.battleCharacterPathP2 : _caster.characterDefinition.battleCharacterPath);
		}
		newBattleCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(path));
		newBattleCharacter.transform.localScale = _caster.transform.localScale;
		newBattleCharacter.character = _caster.character;
		_caster.Play("transform out", isAbilityAnimation: true);
		_casterCamera.Play("transform out", isAbilityAnimation: true);
		newBattleCharacter.Play("transform in");
		yield break;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		switch (evt.stringParameter)
		{
		case "transform":
			_caster.character.Transform(_abilityData.stringValue1);
			break;
		case "complete":
			break;
		}
	}

	public override void CleanUp()
	{
		base.CleanUp();
		BattleController.instance.SwapBattleCharacter(newBattleCharacter, _casterCamera.tag.Equals("Player"));
	}
}
