using UnityEngine;

public class HeartPickup : BattlePickup
{
	public float healPercent = 0.1f;

	private BonusDropSettings bonusDropSettings;

	public override BattlePickupType type
	{
		get
		{
			return BattlePickupType.Heart;
		}
	}

	private void Start()
	{
		bonusDropSettings = GameManager.instance.gameSettings.bonusDropSettings;
	}

	protected override Vector3 GetDestination()
	{
		if (_isPlayerPickup)
		{
			return BattleController.instance.playerPortraits.uiItem.transform.position;
		}
		return BattleController.instance.enemyPortraits.uiItem.transform.position;
	}

	protected override void Collected()
	{
		ActionMeter actionMeter = ((!_isPlayerPickup) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
		if (actionMeter.character != null && actionMeter.character.character != null && !actionMeter.character.character.isDead)
		{
			int amount = Mathf.Max(bonusDropSettings.heartHealMin, Mathf.CeilToInt((float)actionMeter.character.character.maxHitPoints * healPercent));
			actionMeter.character.character.DoHeal(new HealResult(null, actionMeter.character.character, amount));
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_use_heal", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}
}
