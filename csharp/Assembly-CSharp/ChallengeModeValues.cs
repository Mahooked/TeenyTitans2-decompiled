using System;
using UnityEngine;

[Serializable]
public class ChallengeModeValues
{
	[SerializeField]
	private float _timer = 1f;

	[SerializeField]
	private float _floatValue1;

	[SerializeField]
	private float _floatValue2 = 1f;

	[SerializeField]
	private int _intValue1;

	[SerializeField]
	private int _intValue2;

	[SerializeField]
	private bool _boolValue1;

	[SerializeField]
	private int _battleCoinReward = 50;

	[SerializeField]
	private int _coinReward = 100;

	[SerializeField]
	private int _xpReward = 100;

	[SerializeField]
	private int _justicePoints = 100;

	public float timer => _timer;

	public float floatValue1 => _floatValue1;

	public float floatValue2 => _floatValue2;

	public int intValue1 => _intValue1;

	public int intValue2 => _intValue2;

	public bool boolValue1 => _boolValue1;

	public int battleCoinReward => _battleCoinReward;

	public int coinReward => _coinReward;

	public int xpReward => _xpReward;

	public int justicePoints => _justicePoints;
}
