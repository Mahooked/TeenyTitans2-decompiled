using System;
using UnityEngine;

[Serializable]
public class AbilitySlot
{
	public int position;

	public int abilityId = -1;

	public int positionMultiplier
	{
		get
		{
			return Mathf.Clamp(1 + Mathf.FloorToInt((float)(position - 1) / (100f / (float)GameManager.instance.gameSettings.actionMeterMultiplierTiers)), 1, GameManager.instance.gameSettings.actionMeterMultiplierTiers);
		}
	}

	public AbilitySlot()
	{
	}

	public AbilitySlot(int position, int abilityId = -1)
	{
		this.position = position;
		this.abilityId = abilityId;
	}
}
