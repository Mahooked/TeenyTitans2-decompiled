using System;
using UnityEngine;

[Serializable]
public class DiminishingReturnsSetting
{
	[Tooltip("After this amount of uses, diminishing returns takes affect on the ability.")]
	public int uses;

	[Range(0f, 1f)]
	public float effectPowerReduction;
}
