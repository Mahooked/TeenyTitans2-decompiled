using System.Collections.Generic;
using UnityEngine;

public class LegionOfDoomLevelTable : ScriptableObject
{
	public List<LegionOfDoomLevelData> levelData = new List<LegionOfDoomLevelData>();

	public int MaxLevel
	{
		get
		{
			int num = 1;
			for (int i = 0; i < levelData.Count; i++)
			{
				if (levelData[i].levelRange.maxValue > num)
				{
					num = levelData[i].levelRange.maxValue;
				}
			}
			return num;
		}
	}

	public LegionOfDoomLevelData GetLevelData(int level)
	{
		return levelData.Find((LegionOfDoomLevelData lvl) => level >= lvl.levelRange.minValue && level <= lvl.levelRange.maxValue);
	}
}
