using System.Collections;
using UnityEngine;

public class TofuEffect : MonoBehaviour
{
	private BattleCharacter targetCharacter;

	private TofuType type;

	private TofuBubble tofuBubble;

	private TofuSettings tofuSettings;

	public void StartEffectForTarget(TofuType tofuType, BattleCharacter target, TofuBubble bubble, int seed = -1)
	{
		type = tofuType;
		targetCharacter = target;
		tofuBubble = bubble;
		tofuSettings = GameManager.instance.gameSettings.tofuSettings;
		StartCoroutine(StartEffect(seed));
	}

	private IEnumerator StartEffect(int seed = -1)
	{
		yield return new WaitForEndOfFrame();
		Random.State prevRandomState = Random.state;
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network && seed != -1)
		{
			Random.InitState(seed);
		}
		BattleController battleController = BattleController.instance;
		ActionMeter targetMeter = null;
		string useSoundPath = string.Empty;
		switch (type)
		{
		case TofuType.Tasty:
			targetCharacter.character.DoHeal(new HealResult(null, targetCharacter.character, Mathf.Max(tofuSettings.healMin, Mathf.FloorToInt((float)targetCharacter.character.maxHitPoints * tofuSettings.healPercent))));
			useSoundPath = "Audio/SFX/sfx_battle_tofu_use_heal";
			break;
		case TofuType.Spicy:
			targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.enemyActionMeter : battleController.playerActionMeter);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new TeamStatBuff(targetMeter, CharacterStatType.Attack, tofuSettings.GetSpicyTofuTier(BattleController.instance.averageCharacterLevelInBattle).bonus));
			useSoundPath = "Audio/SFX/sfx_battle_tofu_use_spicy";
			break;
		case TofuType.Speedy:
			((!(targetCharacter == battleController.playerCharacter)) ? battleController.enemyActionMeter : battleController.playerActionMeter).progress += 0.3f;
			useSoundPath = "Audio/SFX/sfx_battle_tofu_use_speedy";
			break;
		case TofuType.Sleepy:
			targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.enemyActionMeter : battleController.playerActionMeter);
			if (Random.value >= targetMeter.character.character.stunResistanceChance)
			{
				targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.playerActionMeter : battleController.enemyActionMeter);
				PortraitSwitcher portraits = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.playerPortraits : battleController.enemyPortraits);
				StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new ActionMeterDisabler(targetMeter, portraits, 3f));
				useSoundPath = "Audio/SFX/sfx_battle_tofu_use_sleepy";
			}
			else
			{
				BattleDisplayManager.instance.ShowMessage(Language.Get("RESIST", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
			}
			break;
		case TofuType.Cleanse:
		{
			targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.enemyActionMeter : battleController.playerActionMeter);
			targetMeter.RemoveAllStatusEffectsByType(StatusEffectType.Debuff, isCleanse: true);
			targetCharacter.character.RemoveAllStatusEffectsByType(StatusEffectType.Debuff, isCleanse: true);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new Immunity(targetMeter, 3f));
			if (targetCharacter == battleController.playerCharacter)
			{
				battleController.enemyActionMeter.RemoveAllStatusEffectsByType(StatusEffectType.Buff, isCleanse: true);
			}
			else
			{
				battleController.playerActionMeter.RemoveAllStatusEffectsByType(StatusEffectType.Buff, isCleanse: true);
			}
			BattleCharacter battleCharacter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.playerCharacter : battleController.enemyCharacter);
			for (int k = 0; k < battleCharacter.character.statusEffects.Count; k++)
			{
				StatusEffect statusEffect = battleCharacter.character.statusEffects[k];
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
			if (tofuBubble != null)
			{
				Vector3 position2 = battleController.playerBattleCamera.camera.WorldToScreenPoint(tofuBubble.tofuSprite.transform.position);
				Vector3 destination = battleController.guiCamera.ScreenToWorldPoint(position2);
				destination.z = 0f;
				ObjectPool.GetObjectFromResource("Prefabs/FX/Cleanse_Tofu_FX", destination);
				Transform transform = ObjectPool.GetObjectFromResource("Prefabs/FX/Cleanse_Text").transform;
				transform.parent = battleController.guiCamera.transform;
				transform.localPosition = new Vector3(0f, 0f, 50f);
			}
			useSoundPath = "Audio/SFX/sfx_tofu_cleanse";
			break;
		}
		case TofuType.Battery:
		{
			bool flag = targetCharacter == battleController.playerCharacter;
			Camera camera = ((!flag) ? battleController.enemyBattleCamera.camera : battleController.playerBattleCamera.camera);
			Vector3 position = camera.WorldToScreenPoint((!(tofuBubble != null)) ? targetCharacter.center.position : tofuBubble.transform.position);
			Vector3 vector = battleController.guiCamera.ScreenToWorldPoint(position);
			vector.z = -800f;
			int numBatteriesToDrop = GameManager.instance.gameSettings.tofuSettings.numBatteriesToDrop;
			if (numBatteriesToDrop > 0)
			{
				float num = 60f;
				Vector3 vector2 = vector;
				vector2.x -= (float)(numBatteriesToDrop - 1) / 2f * num;
				for (int j = 0; j < numBatteriesToDrop; j++)
				{
					BattleObjectManager.instance.SpawnBatteryAt(vector2 + new Vector3(num * (float)j, Random.Range(-50, 50)), flag);
				}
			}
			break;
		}
		case TofuType.Dance:
		{
			targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.enemyActionMeter : battleController.playerActionMeter);
			int danceSpeedBonus = GameManager.instance.gameSettings.tofuSettings.danceSpeedBonus;
			float danceDuration = GameManager.instance.gameSettings.tofuSettings.danceDuration;
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new ActionMeterSpeedModifier(targetMeter, danceSpeedBonus, danceDuration));
			break;
		}
		case TofuType.Bubble:
		{
			targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.enemyActionMeter : battleController.playerActionMeter);
			string bubblePrefabPath = GameManager.instance.gameSettings.tofuSettings.bubblePrefabPath;
			float bubbleDuration = GameManager.instance.gameSettings.tofuSettings.bubbleDuration;
			int healOnEnd = Mathf.Max(GameManager.instance.gameSettings.tofuSettings.healOnBubbleEndAmtMin, Mathf.FloorToInt((float)targetCharacter.character.maxHitPoints * tofuSettings.healPercent));
			StatusEffectManager.instance.AddCharacterStatusEffect(targetCharacter.character, new ProtectionBubble(bubblePrefabPath, targetMeter, bubbleDuration, 0f, healOnEnd));
			useSoundPath = "Audio/SFX/sfx_battle_tofu_bubble";
			break;
		}
		case TofuType.Freeze:
		{
			targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.playerActionMeter : battleController.enemyActionMeter);
			float actionMeterFreezeBarReduction = GameManager.instance.gameSettings.tofuSettings.actionMeterFreezeBarReduction;
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new ActionMeterFreeze(targetMeter, 0f, actionMeterFreezeBarReduction));
			useSoundPath = "Audio/SFX/sfx_battle_tofu_freezebar";
			break;
		}
		case TofuType.Waffle:
		{
			targetMeter = ((!(targetCharacter == battleController.playerCharacter)) ? battleController.playerActionMeter : battleController.enemyActionMeter);
			float waffleDuration = GameManager.instance.gameSettings.tofuSettings.waffleDuration;
			int numOfWaffles = GameManager.instance.gameSettings.tofuSettings.numOfWaffles;
			for (int i = 0; i < numOfWaffles; i++)
			{
				StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new AbilityBlocker(targetMeter, waffleDuration));
			}
			useSoundPath = "Audio/SFX/sfx_battle_tofu_waffle";
			break;
		}
		}
		if (!string.IsNullOrEmpty(useSoundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(useSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network && seed != -1)
		{
			Random.state = prevRandomState;
		}
		Object.Destroy(gameObject);
	}
}
