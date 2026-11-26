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

	public float timer
	{
		get
		{
			return _timer;
		}
	}

	public float floatValue1
	{
		get
		{
			return _floatValue1;
		}
	}

	public float floatValue2
	{
		get
		{
			return _floatValue2;
		}
	}

	public int intValue1
	{
		get
		{
			return _intValue1;
		}
	}

	public int intValue2
	{
		get
		{
			return _intValue2;
		}
	}

	public bool boolValue1
	{
		get
		{
			return _boolValue1;
		}
	}

	public int battleCoinReward
	{
		get
		{
			return _battleCoinReward;
		}
	}

	public int coinReward
	{
		get
		{
			return _coinReward;
		}
	}

	public int xpReward
	{
		get
		{
			return _xpReward;
		}
	}

	public int justicePoints
	{
		get
		{
			return _justicePoints;
		}
	}
}
