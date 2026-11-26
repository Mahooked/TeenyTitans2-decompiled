using System.Collections.Generic;
using UnityEngine;

public static class CharacterRoller
{
	private static int maxChallengeModeHealth = 80;

	public static CharacterData RollCharacterData(CharacterDefinition charDef, int uid, int characterUID, int level)
	{
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = -1;
		CharacterData characterData = new CharacterData();
		characterData.uid = uid;
		characterData.characterUID = characterUID;
		characterData.level = level;
		characterData.hitPoints = charDef.baseHitPoints;
		characterData.attack = charDef.baseAttack;
		characterData.speed = charDef.baseSpeed;
		characterData.luck = charDef.baseLuck;
		characterData.dodge = charDef.baseDodge;
		characterData.selectedRewards.Clear();
		characterData.abilityUIDs.Clear();
		LevelData levelData = null;
		List<LevelUpReward> list = new List<LevelUpReward>();
		while (num < characterData.level)
		{
			levelData = charDef.levelTable.GetDataForLevel(num + 1);
			int count = levelData.rewards.Count;
			for (int i = 0; i < count; i++)
			{
				if (levelData.rewards[i].isForced)
				{
					list.Add(levelData.rewards[i]);
				}
			}
			if (list.Count == 0 && levelData.fillWithStats)
			{
				List<int> list2 = new List<int>();
				list2.Add(0);
				list2.Add(1);
				list2.Add(3);
				list2.Add(4);
				List<int> list3 = list2;
				LevelUpReward levelUpReward = null;
				int num8 = characterData.selectedRewards.Count - 1;
				for (int num9 = num8; num9 >= 0; num9--)
				{
					levelUpReward = charDef.levelTable.GetRewardById(characterData.selectedRewards[num9]);
					if (levelUpReward.type == LevelUpRewardType.StatUpgrade)
					{
						list3.Remove((int)levelUpReward.statType);
						break;
					}
				}
				while (list.Count < 3 && list3.Count > 0)
				{
					int index = Random.Range(0, list3.Count);
					CharacterStatType characterStatType = (CharacterStatType)list3[index];
					list3.RemoveAt(index);
					if (characterStatType == (CharacterStatType)num7)
					{
						continue;
					}
					switch (characterStatType)
					{
					case CharacterStatType.HitPoints:
						if (num2 < charDef.levelTable.hitPointsUpgradeRewards.Count && characterData.hitPoints < charDef.maxHitPoints - charDef.baseHitPointsOffset)
						{
							list.Add(charDef.levelTable.hitPointsUpgradeRewards[num2]);
						}
						break;
					case CharacterStatType.Attack:
						if (num3 < charDef.levelTable.attackUpgradeRewards.Count && characterData.attack < charDef.maxAttack - charDef.baseAttackOffset)
						{
							list.Add(charDef.levelTable.attackUpgradeRewards[num3]);
						}
						break;
					case CharacterStatType.Speed:
						if (num4 < charDef.levelTable.speedUpgradeRewards.Count && characterData.speed < charDef.maxSpeed - charDef.baseSpeedOffset)
						{
							list.Add(charDef.levelTable.speedUpgradeRewards[num4]);
						}
						break;
					case CharacterStatType.Luck:
						if (num5 < charDef.levelTable.luckUpgradeRewards.Count && characterData.luck < charDef.maxLuck - charDef.baseLuckOffset)
						{
							list.Add(charDef.levelTable.luckUpgradeRewards[num5]);
						}
						break;
					case CharacterStatType.Dodge:
						if (num6 < charDef.levelTable.dodgeUpgradeRewards.Count && characterData.dodge < charDef.maxDodge - charDef.baseDodgeOffset)
						{
							list.Add(charDef.levelTable.dodgeUpgradeRewards[num6]);
						}
						break;
					}
				}
			}
			if (list.Count > 0)
			{
				LevelUpReward levelUpReward2 = list[Random.Range(0, list.Count)];
				characterData.selectedRewards.Add(levelUpReward2.uid);
				if (levelUpReward2.type != LevelUpRewardType.AbilityUnlock)
				{
					if (levelUpReward2.type == LevelUpRewardType.StatUpgrade)
					{
						switch (levelUpReward2.statType)
						{
						case CharacterStatType.HitPoints:
							characterData.hitPoints += Mathf.Min(levelUpReward2.statValueInt, charDef.maxHitPoints - charDef.baseHitPointsOffset - characterData.hitPoints);
							num2++;
							break;
						case CharacterStatType.Attack:
							characterData.attack += Mathf.Min(levelUpReward2.statValueInt, charDef.maxAttack - charDef.baseAttackOffset - characterData.attack);
							num3++;
							break;
						case CharacterStatType.Speed:
							characterData.speed += Mathf.Min(levelUpReward2.statValueInt, charDef.maxSpeed - charDef.baseSpeedOffset - characterData.speed);
							num4++;
							break;
						case CharacterStatType.Luck:
							characterData.luck += Mathf.Min(levelUpReward2.statValueInt, charDef.maxLuck - charDef.baseLuckOffset - characterData.luck);
							num5++;
							break;
						case CharacterStatType.Dodge:
							characterData.dodge += Mathf.Min(levelUpReward2.statValueInt, charDef.maxDodge - charDef.baseDodgeOffset - characterData.dodge);
							num6++;
							break;
						}
						num7 = (int)levelUpReward2.statType;
					}
					else if (levelUpReward2.type != LevelUpRewardType.ModChipUnlock)
					{
					}
				}
			}
			else
			{
				Debug.Log("No rewards add for Level " + (num + 1));
			}
			num++;
			list.Clear();
		}
		characterData.hitPointsTier = num2;
		characterData.attackTier = num3;
		characterData.speedTier = num4;
		characterData.luckTier = num5;
		characterData.dodgeTier = num6;
		characterData.lastRewardLevel = characterData.level;
		return characterData;
	}

	public static CharacterData RollCharacterDataChallengeMode(CharacterDefinition charDef, int uid, int characterUID, int level, bool hardMode = false)
	{
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = -1;
		CharacterData characterData = new CharacterData();
		characterData.uid = uid;
		characterData.characterUID = characterUID;
		characterData.level = level;
		characterData.hitPoints = charDef.baseHitPoints;
		characterData.attack = charDef.baseAttack;
		characterData.speed = charDef.baseSpeed;
		characterData.luck = charDef.baseLuck;
		characterData.dodge = charDef.baseDodge;
		characterData.selectedRewards.Clear();
		characterData.abilityUIDs.Clear();
		LevelData levelData = null;
		List<LevelUpReward> list = new List<LevelUpReward>();
		while (num < characterData.level)
		{
			levelData = charDef.levelTable.GetDataForLevel(num + 1);
			int count = levelData.rewards.Count;
			for (int i = 0; i < count; i++)
			{
				if (levelData.rewards[i].isForced)
				{
					list.Add(levelData.rewards[i]);
				}
			}
			if (list.Count == 0 && levelData.fillWithStats)
			{
				List<int> list2 = new List<int>();
				list2.Add(0);
				list2.Add(1);
				list2.Add(3);
				list2.Add(4);
				List<int> list3 = list2;
				list2 = new List<int>();
				list2.Add(30);
				list2.Add(30);
				list2.Add(20);
				list2.Add(20);
				List<int> list4 = list2;
				LevelUpReward levelUpReward = null;
				int num8 = characterData.selectedRewards.Count - 1;
				for (int num9 = num8; num9 >= 0; num9--)
				{
					levelUpReward = charDef.levelTable.GetRewardById(characterData.selectedRewards[num9]);
					if (levelUpReward.type == LevelUpRewardType.StatUpgrade)
					{
						int index = list3.IndexOf((int)levelUpReward.statType);
						list3.RemoveAt(index);
						list4.RemoveAt(index);
						break;
					}
				}
				while (list.Count < 3 && list3.Count > 0)
				{
					int index2 = BiasedRandom(list4.ToArray());
					CharacterStatType characterStatType = (CharacterStatType)list3[index2];
					list3.RemoveAt(index2);
					list4.RemoveAt(index2);
					if (characterStatType == (CharacterStatType)num7)
					{
						continue;
					}
					switch (characterStatType)
					{
					case CharacterStatType.HitPoints:
						if (num2 < charDef.levelTable.hitPointsUpgradeRewards.Count && characterData.hitPoints < ((!hardMode) ? charDef.maxHitPoints : maxChallengeModeHealth) - charDef.baseHitPointsOffset)
						{
							list.Add(charDef.levelTable.hitPointsUpgradeRewards[num2]);
						}
						break;
					case CharacterStatType.Attack:
						if (num3 < charDef.levelTable.attackUpgradeRewards.Count && characterData.attack < charDef.maxAttack - charDef.baseAttackOffset)
						{
							list.Add(charDef.levelTable.attackUpgradeRewards[num3]);
						}
						break;
					case CharacterStatType.Speed:
						if (num4 < charDef.levelTable.speedUpgradeRewards.Count && characterData.speed < charDef.maxSpeed - charDef.baseSpeedOffset)
						{
							list.Add(charDef.levelTable.speedUpgradeRewards[num4]);
						}
						break;
					case CharacterStatType.Luck:
						if (num5 < charDef.levelTable.luckUpgradeRewards.Count && characterData.luck < charDef.maxLuck - charDef.baseLuckOffset)
						{
							list.Add(charDef.levelTable.luckUpgradeRewards[num5]);
						}
						break;
					case CharacterStatType.Dodge:
						if (num6 < charDef.levelTable.dodgeUpgradeRewards.Count && characterData.dodge < charDef.maxDodge - charDef.baseDodgeOffset)
						{
							list.Add(charDef.levelTable.dodgeUpgradeRewards[num6]);
						}
						break;
					}
				}
			}
			if (list.Count > 0)
			{
				LevelUpReward levelUpReward2 = list[Random.Range(0, list.Count)];
				characterData.selectedRewards.Add(levelUpReward2.uid);
				if (levelUpReward2.type != LevelUpRewardType.AbilityUnlock)
				{
					if (levelUpReward2.type == LevelUpRewardType.StatUpgrade)
					{
						switch (levelUpReward2.statType)
						{
						case CharacterStatType.HitPoints:
							characterData.hitPoints += Mathf.Min(levelUpReward2.statValueInt, charDef.maxHitPoints - charDef.baseHitPointsOffset - characterData.hitPoints);
							num2++;
							break;
						case CharacterStatType.Attack:
							characterData.attack += Mathf.Min(levelUpReward2.statValueInt, charDef.maxAttack - charDef.baseAttackOffset - characterData.attack);
							num3++;
							break;
						case CharacterStatType.Speed:
							characterData.speed += Mathf.Min(levelUpReward2.statValueInt, charDef.maxSpeed - charDef.baseSpeedOffset - characterData.speed);
							num4++;
							break;
						case CharacterStatType.Luck:
							characterData.luck += Mathf.Min(levelUpReward2.statValueInt, charDef.maxLuck - charDef.baseLuckOffset - characterData.luck);
							num5++;
							break;
						case CharacterStatType.Dodge:
							characterData.dodge += Mathf.Min(levelUpReward2.statValueInt, charDef.maxDodge - charDef.baseDodgeOffset - characterData.dodge);
							num6++;
							break;
						}
						num7 = (int)levelUpReward2.statType;
					}
					else if (levelUpReward2.type != LevelUpRewardType.ModChipUnlock)
					{
					}
				}
			}
			else
			{
				Debug.Log("No rewards add for Level " + (num + 1));
			}
			num++;
			list.Clear();
		}
		characterData.hitPointsTier = num2;
		characterData.attackTier = num3;
		characterData.speedTier = num4;
		characterData.luckTier = num5;
		characterData.dodgeTier = num6;
		characterData.lastRewardLevel = characterData.level;
		return characterData;
	}

	public static void RollTournamentFigure(TournamentRoundFigureData figureData)
	{
		CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(figureData.characterUID);
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = -1;
		figureData.selectedRewards.Clear();
		figureData.abilityUIDs.Clear();
		figureData.hitPoints = character.baseHitPoints;
		figureData.attack = character.baseAttack;
		figureData.speed = character.baseSpeed;
		figureData.luck = character.baseLuck;
		figureData.dodge = character.baseDodge;
		LevelData levelData = null;
		List<LevelUpReward> list = new List<LevelUpReward>();
		while (num < figureData.level)
		{
			levelData = character.levelTable.GetDataForLevel(num + 1);
			int count = levelData.rewards.Count;
			for (int i = 0; i < count; i++)
			{
				if (levelData.rewards[i].isForced)
				{
					list.Add(levelData.rewards[i]);
				}
			}
			if (list.Count == 0 && levelData.fillWithStats)
			{
				List<int> list2 = new List<int>();
				list2.Add(0);
				list2.Add(1);
				list2.Add(3);
				list2.Add(4);
				List<int> list3 = list2;
				while (list.Count < 3 && list3.Count > 0)
				{
					int index = Random.Range(0, list3.Count);
					CharacterStatType characterStatType = (CharacterStatType)list3[index];
					list3.RemoveAt(index);
					if (characterStatType == (CharacterStatType)num7)
					{
						continue;
					}
					switch (characterStatType)
					{
					case CharacterStatType.HitPoints:
						if (num2 < character.levelTable.hitPointsUpgradeRewards.Count)
						{
							list.Add(character.levelTable.hitPointsUpgradeRewards[num2]);
						}
						break;
					case CharacterStatType.Attack:
						if (num3 < character.levelTable.attackUpgradeRewards.Count)
						{
							list.Add(character.levelTable.attackUpgradeRewards[num3]);
						}
						break;
					case CharacterStatType.Speed:
						if (num4 < character.levelTable.speedUpgradeRewards.Count)
						{
							list.Add(character.levelTable.speedUpgradeRewards[num4]);
						}
						break;
					case CharacterStatType.Luck:
						if (num5 < character.levelTable.luckUpgradeRewards.Count)
						{
							list.Add(character.levelTable.luckUpgradeRewards[num5]);
						}
						break;
					case CharacterStatType.Dodge:
						if (num6 < character.levelTable.dodgeUpgradeRewards.Count)
						{
							list.Add(character.levelTable.dodgeUpgradeRewards[num6]);
						}
						break;
					}
				}
			}
			if (list.Count > 0)
			{
				LevelUpReward levelUpReward = list[Random.Range(0, list.Count)];
				figureData.selectedRewards.Add(levelUpReward.uid);
				if (levelUpReward.type != LevelUpRewardType.AbilityUnlock)
				{
					if (levelUpReward.type == LevelUpRewardType.StatUpgrade)
					{
						switch (levelUpReward.statType)
						{
						case CharacterStatType.HitPoints:
							figureData.hitPoints += levelUpReward.statValueInt;
							num2++;
							break;
						case CharacterStatType.Attack:
							figureData.attack += levelUpReward.statValueInt;
							num3++;
							break;
						case CharacterStatType.Speed:
							figureData.speed += levelUpReward.statValueInt;
							num4++;
							break;
						case CharacterStatType.Luck:
							figureData.luck += levelUpReward.statValueInt;
							num5++;
							break;
						case CharacterStatType.Dodge:
							figureData.dodge += levelUpReward.statValueInt;
							num6++;
							break;
						}
						num7 = (int)levelUpReward.statType;
					}
					else if (levelUpReward.type != LevelUpRewardType.ModChipUnlock)
					{
					}
				}
			}
			else
			{
				Debug.Log("No rewards add for Level " + (num + 1));
			}
			num++;
			list.Clear();
		}
	}

	public static CharacterData RollCharacterDataWeighted(CharacterDefinition charDef, int uid, int characterUID, int level, StatWeights statWeights, int overLevel)
	{
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = -1;
		CharacterData characterData = new CharacterData();
		characterData.uid = uid;
		characterData.characterUID = characterUID;
		characterData.level = level;
		characterData.hitPoints = charDef.baseHitPoints;
		characterData.attack = charDef.baseAttack;
		characterData.speed = charDef.baseSpeed;
		characterData.luck = charDef.baseLuck;
		characterData.dodge = charDef.baseDodge;
		characterData.selectedRewards.Clear();
		characterData.abilityUIDs.Clear();
		LevelData levelData = null;
		List<LevelUpReward> list = new List<LevelUpReward>();
		int num8 = ((characterData.level < charDef.levelTable.maxLevel) ? characterData.level : (characterData.level + overLevel));
		while (num < num8)
		{
			int level2 = Mathf.Clamp(num + 1, 1, charDef.levelTable.maxLevel);
			levelData = charDef.levelTable.GetDataForLevel(level2);
			int count = levelData.rewards.Count;
			for (int i = 0; i < count; i++)
			{
				if (levelData.rewards[i].isForced)
				{
					list.Add(levelData.rewards[i]);
				}
			}
			if (list.Count == 0 && levelData.fillWithStats)
			{
				List<int> list2 = new List<int>();
				list2.Add(0);
				list2.Add(1);
				list2.Add(3);
				list2.Add(4);
				List<int> list3 = list2;
				LevelUpReward levelUpReward = null;
				int num9 = characterData.selectedRewards.Count - 1;
				for (int num10 = num9; num10 >= 0; num10--)
				{
					levelUpReward = charDef.levelTable.GetRewardById(characterData.selectedRewards[num10]);
					if (levelUpReward.type == LevelUpRewardType.StatUpgrade)
					{
						list3.Remove((int)levelUpReward.statType);
						break;
					}
				}
				while (list.Count < 3 && list3.Count > 0)
				{
					int index = Random.Range(0, list3.Count);
					CharacterStatType characterStatType = (CharacterStatType)list3[index];
					list3.RemoveAt(index);
					if (characterStatType == (CharacterStatType)num7)
					{
						continue;
					}
					switch (characterStatType)
					{
					case CharacterStatType.HitPoints:
						if (num2 < charDef.levelTable.hitPointsUpgradeRewards.Count && characterData.hitPoints < charDef.maxHitPoints - charDef.baseHitPointsOffset)
						{
							list.Add(charDef.levelTable.hitPointsUpgradeRewards[num2]);
						}
						break;
					case CharacterStatType.Attack:
						if (num3 < charDef.levelTable.attackUpgradeRewards.Count && characterData.attack < charDef.maxAttack - charDef.baseAttackOffset)
						{
							list.Add(charDef.levelTable.attackUpgradeRewards[num3]);
						}
						break;
					case CharacterStatType.Speed:
						if (num4 < charDef.levelTable.speedUpgradeRewards.Count && characterData.speed < charDef.maxSpeed - charDef.baseSpeedOffset)
						{
							list.Add(charDef.levelTable.speedUpgradeRewards[num4]);
						}
						break;
					case CharacterStatType.Luck:
						if (num5 < charDef.levelTable.luckUpgradeRewards.Count && characterData.luck < charDef.maxLuck - charDef.baseLuckOffset)
						{
							list.Add(charDef.levelTable.luckUpgradeRewards[num5]);
						}
						break;
					case CharacterStatType.Dodge:
						if (num6 < charDef.levelTable.dodgeUpgradeRewards.Count && characterData.dodge < charDef.maxDodge - charDef.baseDodgeOffset)
						{
							list.Add(charDef.levelTable.dodgeUpgradeRewards[num6]);
						}
						break;
					}
				}
			}
			if (list.Count > 0)
			{
				LevelUpReward levelUpReward2 = null;
				if (levelData.fillWithStats && list.Find((LevelUpReward x) => x.type != LevelUpRewardType.StatUpgrade) == null)
				{
					int num11 = 0;
					for (int num12 = 0; num12 < list.Count; num12++)
					{
						num11 += statWeights.GetWeight(list[num12].statType);
					}
					int num13 = Random.Range(0, num11);
					for (int num14 = 0; num14 < list.Count; num14++)
					{
						int weight = statWeights.GetWeight(list[num14].statType);
						if (num13 < weight)
						{
							levelUpReward2 = list[num14];
							break;
						}
						num13 -= weight;
					}
				}
				else
				{
					levelUpReward2 = list[Random.Range(0, list.Count)];
				}
				characterData.selectedRewards.Add(levelUpReward2.uid);
				if (levelUpReward2.type == LevelUpRewardType.StatUpgrade)
				{
					switch (levelUpReward2.statType)
					{
					case CharacterStatType.HitPoints:
						characterData.hitPoints += Mathf.Min(levelUpReward2.statValueInt, charDef.maxHitPoints - charDef.baseHitPointsOffset - characterData.hitPoints);
						num2++;
						break;
					case CharacterStatType.Attack:
						characterData.attack += Mathf.Min(levelUpReward2.statValueInt, charDef.maxAttack - charDef.baseAttackOffset - characterData.attack);
						num3++;
						break;
					case CharacterStatType.Speed:
						characterData.speed += Mathf.Min(levelUpReward2.statValueInt, charDef.maxSpeed - charDef.baseSpeedOffset - characterData.speed);
						num4++;
						break;
					case CharacterStatType.Luck:
						characterData.luck += Mathf.Min(levelUpReward2.statValueInt, charDef.maxLuck - charDef.baseLuckOffset - characterData.luck);
						num5++;
						break;
					case CharacterStatType.Dodge:
						characterData.dodge += Mathf.Min(levelUpReward2.statValueInt, charDef.maxDodge - charDef.baseDodgeOffset - characterData.dodge);
						num6++;
						break;
					}
					num7 = (int)levelUpReward2.statType;
				}
				else if (levelUpReward2.type != LevelUpRewardType.ModChipUnlock)
				{
				}
			}
			else
			{
				Debug.Log("No rewards add for Level " + (num + 1));
			}
			num++;
			list.Clear();
		}
		characterData.hitPointsTier = num2;
		characterData.attackTier = num3;
		characterData.speedTier = num4;
		characterData.luckTier = num5;
		characterData.dodgeTier = num6;
		characterData.lastRewardLevel = characterData.level;
		return characterData;
	}

	public static int BiasedRandom(int[] chances)
	{
		int max = Sum(chances);
		int num = RandomRange(0, max);
		for (int i = 0; i < chances.Length; i++)
		{
			if (num < chances[i])
			{
				return i;
			}
			num -= chances[i];
		}
		return chances.Length - 1;
	}

	public static int Sum(params int[] values)
	{
		if (values.Length == 0)
		{
			return 0;
		}
		int num = 0;
		foreach (int num2 in values)
		{
			num += num2;
		}
		return num;
	}

	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}
}
