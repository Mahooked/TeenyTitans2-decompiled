using System;
using UnityEngine;

[Serializable]
public struct StatWeights(int healthWeight, int attackWeight, int speedWeight, int luckWeight, int dodgeWeight)
{
	[Range(0f, 100f)]
	public int healthWeight = healthWeight;

	[Range(0f, 100f)]
	public int attackWeight = attackWeight;

	[Range(0f, 100f)]
	public int speedWeight = speedWeight;

	[Range(0f, 100f)]
	public int luckWeight = luckWeight;

	[Range(0f, 100f)]
	public int dodgeWeight = dodgeWeight;

	public int GetWeight(CharacterStatType characterStatType)
	{
		return characterStatType switch
		{
			CharacterStatType.HitPoints => healthWeight, 
			CharacterStatType.Attack => attackWeight, 
			CharacterStatType.Speed => speedWeight, 
			CharacterStatType.Luck => luckWeight, 
			CharacterStatType.Dodge => dodgeWeight, 
			_ => 0, 
		};
	}
}
