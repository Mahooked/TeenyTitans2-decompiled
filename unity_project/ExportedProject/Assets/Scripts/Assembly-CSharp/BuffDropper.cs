using System.Collections.Generic;
using UnityEngine;

public class BuffDropper : StatusEffectMinion
{
	private static readonly List<Vector3> dropperPositions = new List<Vector3>
	{
		new Vector3(-1.18f, 0f, 0.18f),
		new Vector3(0.66f, 0f, 0.04f)
	};

	private Beatbox beatbox;

	private BeatBoxType beatBoxType;

	private int amount;

	private float interval;

	private float intervalCounter;

	protected override List<Vector3> minionPositions
	{
		get
		{
			return dropperPositions;
		}
	}

	protected override Vector3 minionLocalPosition
	{
		get
		{
			return (!(beatbox != null)) ? Vector3.zero : beatbox.animator.transform.localPosition;
		}
	}

	public BuffDropper(Beatbox beatbox, ActionMeter targetMeter, BeatBoxType beatBoxType, int amount, float duration, float interval, Accessory casterAccessory = null)
		: base(targetMeter, duration, casterAccessory)
	{
		this.beatbox = beatbox;
		this.beatBoxType = beatBoxType;
		this.amount = amount;
		this.interval = interval;
	}

	protected override void InitMinionEffect()
	{
		intervalCounter = interval;
		beatbox.animator.transform.localPosition = targetMinionPosition;
	}

	protected override void UpdateMinionEffect()
	{
		if (!(deathTimer > 0f))
		{
			return;
		}
		intervalCounter -= Time.deltaTime;
		if (intervalCounter <= 0f)
		{
			bool flag = targetMeter == BattleController.instance.playerActionMeter;
			Camera camera = ((!flag) ? BattleController.instance.enemyBattleCamera.camera : BattleController.instance.playerBattleCamera.camera);
			Vector3 position = camera.WorldToScreenPoint(beatbox.spawner.position);
			Vector3 position2 = tk2dCamera.CameraForLayer(LayerMask.NameToLayer("UI")).ScreenCamera.ScreenToWorldPoint(position);
			switch ((BeatBoxType)((beatBoxType != BeatBoxType.Random) ? ((int)beatBoxType) : Random.Range(0, 2)))
			{
			case BeatBoxType.AttackUp:
			{
				FirePickup firePickup = BattleObjectManager.instance.SpawnPickupAt<FirePickup>("Prefabs/GUI/Pickups/FirePickup", position2, flag);
				break;
			}
			case BeatBoxType.Heal:
			{
				HeartPickup heartPickup = BattleObjectManager.instance.SpawnPickupAt<HeartPickup>("Prefabs/GUI/Pickups/HeartPickup", position2, flag);
				heartPickup.healPercent = (float)amount / 100f;
				break;
			}
			}
			intervalCounter += interval;
		}
	}

	protected override void RemoveMinionEffect()
	{
		beatbox.PlayOutro();
	}

	protected override void ResetEffect()
	{
	}

	protected override void CleanUp()
	{
	}
}
