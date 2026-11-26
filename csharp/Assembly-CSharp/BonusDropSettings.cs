using System;
using System.Collections.Generic;

[Serializable]
public class BonusDropSettings
{
	public int heartHealMin;

	public float heartHealPercent;

	public List<TofuSettings.SpicyTofuTier> blueFireTiers;

	public TofuSettings.SpicyTofuTier GetBlueFireTier(int level)
	{
		int num = blueFireTiers.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (level >= blueFireTiers[num2].minLevel)
			{
				return blueFireTiers[num2];
			}
		}
		return new TofuSettings.SpicyTofuTier(1);
	}
}
