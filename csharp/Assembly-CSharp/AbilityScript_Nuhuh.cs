using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript_Nuhuh : AbilityScript
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
		string stringParameter = evt.stringParameter;
		if (stringParameter == null || !(stringParameter == "hit"))
		{
			return;
		}
		if (_casterMeter.GetStatusEffect<HealBuffBlocker>() != null)
		{
			BattleDisplayManager.instance.ShowMessage(Language.Get("BLOCKED", "ABILS,CHIPS,ACHVS,LDBS"), _casterMeter.character);
			return;
		}
		int count = _casterParty.Count;
		for (int i = 0; i < count; i++)
		{
			if (!_casterParty[i].isDead)
			{
				_casterParty[i].RemoveAllStatusEffectsByType(StatusEffectType.Debuff, isCleanse: true);
			}
		}
		_casterMeter.RemoveAllStatusEffectsByType(StatusEffectType.Debuff, isCleanse: true);
		List<Character> list = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.playerParty : BattleController.instance.enemyParty);
		ActionMeter actionMeter = ((!(_caster == BattleController.instance.playerCharacter)) ? BattleController.instance.playerActionMeter : BattleController.instance.enemyActionMeter);
		count = list.Count;
		for (int j = 0; j < count; j++)
		{
			Character character = list[j];
			if (character.isDead)
			{
				continue;
			}
			for (int k = 0; k < character.statusEffects.Count; k++)
			{
				StatusEffect statusEffect = character.statusEffects[k];
				if (statusEffect.type == StatusEffectType.Buff)
				{
					if (statusEffect is Flight)
					{
						((Flight)statusEffect).CleanseFlight();
					}
					else if (statusEffect.isCleansable)
					{
						statusEffect.Remove();
					}
				}
			}
		}
		actionMeter.RemoveAllStatusEffectsByType(StatusEffectType.Buff, isCleanse: true);
		Vector3 centerPosition = GetCenterPosition(_caster);
		centerPosition.z = -100f;
		ObjectPool.GetObjectFromResource("Prefabs/FX/Cleanse_Tofu_FX", centerPosition);
		Transform transform = ObjectPool.GetObjectFromResource("Prefabs/FX/Cleanse_Text").transform;
		transform.parent = BattleController.instance.guiCamera.transform;
		transform.localPosition = new Vector3(0f, 0f, 50f);
		ProcessAbility();
	}
}
