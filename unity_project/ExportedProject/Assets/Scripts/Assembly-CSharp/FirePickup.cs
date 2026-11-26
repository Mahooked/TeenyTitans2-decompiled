using UnityEngine;

public class FirePickup : BattlePickup
{
	public int attackUpAmount = -1;

	private BonusDropSettings bonusDropSettings;

	public override BattlePickupType type
	{
		get
		{
			return BattlePickupType.Fire;
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
			return BattleController.instance.playerActionMeter.backSprite.transform.position + new Vector3(BattleController.instance.playerActionMeter.backSprite.dimensions.x * 0.5f, 0f);
		}
		return BattleController.instance.enemyActionMeter.backSprite.transform.position + new Vector3(BattleController.instance.enemyActionMeter.backSprite.dimensions.x * 0.75f * -0.5f, 0f);
	}

	protected override void Collected()
	{
		ActionMeter actionMeter = ((!_isPlayerPickup) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
		if (BattleController.instance.isBattleRunning)
		{
			int statBonus = ((attackUpAmount <= -1) ? bonusDropSettings.GetBlueFireTier(BattleController.instance.averageCharacterLevelInBattle).bonus : attackUpAmount);
			StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new TeamStatBuff(actionMeter, CharacterStatType.Attack, statBonus));
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_use_spicy", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}
}
