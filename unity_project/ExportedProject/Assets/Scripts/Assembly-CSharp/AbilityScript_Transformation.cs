using System.Collections;
using System.IO;
using UnityEngine;

public class AbilityScript_Transformation : AbilityScript
{
	private BattleCharacter newBattleCharacter;

	public override IEnumerator Play()
	{
		CharacterTransformation characterTransformationById = _caster.characterDefinition.GetCharacterTransformationById(_abilityData.stringValue1);
		string empty = string.Empty;
		string path = ((_caster == BattleController.instance.playerCharacter) ? ((characterTransformationById != null) ? characterTransformationById.battleCharacterPath : _caster.characterDefinition.battleCharacterPath) : ((characterTransformationById != null) ? ((!string.IsNullOrEmpty(characterTransformationById.battleCharacterPathP2)) ? characterTransformationById.battleCharacterPathP2 : characterTransformationById.battleCharacterPath) : ((!string.IsNullOrEmpty(_caster.characterDefinition.battleCharacterPathP2)) ? _caster.characterDefinition.battleCharacterPathP2 : _caster.characterDefinition.battleCharacterPath)));
		newBattleCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(path));
		newBattleCharacter.transform.localScale = _caster.transform.localScale;
		newBattleCharacter.character = _caster.character;
		_caster.Play("transform out", true);
		_casterCamera.Play("transform out", true);
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
