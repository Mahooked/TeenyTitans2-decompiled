using System;
using UnityEngine;

[Serializable]
public class BattleCondition
{
	public enum ConditionType
	{
		FigureName,
		FigureTags,
		FigureLevel,
		FigureShinyRank,
		Accessory,
		TeamSize,
		FigureClass,
		FigureRepainted
	}

	[SerializeField]
	private ConditionType _conditionType;

	[SerializeField]
	private int _intValue1;

	[SerializeField]
	private int _intValue2;

	[SerializeField]
	private string _stringValue1;

	[SerializeField]
	private Comparison _comparison1;

	[SerializeField]
	private Comparison _comparison2;

	[SerializeField]
	private IsOrNot _isOrNot1;

	public ConditionType conditionType
	{
		get
		{
			return _conditionType;
		}
		set
		{
			_conditionType = value;
		}
	}

	public int intValue1
	{
		get
		{
			return _intValue1;
		}
		set
		{
			_intValue1 = value;
		}
	}

	public int intValue2
	{
		get
		{
			return _intValue2;
		}
		set
		{
			_intValue2 = value;
		}
	}

	public string stringValue1
	{
		get
		{
			return _stringValue1;
		}
		set
		{
			_stringValue1 = value;
		}
	}

	public Comparison comparison1
	{
		get
		{
			return _comparison1;
		}
		set
		{
			_comparison1 = value;
		}
	}

	public Comparison comparison2
	{
		get
		{
			return _comparison2;
		}
		set
		{
			_comparison2 = value;
		}
	}

	public IsOrNot isOrNot1
	{
		get
		{
			return _isOrNot1;
		}
		set
		{
			_isOrNot1 = value;
		}
	}

	public BattleCondition Copy()
	{
		BattleCondition battleCondition = new BattleCondition();
		battleCondition._conditionType = _conditionType;
		battleCondition._intValue1 = _intValue1;
		battleCondition._intValue2 = _intValue2;
		battleCondition._stringValue1 = _stringValue1;
		battleCondition._comparison1 = _comparison1;
		battleCondition._comparison2 = _comparison2;
		battleCondition._isOrNot1 = _isOrNot1;
		return battleCondition;
	}
}
