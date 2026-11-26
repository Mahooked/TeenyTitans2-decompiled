using System;
using UnityEngine;

[Serializable]
public struct StatWeights
{
	[Range(0f, 100f)]
	public int healthWeight;

	[Range(0f, 100f)]
	public int attackWeight;

	[Range(0f, 100f)]
	public int speedWeight;

	[Range(0f, 100f)]
	public int luckWeight;

	[Range(0f, 100f)]
	public int dodgeWeight;

	public StatWeights(int healthWeight, int attackWeight, int speedWeight, int luckWeight, int dodgeWeight)
	{
		this.healthWeight = healthWeight;
		this.attackWeight = attackWeight;
		this.speedWeight = speedWeight;
		this.luckWeight = luckWeight;
		this.dodgeWeight = dodgeWeight;
	}

	public int GetWeight(CharacterStatType characterStatType)
	{
		switch (characterStatType)
		{
		case CharacterStatType.HitPoints:
			return healthWeight;
		case CharacterStatType.Attack:
			return attackWeight;
		case CharacterStatType.Speed:
			return speedWeight;
		case CharacterStatType.Luck:
			return luckWeight;
		case CharacterStatType.Dodge:
			return dodgeWeight;
		default:
			return 0;
		}
	}
}
