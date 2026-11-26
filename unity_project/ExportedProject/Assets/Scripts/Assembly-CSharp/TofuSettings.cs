using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TofuSettings
{
	[Serializable]
	public struct SpicyTofuTier
	{
		public int minLevel;

		public int bonus;

		public SpicyTofuTier(int bonus)
		{
			minLevel = 1;
			this.bonus = bonus;
		}
	}

	[Serializable]
	public class TofuDropWeight
	{
		public TofuType tofuType = TofuType.Battery;

		[Range(0f, 100f)]
		public int weight = 100;

		public TofuDropWeight()
		{
		}

		public TofuDropWeight(TofuType tofuType, int weight)
		{
			this.tofuType = tofuType;
			this.weight = weight;
		}

		public TofuDropWeight(TofuDropWeight copy)
		{
			tofuType = copy.tofuType;
			weight = copy.weight;
		}
	}

	public int healMin;

	public float healPercent;

	public List<SpicyTofuTier> spicyTiers;

	public int numBatteriesToDrop;

	public int danceSpeedBonus;

	public float danceDuration;

	public float bubbleDuration;

	public string bubblePrefabPath;

	public int healOnBubbleEndAmtMin;

	public float healOnBubbleEndPercent;

	public float actionMeterFreezeBarReduction;

	public float waffleDuration;

	public int numOfWaffles;

	public List<TofuDropWeight> tofuDropWeights = new List<TofuDropWeight>();

	public SpicyTofuTier GetSpicyTofuTier(int level)
	{
		int num = spicyTiers.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (level >= spicyTiers[num2].minLevel)
			{
				return spicyTiers[num2];
			}
		}
		return new SpicyTofuTier(1);
	}
}
