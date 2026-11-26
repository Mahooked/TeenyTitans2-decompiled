using UnityEngine;

public class BatteryPickup : BattlePickup
{
	public int attackAmount = 2;

	private BonusDropSettings bonusDropSettings;

	public override BattlePickupType type
	{
		get
		{
			return BattlePickupType.Battery;
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
			return BattleController.instance.playerAccessoryButton.batteryTargetPos.position;
		}
		return BattleController.instance.enemyAccessoryButton.batteryTargetPos.position;
	}

	protected override void Collected()
	{
		float maxHealthPercent = GameManager.instance.gameSettings.batteryPickupChargeUpPercent;
		if (_isPlayerPickup)
		{
			if (BattleController.instance.playerAccessoryButton.isActivated)
			{
				maxHealthPercent = GameManager.instance.gameSettings.batteryPickupChargeUpPercentActiveAccessory;
			}
			BattleController.instance.playerAccessoryButton.AddChargeHealthPercentage(maxHealthPercent, true, true);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_battery_collect", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			if (BattleController.instance.enemyAccessoryButton.isActivated)
			{
				maxHealthPercent = GameManager.instance.gameSettings.batteryPickupChargeUpPercentActiveAccessory;
			}
			BattleController.instance.enemyAccessoryButton.AddChargeHealthPercentage(maxHealthPercent, true, true);
		}
	}
}
